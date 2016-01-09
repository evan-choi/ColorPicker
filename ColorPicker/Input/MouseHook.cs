using ColorPicker.Native;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ColorPicker.Input
{
    public class MouseHook : IHook
    {
        public event MouseMoveHandler OnMouseMove;
        public delegate void MouseMoveHandler(MouseHook sender, Point pt);
        
        private bool _hooked;
        public bool IsHooked
        {
            get
            {
                return _hooked;
            }
        }

        private Point _position;
        public Point LastPosition
        {
            get
            {
                return _position;
            }
        }

        private IntPtr mHookPtr;
        private NativeMethods.HookProc mHookProc;
        
        public bool Hook()
        {
            mHookProc = new NativeMethods.HookProc(_Hook);

            IntPtr module = NativeMethods.LoadLibrary("user32");

            mHookPtr = NativeMethods.SetWindowsHookEx(NativeEnums.HookType.WH_MOUSE_LL, mHookProc, module, 0);

            if (mHookPtr.ToInt32() != 0)
            {
                _hooked = true;
            }

            return _hooked;
        }

        private IntPtr _Hook(int code, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                if (code >= 0 && (NativeEnums.MouseMessages)wParam == NativeEnums.MouseMessages.WM_MOUSEMOVE)
                {
                    var ms = (NativeStructs.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeStructs.MSLLHOOKSTRUCT));

                    _position = new Point(ms.pt.x, ms.pt.y);
                    OnMouseMove?.Invoke(this, _position);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return NativeMethods.CallNextHookEx(mHookPtr, code, wParam, lParam);
        }

        public bool UnHook()
        {
            if (_hooked)
            {
                if (NativeMethods.UnhookWindowsHookEx(mHookPtr))
                {
                    _hooked = false;
                }
            }

            return !_hooked;
        }
    }
}
