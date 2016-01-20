using static ColorPicker.Native.NativeEnums;
using static ColorPicker.Native.NativeMethods;

using System.Windows.Forms;
using System;

namespace ColorPicker.Utils
{
    public static class ListViewHelper
    {        
        public static void SetExtendedStyle(Control control, Native.NativeEnums.ListViewExtendedStyles exStyle)
        {
            ListViewExtendedStyles styles;

            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, IntPtr.Zero, IntPtr.Zero);
            styles |= exStyle;

            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, IntPtr.Zero, new IntPtr((int)styles));
        }

        public static void EnableDoubleBuffer(Control control)
        {
            ListViewExtendedStyles styles;

            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, IntPtr.Zero, IntPtr.Zero);
            styles |= ListViewExtendedStyles.DoubleBuffer | ListViewExtendedStyles.BorderSelect;

            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, IntPtr.Zero, new IntPtr((int)styles));
        }
        public static void DisableDoubleBuffer(Control control)
        {
            ListViewExtendedStyles styles;

            styles = (ListViewExtendedStyles)SendMessage(control.Handle, (int)ListViewMessages.GetExtendedStyle, IntPtr.Zero, IntPtr.Zero);
            styles -= styles & ListViewExtendedStyles.DoubleBuffer;
            styles -= styles & ListViewExtendedStyles.BorderSelect;

            SendMessage(control.Handle, (int)ListViewMessages.SetExtendedStyle, IntPtr.Zero, new IntPtr((int)styles));
        }
    }
}
