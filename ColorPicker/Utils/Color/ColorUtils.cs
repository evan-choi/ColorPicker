using System;
using System.Drawing;

namespace ColorPicker.Utils
{
    public static class ColorUtils
    {
        public static Color Invert(Color c)
        {
            byte r = (byte)Math.Sqrt(ushort.MaxValue - Math.Pow(c.R, 2));
            byte g = (byte)Math.Sqrt(ushort.MaxValue - Math.Pow(c.G, 2));
            byte b = (byte)Math.Sqrt(ushort.MaxValue - Math.Pow(c.B, 2));

            return Color.FromArgb(r, g, b);
        }
    }
}
