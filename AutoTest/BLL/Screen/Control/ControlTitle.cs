using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlTitle : AbstractControl
    {
        public ElementString str;

        public ControlTitle(ElementString str)
            :base(str.rect)
        {
            this.str = str;
        }

        public override void hasFixed()
        {
            if (null != str)
            {
                str.hasFixed();
            }
        }
        public override string ToString()
        {
            string str = "";

            str += "Title:";
            if (null == rect)
                str += "null,\r\n";
            else
                str += rect.ToString() + ",\r\n";
            str += "{";
            string tmpStr = "";
            tmpStr += "\r\n" + this.str.ToString();
            str += tmpStr.Replace("\r\n", "\r\n\t");
            str += "\r\n};";

            return str;
        }
        public override bool Equals(AbstractControl targetControl)
        {
            ControlTitle title = targetControl as ControlTitle;
            if (null == title)
                return false;

            if (null != str && !str.EqualsByAllAttribute(title.str))
                return false;
            if (null == str && null != title.str)
                return false;

            return base.Equals(targetControl);
        }
    }
}
