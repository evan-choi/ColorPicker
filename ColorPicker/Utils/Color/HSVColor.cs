using System;
using System.Drawing;

namespace ColorPicker.Utils
{
    public struct HSVColor
    {

        public float Hue { get; set; }
        public float Saturation { get; set; }
        public float Value { get; set; }

        public HSVColor(Color c)
        {
            int max = Math.Max(c.R, Math.Max(c.G, c.B));
            int min = Math.Min(c.R, Math.Min(c.G, c.B));

            Hue = c.GetHue();
            Saturation = (max == 0) ? 0f : 1f - (1f * min / max);
            Value = max / 255f;
        }

        public Color ToColor()
        {
            int hi = Convert.ToInt32(Math.Floor(Hue / 60)) % 6;
            double f = Hue / 60 - Math.Floor(Hue / 60);
            
            int v = Convert.ToInt32(Value * 255);
            int p = Convert.ToInt32(Value * 255 * (1 - Saturation));
            int q = Convert.ToInt32(Value * 255 * (1 - f * Saturation));
            int t = Convert.ToInt32(Value * 255 * (1 - (1 - f) * Saturation));

            switch (hi)
            {
                case 0:
                    return Color.FromArgb(255, v, t, p);
                case 1:
                    return Color.FromArgb(255, q, v, p);
                case 2:
                    return Color.FromArgb(255, p, v, t);
                case 3:
                    return Color.FromArgb(255, p, q, v);
                case 4:
                    return Color.FromArgb(255, t, p, v);
                default:
                    return Color.FromArgb(255, v, p, q);
            }
        }

        public static HSVColor FromColor(Color c)
        {
            return new HSVColor(c);
        }
    }
}
