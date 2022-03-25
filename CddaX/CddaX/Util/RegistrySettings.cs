using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Win32;

namespace CddaX.Util
{
    public class RegistrySettings : Component
    {
        [Description("Name of the subkey below HKCU\\Software"), Category("Registry")]
        public string SoftwareName { get; set; }

        private string FullKey
        {
            get
            {
                return string.Format("HKEY_CURRENT_USER\\Software\\{0}", SoftwareName);
            }
        }

        public int LoadInt(string name, int defaultValue)
        {
            object v = Registry.GetValue(FullKey, name, null);
            if (v is int)
            {
                return (int)v;
            }
            else
            {
                return defaultValue;
            }
        }

        public void SaveInt(string name, int value)
        {
            Registry.SetValue(FullKey, name, value);
        }

        public bool LoadBool(string name, bool defaultValue)
        {
            return LoadInt(name, defaultValue ? 1 : 0) == 1;
        }

        public void SaveBool(string name, bool value)
        {
            SaveInt(name, value ? 1 : 0);
        }

        public string LoadString(string name, string defaultValue)
        {
            object v = Registry.GetValue(FullKey, name, null);
            if (v != null)
            {
                string s = v.ToString();
                if (!string.IsNullOrEmpty(s))
                {
                    return s;
                }
            }

            return defaultValue;
        }

        public void SaveString(string name, string value)
        {
            Registry.SetValue(FullKey, name, value);
        }

        public T LoadEnum<T>(string name, T defaultValue) where T: struct, IConvertible
        {
            int v = LoadInt(name, defaultValue.ToInt32(null));
            if (Enum.IsDefined(typeof(T), v))
                return (T)Enum.ToObject(typeof(T), v);
            else
                return defaultValue;
        }

        public void SaveEnum<T>(string name, T value) where T : struct, IConvertible
        {
            SaveInt(name, value.ToInt32(null));
        }

        public void RemoveAllSettings()
        {
            Registry.CurrentUser.DeleteSubKeyTree("Software\\" + SoftwareName, false);
        }
    }
}
