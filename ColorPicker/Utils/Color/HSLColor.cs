using System;
using System.Drawing;

namespace ColorPicker.Utils
{
    public struct HSLColor
    {
        public float Hue { get; set; }
        public float Saturation { get; set; }
        public float Lightness { get; set; }
        
        public HSLColor(float h, float s, float l)
        {
            Hue = h;
            Saturation = s;
            Lightness = l;
        }

        public HSLColor(Color c)
        {
            Hue = c.GetHue();
            Saturation = c.GetSaturation();
            Lightness = c.GetBrightness();
        }

        public Color ToColor()
        {
            if (0 == Saturation)
            {
                return Color.FromArgb(
                    Convert.ToByte(Lightness * 255),
                    Convert.ToByte(Lightness * 255), 
                    Convert.ToByte(Lightness * 255));
            }

            float fMax, fMid, fMin;
            int iSextant, iMax, iMid, iMin;
            float vHue = Hue;

            if (0.5 < Lightness)
            {
                fMax = Lightness - (Lightness * Saturation) + Saturation;
                fMin = Lightness + (Lightness * Saturation) - Saturation;
            }
            else {
                fMax = Lightness + (Lightness * Saturation);
                fMin = Lightness - (Lightness * Saturation);
            }

            iSextant = (int)Math.Floor(vHue / 60f);

            if (300f <= vHue)
            {
                vHue -= 360f;
            }

            vHue /= 60f;
            vHue -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);

            if (0 == iSextant % 2)
            {
                fMid = vHue * (fMax - fMin) + fMin;
            }
            else {
                fMid = fMin - vHue * (fMax - fMin);
            }

            iMax = Convert.ToInt32(fMax * 255);
            iMid = Convert.ToInt32(fMid * 255);
            iMin = Convert.ToInt32(fMin * 255);

            switch (iSextant)
            {
                case 1:
                    return Color.FromArgb(iMid, iMax, iMin);
                case 2:
                    return Color.FromArgb(iMin, iMax, iMid);
                case 3:
                    return Color.FromArgb(iMin, iMid, iMax);
                case 4:
                    return Color.FromArgb(iMid, iMin, iMax);
                case 5:
                    return Color.FromArgb(iMax, iMin, iMid);
                default:
                    return Color.FromArgb(iMax, iMid, iMin);
            }
        }

        public static HSLColor FromColor(Color c)
        {
            return new HSLColor(c);
        }
    }
}
