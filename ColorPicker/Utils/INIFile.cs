using static ColorPicker.Native.NativeMethods;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ColorPicker.Utils
{
    public class INIFile
    {
        public FileInfo FileInfo;

        public INIFile(string fileName)
        {
            this.FileInfo = new FileInfo(fileName);

            if (!FileInfo.Exists)
            {
                FileInfo.Create().Close();
            }
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
            return WritePrivateProfileString(category, property, value.ToString(), FileInfo.FullName);
        }

        private string GetValue(string category, string property, string defaultValue = "")
        {
            StringBuilder v = new StringBuilder(255);

            GetPrivateProfileString(category, property, defaultValue, v, (uint)v.Capacity, FileInfo.FullName);

            return v.ToString();
        }
    }
}