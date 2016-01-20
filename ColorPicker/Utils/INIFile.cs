using static ColorPicker.Native.NativeMethods;

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Security.Permissions;

namespace ColorPicker.Utils
{
    public class INIFile
    {
        public FileInfo FileInfo;
        public DateTime LastUpdate;

        public INIFile(string fileName)
        {
            this.FileInfo = new FileInfo(fileName);

            if (!FileInfo.Exists)
            {
                FileInfo.Create().Close();
            }

            LastUpdate = DateTime.Now;
        }

        public T GetValue<T>(string category, string property, T defaultValue = default(T))
        {
            T v = default(T);

            try
            {
                v = (T)Convert.ChangeType(GetValue(category, property, defaultValue.ToString()), typeof(T));
            }
            catch
            {
            }

            return v;
        }

        public bool SetValue<T>(string category, string property, T value)
        {
            bool r = WritePrivateProfileString(category, property, value.ToString(), FileInfo.FullName);
            
            LastUpdate = DateTime.Now;

            return r;
        }

        private string GetValue(string category, string property, string defaultValue = "")
        {
            StringBuilder v = new StringBuilder(255 * 255);
            GetPrivateProfileString(category, property, defaultValue, v, (uint)v.Capacity, FileInfo.FullName);

            LastUpdate = DateTime.Now;

            return v.ToString();
        }
    }
}