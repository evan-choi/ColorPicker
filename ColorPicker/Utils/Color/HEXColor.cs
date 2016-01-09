using System;
using System.Drawing;

namespace ColorPicker.Utils
{
    public struct HEXColor : IColor
    {
        public string Html { get; set; }

        public ColorType Type
        {
            get
            {
                return ColorType.HEX;
            }
        }

        public HEXColor(Color c)
        {
            Html = ColorTranslator.ToHtml(c);
        }

        public static HEXColor FromColor(Color c)
        {
            return new HEXColor(c);
        }

        public Color ToColor()
        {
            return ColorTranslator.FromHtml(Html);
        }

        public override string ToString()
        {
            return Html;
        }
    }
}