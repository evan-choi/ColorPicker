using ColorPicker.Input;
using ColorPicker.Utils;
using ColorPicker.Windows.Base;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace ColorPicker.Windows
{
    public partial class MainWindow : SkinWindow
    {
        private BackgroundWorker mWorker;
        
        public MainWindow()
        {
            InitializeComponent();
            
            InitWorker();

            this.Shown += MainWindow_Shown;
            this.zoomSlider.Scroll += ZoomSlider_Scroll;
        }

        private void ZoomSlider_Scroll(object sender, int value)
        {
            Setting.Zoom = (float)value;
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            mWorker.RunWorkerAsync();
        }

        private void InitWorker()
        {
            mWorker = new BackgroundWorker();
            mWorker.DoWork += MWorkThread_DoWork;
        }

        private void MWorkThread_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var mre = new ManualResetEvent(false))
            {
                while (true)
                {
                    // ViewSize
                    Size vSize = new Size(100, 100);

                    // View Half Size
                    Size vhSize = new Size(vSize.Width / 2, vSize.Height / 2);

                    // Capture Size
                    Size scSize = new Size(SelectOdd(vSize.Width / Setting.Zoom), SelectOdd(vSize.Height / Setting.Zoom));

                    // ZoomBuffer Size
                    Size zbSize = new Size((int)(scSize.Width * Setting.Zoom), (int)(scSize.Height * Setting.Zoom));

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
                        if (Setting.ShowGrid)
                        {
                            using (SolidBrush sb = new SolidBrush(Color.FromArgb(100, Color.White)))
                            {
                                using (Pen p = new Pen(sb))
                                {
                                    int z = (int)Setting.Zoom;

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

                    UIInvoke(() =>
                    {
                        SetPickedColor(pColor);

                        ldcPlate.BaseColor = pColor;

                        viewBox.Image?.Dispose();
                        viewBox.Image = view;
                    });

                    mre.Reset();
                    mre.Set();
                    mre.WaitOne(10, true);
                }
            }
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

            lbl_HTML.Text = $"#{HEXColor.FromColor(pColor).Html}";
            lbl_RGB.Text = $"RGB ({pColor.R}, {pColor.G}, {pColor.B})";
            lbl_HSL.Text = $"HSL ({(int)hlc.Hue}, {(int)(hlc.Saturation * 100)}%, {(int)(hlc.Lightness * 100)}%)";
            lbl_HSV.Text = $"HSB ({(int)hvc.Hue}, {(int)(hvc.Saturation * 100)}%, {(int)(hvc.Value * 100)}%)";

            colorBox.Image?.Dispose();
            colorBox.Image = cView;
        }

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

        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            Setting.ShowGrid = chkGrid.Checked;
        }

        private void chkSemi_CheckedChanged(object sender, EventArgs e)
        {
            Setting.SemiControl = chkSemi.Checked;
        }
    }
}
