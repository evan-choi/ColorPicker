using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ColorPicker.Utils
{
    public struct RGBColor : IColor
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public ColorType Type
        {
            get
            {
                return ColorType.RGB;
            }
        }

        public RGBColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public RGBColor(Color c)
        {
            R = c.R;
            G = c.G;
            B = c.B;
        }

        public static RGBColor FromColor(Color c)
        {
            return new RGBColor(c);
        }

        public Color ToColor()
        {
            return Color.FromArgb(R, G, B);
        }

        public override string ToString()
        {
            return $"RGB ({R}, {G}, {B})";
        }
    }
}
