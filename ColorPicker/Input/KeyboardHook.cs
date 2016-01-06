using System;
using System.Collections.Generic;
using System.Text;

namespace ColorPicker.Input
{
    public class KeyboardHook : IHook
    {
        private IntPtr _Handle;
        public IntPtr Handle
        {
            get
            {
                return _Handle;
            }
        }

        private bool _hooked = false;
        public bool IsHooked
        {
            get
            {
                return _hooked;
            }
        }

        public KeyboardHook(IntPtr hwnd)
        {
            _Handle = hwnd;
        }

        public bool Hook()
        {
            return false;
        }

        public bool UnHook()
        {
            return false;
        }
    }
}
