using ColorPicker.Input;
using ColorPicker.Native;
using ColorPicker.Utils;
using ColorPicker.Utils.Hotkey;
using ColorPicker.Windows.Base;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ColorPicker.Windows
{
    public partial class MainWindow : SkinWindow
    {
        #region [ 전역 변수 ]
        // 설정
        private const int AutoSaveDelay = 1000 * 30;

        private DateTime lastSave = DateTime.Now;
        private SettingWindow settingWindow;
        private Setting setting;

        // 작업
        private bool mPause = false;
        private bool mLastPause = false;
        private bool mInWorkArea = false;
        private BackgroundWorker mWorker;
        private ImageList cList;
        private Dictionary<int, int> iconCache = new Dictionary<int, int>();

        // 확장
        private bool mExtended = false;
        private System.Timers.Timer mAnimateTimer;

        // 팔레트
        private ContextMenu paletteMenu;
        private int selectedPalette = -1;
        private string selectedPaletteName = "";
        #endregion

        #region [ 초기화 ]
        public MainWindow()
        {
            InitializeComponent();

            CrossThreading.Init();
            ListViewHelper.EnableDoubleBuffer(lvPalette);
            ListViewHelper.EnableDoubleBuffer(recordView);

            InitWorker();
            InitToolTip();
            InitHotKey();
            InitPaletteMenu();

            cList = new ImageList();
            cList.ImageSize = new Size(16, 16);
            recordView.SmallImageList = cList;

            InitSetting();

            if (lvPalette.Items.Count == 0)
            {
                AddPalette("Palette1");
            }

            SelectPalette(0);

            this.Height = this.MinimumSize.Height;
            this.Shown += MainWindow_Shown;
            this.FormClosing += MainWindow_FormClosing;

            zoomSlider.Scroll += ZoomSlider_Scroll;
            ldcPlate.SelectedColorChanged += LdcPlate_SelectedColorChanged;
            extender.Click += Extender_Click;
        }
        
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mWorker.IsBusy)
            {
                mWorker.CancelAsync();
                e.Cancel = true;
            }
            else
            {
                HotkeyManager.kHook.UnHook();
                PaletteManager.SaveAll();
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            mWorker.WorkerSupportsCancellation = true;
            mWorker.RunWorkerAsync();
        }

        private void InitWorker()
        {
            mWorker = new BackgroundWorker();
            mWorker.DoWork += MWorkThread_DoWork;
            mWorker.RunWorkerCompleted += MWorker_RunWorkerCompleted;
        }

        private void InitToolTip()
        {
            ToolTipManager.Init(this);

            ToolTipManager.SetToolTip(extender, "색상 기록을 확장합니다.");
            ToolTipManager.SetToolTip(lbl_HTML, "색상을 HEX로 표현한 값 입니다.");
            ToolTipManager.SetToolTip(lbl_RGB, "색상을 RGB로 표현한 값 입니다.");
            ToolTipManager.SetToolTip(lbl_HSL, "색상을 HSL로 표현한 값 입니다.");
            ToolTipManager.SetToolTip(lbl_HSV, "색상을 HSB로 표현한 값 입니다.");
            ToolTipManager.SetToolTip(chkGrid, "캡쳐영역에 격자를 표시합니다.");
            ToolTipManager.SetToolTip(chkSemi, "키보드 방향키로 마우스를 미세조정합니다.");
            ToolTipManager.SetToolTip(ldcPlate, "클릭하여 생삭의 밝기를 조절할 수 있습니다.");
            ToolTipManager.SetToolTip(zoomSlider, "스크롤하여 확대를할 수 있습니다.");
        }

        private void InitPaletteMenu()
        {
            paletteMenu = new ContextMenu();

            MenuItem mRename = new MenuItem("이름 변경");
            MenuItem mOpen = new MenuItem("폴더 열기");
            MenuItem mDelete = new MenuItem("삭제");

            mRename.Click += MRename_Click;
            mOpen.Click += MOpen_Click;
            mDelete.Click += MDelete_Click;

            paletteMenu.MenuItems.AddRange(new MenuItem[] { mRename, mOpen, mDelete });
        }

        private void InitSetting()
        {
            setting = new Setting($"{AppDomain.CurrentDomain.BaseDirectory}setting.ini");
            settingWindow = new SettingWindow(setting);

            setting.Updated += (s) => {
                UpdateSetting();
            };

            UpdateSetting();
        }

        internal void UpdateSetting()
        {
            zoomSlider.Value = (int)setting.Zoom;
            chkGrid.Checked = setting.ShowGrid;
            chkSemi.Checked = setting.SemiControl;

            // Palette List
            lvPalette.Items.Clear();

            List<string> created = new List<string>();

            foreach (var pf in setting.PaletteList)
            {
                string tmp = pf.ToLower();

                if (!created.Contains(tmp) && pf.Length > 0 && File.Exists(pf))
                {
                    PaletteItem item = new PaletteItem(pf);
                    item.Load();
                    CreatePaletteColorItems(item);

                    PaletteManager.AddPalette(item);

                    lvPalette.Items.Add(item.Name);

                    created.Add(tmp);
                }
            }

            SelectPalette(selectedPalette);
        }

        private void SetCursorPosDelta(Point pt)
        {
            Point mpt = Mouse.Position + (Size)pt;
            NativeMethods.SetCursorPos(mpt.X, mpt.Y);
        }

        // 임시 핫키 세팅과 연동 필요함
        private void InitHotKey()
        {
            Keys[] semi_hkKeys = new Keys[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };
            Point[] semi_hkDeltas = new Point[] { new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1) };

            for (int i = 0; i < semi_hkDeltas.Length; i++)
            {
                int idx = i;
                
                HotkeyManager.Register($"SEMI_{semi_hkKeys[idx].ToString()}", new HotKey()
                {
                    Keys = new Keys[] { semi_hkKeys[idx] },
                    Action = () =>
                    {
                        if (setting.SemiControl)
                        {
                            Point acPt = Point.Empty;

                            if (HotkeyManager.IsShift()){
                                acPt = new Point(semi_hkDeltas[idx].X * 3, semi_hkDeltas[idx].Y * 3);
                            }

                            SetCursorPosDelta(semi_hkDeltas[idx] + (Size)acPt);
                        }
                    }
                });
            }

            HotkeyManager.Register("Pause", new HotKey()
            {
                Keys = new Keys[] { Keys.None, Keys.F1 },
                Action = () =>
                {
                    mPause = !mPause;
                }
            });

            HotkeyManager.Register("ZoomIn", new HotKey()
            {
                Keys = new Keys[] { Keys.None, Keys.Add },
                Action = () =>
                {
                    zoomSlider.Value += 1;
                }
            });

            HotkeyManager.Register("ZoomOut", new HotKey()
            {
                Keys = new Keys[] { Keys.None, Keys.Subtract },
                Action = () =>
                {
                    zoomSlider.Value -= 1;
                }
            });

            HotkeyManager.Register("RGB", new HotKey()
            {
                Keys = new Keys[] { Keys.None, Keys.F2 },
                Action = () =>
                {
                    AddColor(RGBColor.FromColor(ldcPlate.SelectedColor));
                }
            });

            HotkeyManager.Register("HEX", new HotKey()
            {
                Keys = new Keys[] { Keys.None, Keys.F3 },
                Action = () =>
                {
                    AddColor(HEXColor.FromColor(ldcPlate.SelectedColor));
                }
            });

            HotkeyManager.Register("HSL", new HotKey()
            {
                Keys = new Keys[] { Keys.None, Keys.F4 },
                Action = () =>
                {
                    AddColor(HSLColor.FromColor(ldcPlate.SelectedColor));
                }
            });

            HotkeyManager.Register("HSB", new HotKey()
            {
                Keys = new Keys[] { Keys.None, Keys.F5 },
                Action = () =>
                {
                    AddColor(HSVColor.FromColor(ldcPlate.SelectedColor));
                }
            });
        }
        #endregion

        #region [ 작업 ]
        private void MWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void MWorkThread_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var mre = new ManualResetEvent(false))
            {
                while (!mWorker.CancellationPending)
                {
                    AutoSave();

                    if (!mPause)
                    {
                        Processing();
                    }

                    mre.WaitOne(10, true);
                }
            }
        }
        
        private void AutoSave()
        {
            TimeSpan delta = DateTime.Now - lastSave;

            if (delta.TotalMilliseconds >= AutoSaveDelay)
            {
                PaletteManager.SaveAll();
                lastSave = DateTime.Now;
            }
        }

        private void Processing()
        {
            // Prevent Zoom Tolerance
            float Zoom = setting.Zoom;

            // ViewSize
            Size vSize = new Size(100, 100);

            // View Half Size
            Size vhSize = new Size(vSize.Width / 2, vSize.Height / 2);

            // Capture Size
            Size scSize = new Size(SelectOdd(vSize.Width / Zoom), SelectOdd(vSize.Height / Zoom));

            // ZoomBuffer Size
            Size zbSize = new Size((int)(scSize.Width * Zoom), (int)(scSize.Height * Zoom));

            // Remain Size
            Size rmSize = zbSize - vSize;

            // Capture Area
            Point mPt = Mouse.Position;
            if (IsActivated && IntersectWith(this.Bounds, mPt))
            {
                if (mInWorkArea)
                {
                    return;
                }
                else
                {
                    mInWorkArea = true;
                }

                mPt = this.Location;
            }
            else
            {
                mInWorkArea = false;
            }

            Rectangle area = new Rectangle(mPt.X - scSize.Width / 2, mPt.Y - scSize.Height / 2, scSize.Width, scSize.Height);

            // Capture
            Bitmap buffer = ScreenCapture.Capture(area);

            // Picked Color
            Color pColor = buffer.GetPixel(area.Width / 2, area.Height / 2);

            // ViewBox Processing
            Bitmap view = new Bitmap(vSize.Width, vSize.Height);
            using (Graphics g = Graphics.FromImage(view))
            {
                Point dPos = new Point(-rmSize.Width / 2, -rmSize.Height / 2);

                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                g.DrawImage(buffer, new Rectangle(dPos, zbSize));

                // Grid Processing
                if (setting.ShowGrid && Zoom >= 2f)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(100, Color.White)))
                    {
                        using (Pen p = new Pen(sb))
                        {
                            int z = (int)Zoom;

                            for (int x = dPos.X; x <= vSize.Width; x += z)
                            {
                                if (x >= 0)
                                {
                                    g.DrawLine(p, new PointF(x, 0), new PointF(x, vSize.Height));
                                }
                            }

                            for (int y = dPos.Y; y <= vSize.Height; y += z)
                            {
                                if (y >= 0)
                                {
                                    g.DrawLine(p, new PointF(0, y), new PointF(vSize.Width, y));
                                }
                            }
                        }
                    }
                }

                // CrossLine Processing
                for (int x = 0; x < vSize.Width; x++)
                {
                    Color px = view.GetPixel(x, vhSize.Height);

                    view.SetPixel(x, vhSize.Height, ColorUtils.Invert(px));
                }

                for (int y = 0; y < vSize.Height; y++)
                {
                    Color px = view.GetPixel(vhSize.Width, y);

                    view.SetPixel(vhSize.Width, y, ColorUtils.Invert(px));
                }
            }

            buffer.Dispose();

            SetPickedColor(pColor);

            CrossThreading.UIInvoke(() =>
            {
                ldcPlate.BaseColor = pColor;

                viewBox.Image?.Dispose();
                viewBox.Image = view;
            });
        }

        private void SetPickedColor(Color pColor)
        {
            Bitmap cView = new Bitmap(colorBox.Width, colorBox.Height);
            using (Graphics g = Graphics.FromImage(cView))
            {
                g.Clear(pColor);
                using (SolidBrush sb = new SolidBrush(ColorUtils.Invert(pColor)))
                {
                    using (Pen p = new Pen(sb, 1))
                    {
                        g.DrawRectangle(p, new Rectangle(1, 1, cView.Width - 3, cView.Height - 3));
                    }
                }
            }

            HSLColor hlc = HSLColor.FromColor(pColor);
            HSVColor hvc = HSVColor.FromColor(pColor);
            RGBColor rgc = RGBColor.FromColor(pColor);
            HEXColor hxc = HEXColor.FromColor(pColor);
            
            lbl_HTML.Text = hxc.ToString();
            lbl_RGB.Text = rgc.ToString();
            lbl_HSL.Text = hlc.ToString();
            lbl_HSV.Text = hvc.ToString();

            colorBox.Image?.Dispose();
            colorBox.Image = cView;
        }

        private void LdcPlate_SelectedColorChanged(object sender, Color value)
        {
            SetPickedColor(value);
        }
        #endregion

        #region [ 기록 ]
        private void AddColor(IColor c)
        {
            CrossThreading.UIInvoke(() =>
            {
                foreach (ListViewItem item in recordView.Items)
                {
                    if (item.SubItems[1].Text == c.ToString())
                    {
                        return;
                    }
                }

                Copy(c);
                                
                ListViewItem itm = PaletteManager.Add(selectedPaletteName, c, GetItemIconIndex(c));
                recordView.Items.Add(itm);
                recordView.EnsureVisible(recordView.Items.Count - 1);
            });
        }

        private void CreatePaletteColorItems(PaletteItem p)
        {
            foreach (IColor c in p.Items)
            {
                p.Add(c, GetItemIconIndex(c));
            }
        }

        private int GetItemIconIndex(IColor c)
        {
            int idx = -1;
            int argb = c.ToColor().ToArgb();

            if (!iconCache.ContainsKey(argb))
            {
                Bitmap icon = DrawColorIcon(c.ToColor());
                cList.Images.Add(icon);
                idx = cList.Images.Count - 1;

                iconCache.Add(argb, idx);
            }
            else
            {
                idx = iconCache[argb];
            }

            return idx;
        }

        private Bitmap DrawColorIcon(Color c)
        {
            Bitmap icon = new Bitmap(16, 16);

            using (Graphics g = Graphics.FromImage(icon))
            {
                g.Clear(c);

                using (SolidBrush sb = new SolidBrush(ColorUtils.Invert(c)))
                {
                    using (Pen p = new Pen(sb))
                    {
                        g.DrawRectangle(p, new Rectangle(1, 1, 13, 13));
                    }
                }
            }

            return icon;
        }

        protected override void OnMinimize()
        {
            mLastPause = mPause;
            mPause = true;

            base.OnMinimize();
        }

        protected override void OnRestore()
        {
            mPause = mLastPause;

            base.OnRestore();
        }
        #endregion

        #region [ 윈도우 확장 ]
        private void Extender_Click(object sender, EventArgs e)
        {
            if (mExtended)
            {
                UnExtend();
            }
            else
            {
                Extend();
            }

            extender.Image = (mExtended ? Properties.Resources.arrow_down : Properties.Resources.arrow_up);

            mExtended = !mExtended;
        }

        private void Extend()
        {
            StopAnimation();
            EaseCircleOut(this.Height, this.MaximumSize.Height, 500);
        }

        private void UnExtend()
        {
            StopAnimation();
            EaseCircleOut(this.Height, this.MinimumSize.Height, 500);
        }

        private void EaseCircleOut(int from, int to, int duration)
        {
            DateTime stDt = DateTime.Now;

            mAnimateTimer = new System.Timers.Timer() { Interval = 10 };
            mAnimateTimer.Elapsed += (s, e) =>
            {
                TimeSpan elapsed = DateTime.Now - stDt;
                double time = Math.Min(elapsed.TotalMilliseconds, duration);
                double v = easeOut_Circle(time, from, to - from, duration);
                
                this.Height = (int)v;

                if (time >= duration) StopAnimation();
            };

            mAnimateTimer.Start();
        }

        private void StopAnimation()
        {
            if (mAnimateTimer != null)
            {
                mAnimateTimer?.Stop();

                mAnimateTimer?.Dispose();
                mAnimateTimer = null;
            }
        }
        
        private double easeOut_Circle(double time, double from, double increase, int duration)
        {
            double tt = time;
            return increase * Math.Sqrt(1 - (InlineAssignHelper<double>(ref tt, tt / duration - 1)) * tt) + from;
        }
        
        private static T InlineAssignHelper<T>(ref T target, T value)
        {
            target = value;
            return value;
        }
        #endregion

        #region [ 설정 ]
        private void ZoomSlider_Scroll(object sender, int value)
        {
            setting.Zoom = value;
        }

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            setting.ShowGrid = chkGrid.Checked;
        }

        private void chkSemi_CheckedChanged(object sender, EventArgs e)
        {
            setting.SemiControl = chkSemi.Checked;
        }

        protected override void OnSettingOpen(object obj)
        {
            bool lPause = mPause;

            mPause = true;
            HotkeyManager.Enabled = false;

            settingWindow.ShowDialog();

            mPause = lPause;
            HotkeyManager.Enabled = true;
        }
        #endregion

        #region [ 함수 ]
        private int SelectOdd(float v)
        {
            int c = (int)Math.Ceiling(v);

            if (c % 2 == 1)
            {
                return c;
            }
            else
            {
                return c + 1;
            }
        }
        #endregion

        #region [ 컬러 팔렛트 ]
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int idx = 0;

            foreach (var p in PaletteManager.GetPaletteNames())
            {
                if (p.ToLower().StartsWith("palette"))
                {
                    string ns = p.Substring(7);
                    int n;

                    if (int.TryParse(ns, out n))
                    {
                        idx = Math.Max(n, idx);
                    }
                }
            }

            if (InputWindow.Show("팔레트이름을 입력해주세요", $"Palette{idx + 1}") == DialogResult.OK)
            {
                string name = InputWindow.Result.Trim();

                if (name.Length > 0)
                {
                    if (!CheckPalette(name))
                    {
                        return;
                    }

                    AddPalette(name);
                }
            }
        }
        #endregion

        private void lvPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPalette.SelectedItems.Count > 0)
            {
                SelectPalette(lvPalette.Items.IndexOf(lvPalette.SelectedItems[0]));

                lvPalette.SelectedItems[0].Selected = false;
            }
        }

        private void AddPalette(string name)
        {
            name = name.Trim();
            
            PaletteManager.AddPalette(name);
            lvPalette.Items.Add(name);

            SavePaletteList();
        }

        private void SelectPalette(int idx)
        {
            if (idx < 0 || idx >= lvPalette.Items.Count) return;

            if (selectedPalette >= 0 && selectedPalette < lvPalette.Items.Count) lvPalette.Items[selectedPalette].ForeColor = Color.Black;
            lvPalette.Items[idx].ForeColor = Color.FromArgb(41, 128, 185);

            selectedPalette = idx;
            selectedPaletteName = PaletteManager.NameFromIndex(idx);

            recordView.Items.Clear();
            recordView.Items.AddRange(PaletteManager.GetPalette(selectedPaletteName).ViewItems.ToArray());

            if (recordView.Items.Count > 0)
            {
                recordView.EnsureVisible(recordView.Items.Count - 1);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteSelectedPalette();
        }

        private void DeleteSelectedPalette()
        {
            if (lvPalette.Items.Count > 1)
            {
                lvPalette.Items.RemoveAt(selectedPalette);
                PaletteManager.RemovePalette(selectedPalette);

                SelectPalette(Math.Max(selectedPalette - 1, 0));

                SavePaletteList();
            }
            else
            {
                MessageBox.Show("팔레트는 최소한 한개이상 존재해야됩니다.", "ColorPicker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SavePaletteList()
        {
            var lst = new List<string>();

            foreach (var p in PaletteManager.GetPalettes())
            {
                lst.Add(p.FileInfo.FullName);
            }

            setting.PaletteList = lst.ToArray();
        }

        private void recordView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (recordView.SelectedItems.Count > 0)
            {
                IColor c = (IColor)recordView.SelectedItems[0].Tag;

                Copy(c);
            }
        }

        private void Copy(IColor c)
        {
            Clipboard.SetText(c.ToString());
            ToolTipManager.Show("복사됐습니다", 500);
        }

        private void recordView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && recordView.SelectedItems.Count > 0)
            {
                ArrayList arr = new ArrayList(recordView.SelectedItems);
                PaletteItem p = PaletteManager.GetPalette(selectedPaletteName);

                foreach (ListViewItem itm in arr)
                {
                    IColor c = (IColor)itm.Tag;

                    p.Items.Remove(c);
                    p.ViewItems.Remove(itm);
                    recordView.Items.Remove(itm);
                }
            }
        }

        private void lvPalette_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                paletteMenu.Show(lvPalette, new Point(e.X, e.Y));
            }
        }

        private void MDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedPalette();
        }

        private void MOpen_Click(object sender, EventArgs e)
        {
            PaletteItem p = PaletteManager.GetPalette(selectedPaletteName);
            Process.Start(p.FileInfo.DirectoryName);
        }

        private void MRename_Click(object sender, EventArgs e)
        {
            if (InputWindow.Show("변경할 이름을 설정해주세요", selectedPaletteName) == DialogResult.OK)
            {
                string name = InputWindow.Result.Trim();

                if (!CheckPalette(name))
                {
                    return;
                }

                PaletteManager.RenamePalette(selectedPaletteName, name);
                lvPalette.Items[selectedPalette].Text = name;

                SavePaletteList();

                selectedPaletteName = name;
            }
        }

        private bool CheckPalette(string name)
        {
            foreach (var n in PaletteManager.GetPaletteNames())
            {
                if (n.ToLower() == name.ToLower())
                {
                    MessageBox.Show("이미 존재하는 이름입니다", "ColorPicker", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            return true;
        }

        private void lbl_HTML_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Copy(HEXColor.FromColor(ldcPlate.SelectedColor));
        }

        private void lbl_RGB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Copy(RGBColor.FromColor(ldcPlate.SelectedColor));
        }

        private void lbl_HSL_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Copy(HSLColor.FromColor(ldcPlate.SelectedColor));
        }

        private void lbl_HSV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Copy(HSVColor.FromColor(ldcPlate.SelectedColor));
        }

        private void recordView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (recordView.SelectedItems.Count > 0)
            {
                IColor c = (IColor)recordView.SelectedItems[0].Tag;
                SetPickedColor(c.ToColor());
                ldcPlate.BaseColor = c.ToColor();
            }
        }

        private void lvPalette_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                
                foreach (var f in files)
                {
                    if (Path.GetExtension(f).ToLower() == ".p")
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
        }

        private void lvPalette_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var f in files)
                {
                    if (Path.GetExtension(f).ToLower() == ".p")
                    {
                        if (CheckPalette(Path.GetFileNameWithoutExtension(f).ToLower()))
                        {
                            PaletteItem p = new PaletteItem(f);
                            p.Load();
                            CreatePaletteColorItems(p);

                            PaletteManager.AddPalette(p);

                            lvPalette.Items.Add(p.Name);
                        }
                    }
                }

                SavePaletteList();
            }
        }
    }
}