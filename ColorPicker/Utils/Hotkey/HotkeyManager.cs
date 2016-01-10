using ColorPicker.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ColorPicker.Utils.Hotkey
{
    public delegate void HotKeyEvent();

    public static class HotkeyManager
    {
        private static KeyboardHook kHook;
        private static Dictionary<Keys, DateTime> dKeys;
        private static Dictionary<string, HotKey> hKeys;

        static HotkeyManager()
        {
            dKeys = new Dictionary<Keys, DateTime>();
            hKeys = new Dictionary<string, HotKey>();
            kHook = new KeyboardHook();

            kHook.OnKeyDown += KHook_OnKeyDown;
            kHook.OnKeyUp += KHook_OnKeyUp;

            kHook.Hook();
        }

        public static void Register(string name, HotKey hk)
        {
            hk.Name = name;

            if (!hKeys.ContainsKey(name))
            {
                hKeys.Add(name, hk);
            }
            else
            {
                hKeys[name] = hk;
            }
        }

        public static void UnRegister(string name)
        {
            if (hKeys.ContainsKey(name))
            {
                hKeys.Remove(name);
            }
        }

        private static void KHook_OnKeyUp(KeyboardHook sender, Keys key)
        {
            if (dKeys.ContainsKey(key))
            {
                dKeys.Remove(key);
            }
        }

        private static void KHook_OnKeyDown(KeyboardHook sender, Keys key, ref bool throwInput)
        {
            if (!dKeys.ContainsKey(key))
            {
                dKeys.Add(key, DateTime.Now);
            }

            if (CheckAvailableHotKey())
            {
                throwInput = true;
            }
        }

        private static bool CheckAvailableHotKey()
        {
            foreach (HotKey hk in hKeys.Values)
            {
                ArrayList arr = new ArrayList(hk.Keys);
                int cnt = 0;

                foreach (Keys k in dKeys.Keys)
                {
                    cnt += (arr.Contains(k) ? 1 : 0);
                }

                if (cnt == arr.Count)
                {
                    hk.Action?.Invoke();
                    return true;
                }
            }

            return false;
        }
    }
}