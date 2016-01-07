using System.Drawing;

namespace ColorPicker.Utils
{
    public struct HEXColor
    {
        public string Html { get; set; }

        public HEXColor(Color c)
        {
            Html = $"{c.R:X2}{c.G:X2}{c.B:X2}";
        }

        public static HEXColor FromColor(Color c)
        {
            return new HEXColor(c);
        }
    }
}
