using System;
using System.Windows.Forms;
using ColorPicker.Native;

namespace ColorPicker.Windows.Base
{
    public class DropShadowWindow : Form
    {
        private bool aero;

        public int Deapth { get; set; } = 1;
        public bool DropShadowEnable { get; set; } = true;

        public DropShadowWindow()
        {
            aero = IsAeroEnabled();
        }
        
        protected override void WndProc(ref Message m)
        {
            if (aero && DropShadowEnable && (m.Msg == NativeConstants.WM_NCPAINT))
            {
                int attr = 2;
                NativeMethods.DwmSetWindowAttribute(this.Handle, NativeEnums.DWMWINDOWATTRIBUTE.NCRenderingPolicy, ref attr, 4);

                var margin = new NativeStructs.MARGINS
                {
                    bottomHeight = Deapth,
                    leftWidth = Deapth,
                    rightWidth = Deapth,
                    topHeight = Deapth
                };

                NativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margin);
            }

            base.WndProc(ref m);
        }

        protected bool IsAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                bool result;
                NativeMethods.DwmIsCompositionEnabled(out result);

                return result;
            }

            return false;
        }
    }
}