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
            name = name.ToLower();
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

        public static HotKey GetHotKey(string name)
        {
            name = name.ToLower();

            if (hKeys.ContainsKey(name))
            {
                return hKeys[name];
            }

            return null;
        }

        public static void UnRegister(string name)
        {
            name = name.ToLower();

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
                if (hk.Enabled)
                {
                    ArrayList arr = new ArrayList(hk.Keys);
                    int cnt = (arr.Contains(Keys.None) ? 1 : 0);

                    foreach (Keys k in dKeys.Keys)
                    {
                        foreach (Keys vk in arr)
                        {
                            if (k == vk ||
                                (k.ToString().Contains("ShiftKey") && vk.ToString().Contains("ShiftKey")) ||
                                (k.ToString().Contains("ControlKey") && vk.ToString().Contains("ControlKey")) ||
                                (k.ToString().Contains("Menu") && vk.ToString().Contains("Menu")))
                            {
                                cnt += 1;
                                break;
                            }
                        }
                    }

                    if (cnt == arr.Count)
                    {
                        hk.Action?.Invoke();
                        return true;
                    }
                }
            }

            return false;
        }
    }
}