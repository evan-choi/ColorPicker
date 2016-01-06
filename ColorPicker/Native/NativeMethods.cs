using System;
using System.Runtime.InteropServices;

namespace ColorPicker.Native
{
    internal class NativeMethods
    {
        public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        #region [ dwmapi ]
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref NativeStructs.MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, NativeEnums.DWMWINDOWATTRIBUTE attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(out bool enabled);
        #endregion

        #region [ user32 ]
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(HandleRef hWnd, NativeEnums.WM Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, NativeEnums.WindowLongFlags nIndex, NativeEnums.WindowStyles dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern NativeEnums.WindowStyles GetWindowLong(IntPtr hWnd, NativeEnums.WindowLongFlags nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(NativeEnums.HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        #endregion

        #region [ Kernel ]

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);
        #endregion

    }
}
