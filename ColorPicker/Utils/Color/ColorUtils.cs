using System;
using System.Drawing;

namespace ColorPicker.Utils
{
    public static class ColorUtils
    {
        public static Color Invert(Color c)
        {
            return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
        }
    }
}
