using ColorPicker.Native;

using System;
using System.Drawing;

namespace ColorPicker.Utils
{
    public static class ScreenCapture
    {
        public static Bitmap Capture(Rectangle area)
        {
            IntPtr hDest = IntPtr.Zero;
            
            IntPtr hdc = NativeMethods.GetDC(IntPtr.Zero);
            hDest = NativeMethods.CreateCompatibleDC(hdc);
                
            IntPtr hBitmap = NativeMethods.CreateCompatibleBitmap(hdc, area.Width, area.Height);

            IntPtr m_obit = NativeMethods.SelectObject(hDest, hBitmap);

            NativeMethods.BitBlt(hDest, 0, 0, area.Width, area.Height, hdc, area.X, area.Y, NativeEnums.TernaryRasterOperations.SRCCOPY);

            Bitmap img = Image.FromHbitmap(hBitmap);

            NativeMethods.ReleaseDC(IntPtr.Zero, hdc);
            NativeMethods.DeleteDC(hDest);
            NativeMethods.DeleteObject(hBitmap);

            // 메모리릭 임시 방편
            GC.Collect();

            return img;
        }
    }
}
