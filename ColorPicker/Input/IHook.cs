using System;
using System.Collections.Generic;
using System.Text;

namespace ColorPicker.Input
{
    public interface IHook
    {
        bool IsHooked { get; }

        bool Hook();
        bool UnHook();
    }
}
