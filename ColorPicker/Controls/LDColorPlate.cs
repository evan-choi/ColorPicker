using System;
using System.Drawing;
using System.Windows.Forms;

using ColorPicker.Utils;

/// <summary>
/// Lighter / Darker Color Plate
/// </summary>
namespace ColorPicker.Controls
{
    public partial class LDColorPlate : UserControl
    {
        public event SelectedColorChangedHandler SelectedColorChanged;
        public delegate void SelectedColorChangedHandler(object sender, Color value);

        private Color _bColor = Color.White;
        public Color BaseColor
        {
            get
            {
                return _bColor;
            }
            set
            {
                mCurrentIdx = -1;
                _selectedIndex = -1;

                _bColor = value;

                this.Invalidate();
            }
        }

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                this.Invalidate();
            }
        }

        private int mCurrentIdx = -1;
        private bool mMouseDown = false;

        public LDColorPlate()
        {
            InitializeComponent();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            HSLColor oHlc = HSLColor.FromColor(_bColor);
            float oLight = oHlc.Lightness;

            float height = this.Height / 22f;
            mCurrentIdx = _selectedIndex;

            for (int i = 0; i <= 20; i++)
            {
                bool c = false;
                bool fc = (mCurrentIdx < i && mCurrentIdx != -1);

                oHlc.Lightness = 1f - (i * 5f) / 100;
                
                // 오차범위 계산
                if (_selectedIndex == -1 && Math.Abs(oLight - oHlc.Lightness) <= 0.025)
                {
                    mCurrentIdx = i;
                    c = true;
                }

                float dHeight = height * (fc ? 1f : 2f);
                float top = (i + (fc ? 1 : 0)) * height + dHeight / 2f;

                using (SolidBrush sb = new SolidBrush(c ? _bColor : oHlc.ToColor()))
                {
                    using (Pen p = new Pen(sb, dHeight))
                    {
                        g.DrawLine(p, new PointF(0, top), new PointF(this.Width, top));
                    }
                }
            }

            if (mCurrentIdx > -1)
            {
                float top = mCurrentIdx * height;

                oHlc.Lightness = 1f - (mCurrentIdx * 5f) / 100;

                using (SolidBrush sb = new SolidBrush(ColorUtils.Invert(oHlc.ToColor())))
                {
                    using (Pen p = new Pen(sb))
                    {
                        g.DrawRectangle(p, new Rectangle(0, (int)top, this.Width - 1, (int)(height * 2f)));
                    }
                }
            }

            base.OnPaint(e);
        }


        private void LDColorPlate_MouseDown(object sender, MouseEventArgs e)
        {
            mMouseDown = true;
            ColorScroll(e);
        }

        private void LDColorPlate_MouseMove(object sender, MouseEventArgs e)
        {
            if (mMouseDown)
            {
                ColorScroll(e);
            }
        }

        private void LDColorPlate_MouseUp(object sender, MouseEventArgs e)
        {
            mMouseDown = false;
        }

        private void ColorScroll(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mCurrentIdx > -1)
            {
                HSLColor oHlc = HSLColor.FromColor(_bColor);

                int idx = (int)Math.Floor(e.Y / (this.Height / 22f));
                int dt = idx - mCurrentIdx;

                if (dt >= 0 && dt <= 1)
                {
                    idx = mCurrentIdx;
                }

                if (idx > mCurrentIdx)
                {
                    idx -= 1;
                }

                SelectedIndex = Math.Max(Math.Min(idx, 20), 0);

                oHlc.Lightness = 1f - (SelectedIndex * 5f) / 100;

                SelectedColorChanged?.Invoke(this, oHlc.ToColor());
            }
        }
    }
}
