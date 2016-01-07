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
        private Color _bColor = Color.White;
        public Color BaseColor
        {
            get
            {
                return _bColor;
            }
            set
            {
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
            float cIdx = _selectedIndex;

            for (int i = 0; i <= 20; i++)
            {
                bool c = false;
                bool fc = (cIdx < i && cIdx != -1);

                oHlc.Lightness = 1f - (i * 5f) / 100;
                
                // 오차범위 계산
                if (_selectedIndex == -1 && Math.Abs(oLight - oHlc.Lightness) <= 0.025)
                {
                    cIdx = i;
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

            if (cIdx > -1)
            {
                float top = cIdx * height;

                using (SolidBrush sb = new SolidBrush(ColorUtils.Invert(_bColor)))
                {
                    using (Pen p = new Pen(sb))
                    {
                        g.DrawRectangle(p, new Rectangle(0, (int)top, this.Width - 1, (int)(height * 2f)));
                    }
                }
            }

            base.OnPaint(e);
        }
    }
}
