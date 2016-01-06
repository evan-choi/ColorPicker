using System;
using System.Collections.Generic;
using System.Text;

namespace ColorPicker.Input
{
    public interface IHook
    {
        IntPtr Handle { get; }
        bool IsHooked { get; }

        bool Hook();
        bool UnHook();
    }
}
