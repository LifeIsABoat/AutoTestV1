using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Specialized;

namespace ColorDivisionTool.Operation
{
    public class IniOperation
    {
        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        internal static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        [DllImport("kernel32")]
        internal static extern bool WritePrivateProfileString(string section, string key, string sValue, string filePath);

        /// <summary>
        /// 获取指定 Key 的值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="iniFilePath"></param>
        /// <returns></returns>
        public static string GetSpecifyValue(string section, string key, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                byte[] temp = new byte[1024];
                GetPrivateProfileString(section, key, "", temp, 1024, iniFilePath);
                return Encoding.GetEncoding(0).GetString(temp, 0, temp.ToList().FindIndex(0, c => c == 0));
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 获取指定 Section 所有的 Key
        /// </summary>
        /// <param name="section"></param>
        /// <param name="iniFilePath"></param>
        public static List<string> GetSpecifySectionKeys(string section, string iniFilePath)
        {
            byte[] buffer = new byte[5000];
            int readLen = GetPrivateProfileString(section, null, null, buffer, 5000, iniFilePath);
            return GetStringsFromBuffer(buffer, readLen);
        }
        /// <summary>
        /// 获取指定 Section 所有的 Value
        /// </summary>
        /// <param name="section"></param>
        /// <param name="iniFilePath"></param>
        /// <returns></returns>
        public static List<string> GetSpecifySectionValues(string section, string iniFilePath)
        {
            List<string> keyValue = new List<string>();
            List<string> keys = GetSpecifySectionKeys(section, iniFilePath);
            if (keys == null)
                return null;
            foreach (string e in keys)
            {
                keyValue.Add(GetSpecifyValue(section, e, iniFilePath));
            }
            return keyValue;
        }

        private static List<string> GetStringsFromBuffer(byte[] buffer, int bufLen)
        {
            List<string> data = new List<string>();
            if (bufLen != 0)
            {
                int start = 0;
                int end = -1;
                while (end < bufLen)
                {
                    end = buffer.ToList().FindIndex(start, c => c == 0);
                    if (end > start)
                        data.Add(Encoding.GetEncoding(0).GetString(buffer, start, end - start));
                    start = end + 1;
                }
            }
            return data;
        }
    }
}
