using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FTBExcel2Script
{
    class InitHelper
    {
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpstring, string lpFileName);
        [DllImport("kernel32.DLL")]
        private static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        private string filePath;

        public InitHelper(String filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// iniファイル中のセクションのキーを指定して、文字列を返す
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string section, string key)
        {
            StringBuilder sb = new StringBuilder(1024);

            GetPrivateProfileString(section, key, "", sb, Convert.ToInt32(sb.Capacity), filePath);

            return sb.ToString();
        }

        /// <summary>
        /// iniファイル中のセクションのキーを指定して、整数値を返す
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetInt(string section, string key)
        {
            return GetPrivateProfileInt(section, key, 0, filePath);
        }

        /// <summary>
        /// 指定したセクション、キーに数値を書き込む
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Set(string section, string key, int val)
        {
            Set(section, key, val.ToString());
        }

        /// <summary>
        /// 指定したセクション、キーに文字列を書き込む
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Set(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, filePath);
        }
    }
}
