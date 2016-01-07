using ColorPicker.Native;
using System.Drawing;

namespace ColorPicker.Input
{
    public static class Mouse
    {
        public static Point Position
        {
            get
            {
                NativeStructs.POINT pt = new NativeStructs.POINT();

                NativeMethods.GetCursorPos(out pt);

                return new Point(pt.x, pt.y);
            }
        }
    }
}
