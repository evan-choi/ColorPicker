using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ColorPicker.Utils
{
    public interface IColor
    {
        Color ToColor();
        ColorType Type { get; }
    }
}
