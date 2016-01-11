using ColorPicker.Input;
using ColorPicker.Utils;
using ColorPicker.Utils.Hotkey;
using ColorPicker.Windows.Base;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace ColorPicker.Windows
{
    public partial class MainWindow : SkinWindow
    {
        #region [ 전역 변수 ]
        // 설정
        private Setting setting;

        // 작업
        private bool mPause = false;
        private bool mLastPause = false;
        private BackgroundWorker mWorker;
        private ImageList cList;

        // 확장
        private bool mExtended = false;
        private System.Timers.Timer mAnimateTimer;
        #endregion

        #region [ 초기화 ]
        public MainWindow()
        {
            InitializeComponent();
            InitWorker();
            InitToolTip();
            InitHotKey();
            
            GlobalException.Init();

            setting = new Setting();

            cList = new ImageList();
            cList.ImageSize = new Size(16, 16);
            recordView.SmallImageList = cList;

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

        // 임시 핫키 세팅과 연동 필요함
        private void InitHotKey()
        {
            HotkeyManager.Register("Pause", new HotKey()
            {
                Keys = new Keys[] { Keys.F1 },
                Action = () =>
                {
                    mPause = !mPause;
                }
            });

            HotkeyManager.Register("Zoom_IN", new HotKey()
            {
                Keys = new Keys[] { Keys.Add },
                Action = () =>
                {
                    zoomSlider.Value += 1;
                }
            });

            HotkeyManager.Register("Zoom_OUT", new HotKey()
            {
                Keys = new Keys[] { Keys.Subtract },
                Action = () =>
                {
                    zoomSlider.Value -= 1;
                }
            });

            HotkeyManager.Register("Color_Snapshot", new HotKey()
            {
                Keys = new Keys[] { Keys.F2 },
                Action = () =>
                {
                    AddColor(RGBColor.FromColor(ldcPlate.BaseColor));
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
                    if (!mPause)
                    {
                        Processing();
                    }

                    mre.WaitOne(10, true);
                }
            }
        }

        private void Processing()
        {
            // ViewSize
            Size vSize = new Size(100, 100);

            // View Half Size
            Size vhSize = new Size(vSize.Width / 2, vSize.Height / 2);

            // Capture Size
            Size scSize = new Size(SelectOdd(vSize.Width / setting.Zoom), SelectOdd(vSize.Height / setting.Zoom));

            // ZoomBuffer Size
            Size zbSize = new Size((int)(scSize.Width * setting.Zoom), (int)(scSize.Height * setting.Zoom));

            // Remain Size
            Size rmSize = zbSize - vSize;

            // Capture Area
            Point mPt = Mouse.Position;
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
                if (setting.ShowGrid)
                {
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(100, Color.White)))
                    {
                        using (Pen p = new Pen(sb))
                        {
                            int z = (int)setting.Zoom;

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

            UIInvoke(() =>
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
            ToolTipManager.Show("복사됐습니다", 500);

            UIInvoke(() =>
            {
                Bitmap icon = DrawColorIcon(c.ToColor());
                cList.Images.Add(icon);

                ListViewItem itm = new ListViewItem();
                itm.SubItems.Add(c.ToString());
                itm.SubItems.Add(c.Type.ToString());

                itm.ImageIndex = cList.Images.Count - 1;

                recordView.Items.Add(itm);
                recordView.EnsureVisible(recordView.Items.Count - 1);
            });
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
                mAnimateTimer.Stop();

                mAnimateTimer.Dispose();
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
            SettingWindow sw = new SettingWindow();
            sw.ShowDialog();
        }
        #endregion

        #region [ 함수 ]
        private delegate void Action();
        private void UIInvoke(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

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
    }
}