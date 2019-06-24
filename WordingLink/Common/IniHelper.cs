using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WordingLink
{
    public class IniHelper
    {
        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        internal static extern long GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        internal static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// 获取指定 Key 的值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="iniFilePath"></param>
        /// <returns></returns>
        public static string GetKeyValue(string filePath, string section, string key)
        {
            if (File.Exists(filePath))
            {
                byte[] temp = new byte[1024];
                GetPrivateProfileString(section, key, "", temp, 1024, filePath);
                return Encoding.GetEncoding(0).GetString(temp, 0, temp.ToList().FindIndex(0, c => c == 0));
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 写指定 Key 的值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="iniFilePath"></param>
        /// <returns></returns>
        public static bool SetKeyValue(string filePath, string section, string key, string val)
        {
            if (File.Exists(filePath))
            {
                WritePrivateProfileString(section, key, val, filePath);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
