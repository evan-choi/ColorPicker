using ColorPicker.Native;

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ColorPicker.Input
{
    public class KeyboardHook : IHook
    {
        public event KeyDownHandler OnKeyDown;
        public delegate void KeyDownHandler(KeyboardHook sender, Keys key);

        public event KeyUpHandler OnKeyUp;
        public delegate void KeyUpHandler(KeyboardHook sender, Keys key);

        private bool _hooked = false;
        public bool IsHooked
        {
            get
            {
                return _hooked;
            }
        }

        private IntPtr mHookPtr;
        private NativeMethods.HookProc mHookProc;
        
        public bool Hook()
        {
            mHookProc = new NativeMethods.HookProc(_Hook);

            IntPtr module = NativeMethods.LoadLibrary("user32");
            
            mHookPtr = NativeMethods.SetWindowsHookEx(NativeEnums.HookType.WH_KEYBOARD_LL, mHookProc, module, 0);

            if (mHookPtr.ToInt32() != 0)
            {
                _hooked = true;
            }

            return _hooked;
        }

        private IntPtr _Hook(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                var kb = (NativeStructs.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(NativeStructs.KBDLLHOOKSTRUCT));

                if (kb.flags == NativeEnums.KBDLLHOOKSTRUCTFlags.LLKHF_DOWN)
                {
                    OnKeyDown?.Invoke(this, (Keys)kb.vkCode);
                }
                else
                {
                    OnKeyUp?.Invoke(this, (Keys)kb.vkCode);
                }
            }

            return NativeMethods.CallNextHookEx(mHookPtr, code, wParam, lParam);
        }

        public bool UnHook()
        {
            return false;
        }
    }
}
