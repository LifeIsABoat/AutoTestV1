/*
 * File Name: ButtonIdentify.cs
 * Creat Date: 2017-3-24
 * Description: Button Identify Class
 * Modify Content:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tool.BLL
{
    /*
     * Description: String Info calss,include rectangle and string
     */
    class ElementString : AbstractElement
    {
        public string str;

        /* 
         *  Description:special constructor
         *  Param:rect - Rectangle of String Info
         *        str - String of String Info
         *  Return:
         *  Exception:
         *  Example:ButtonIdentify(btnWordsInfo)
         */
        public ElementString(string str,
                             ushort? id = null,
                             Rectangle rect = null)
            : base(id, rect)
        {
            this.str = str;
        }

        public override string ToString()
        {
            string str = "";

            str += "STR,";
            str += string.Format("0x{0:x4},", id) + fixedFlag + ",";
            str += rect.ToString() + ",";
            if (null == this.str)
                str += "null";
            else
                str +=  "\"" + this.str.Replace("\r\n", "\\r\\n") + "\"";
            str += ";";
            return str;
        }

        public override bool EqualsByAllAttribute(AbstractElement targetElement)
        {
            ElementString eleStr = targetElement as ElementString;
            if (null == eleStr)
                return false;
            if (str != eleStr.str)
            {
                if (str.Contains(",") && eleStr.str.Contains(","))
                {
                    str = str.Replace(" ", "");
                    string[] sArray = str.Split(',');
                    string a_Str = eleStr.str.Replace(" ", "");
                    string[] a_sArray = eleStr.str.Split(',');
                    foreach (string i in sArray)
                    {
                        foreach (string jkl in a_sArray)
                        {
                            string popResult = Regex.Replace(i, @"[^a-zA-Z0-9]+", "");
                            string meResult = Regex.Replace(jkl, @"[^a-zA-Z0-9]+", "");
                            if (popResult.Equals(meResult))
                                return true;
                        }
                    }
                }
                return false;
            }
            
            return base.EqualsByAllAttribute(targetElement);
        }
        public override bool EqualsBySingleAttribute(AbstractElement targetElement)
        {
            ElementString eleStr = targetElement as ElementString;
            if (null == eleStr)
                return false;
            if (null != str && str!= "" && str == eleStr.str)
                return true;
            return base.EqualsBySingleAttribute(targetElement);
        }
    }
}
