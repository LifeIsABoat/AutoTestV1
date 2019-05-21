using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlLabel : AbstractControl
    {
        public ElementString str;

        public ControlLabel(ElementString str)
            :base()
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

            str += "Label:";
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
            ControlLabel label = targetControl as ControlLabel;
            if (null == label)
                return false;

            if (null != str)
                return str.EqualsByAllAttribute(label.str);
            else if (null != label.str)
                return false;

            return base.Equals(targetControl);
        }
    }
}
