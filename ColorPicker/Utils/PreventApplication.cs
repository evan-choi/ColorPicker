using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ColorPicker.Utils
{
    public static class PreventApplication
    {
        private static Mutex mutex;

        public static bool Register(string name)
        {
            bool created = true;

            if (mutex == null)
            {
                mutex = new Mutex(true, name, out created);
            }

            return created;
        }
        
        public static void UnRegister()
        {
            mutex?.ReleaseMutex();
            mutex?.Close();
            mutex = null;
        }
    }
}
