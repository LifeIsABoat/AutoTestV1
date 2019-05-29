using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Tool.Parser
{
    class INIParser
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defvalue, StringBuilder retvalue, int size, string filepath);
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileSection")]
        protected internal static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string lpFileName);
        public static void getKeyList(string section, string filepath, List<string> keylist)
        {
            byte[] retkeyvalue = new byte[32768];
            //get key
            int bufLen = GetPrivateProfileSection(section, retkeyvalue, retkeyvalue.GetUpperBound(0), filepath);
            if (bufLen > 0)
            {
                StringBuilder temp = new StringBuilder();
                for (int i = 0; i < bufLen; i++)
                {
                    if (retkeyvalue[i] != 0)
                    {
                        temp.Append((char)retkeyvalue[i]);
                    }
                    else
                    {
                        if (temp.Length > 0)
                        {
                            string tempkey = temp.ToString().Split('=')[0];
                            keylist.Add(tempkey);
                            temp = new StringBuilder();
                        }
                    }
                }
            }
        }
        public static string getvalue(string section, string filepath, string key)
        {
            string defvalue = "";
            StringBuilder retvalue = new StringBuilder();

            long flag = GetPrivateProfileString(section, key, defvalue, retvalue, 255, filepath);
            if (flag > 0)
            {
                return retvalue.ToString();
            }
            return "";
        }
    }
}
