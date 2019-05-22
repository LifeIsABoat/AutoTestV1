using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ElementImage : AbstractElement
    {
        public ulong? hashValue;

        public ElementImage(Rectangle rect, 
            ushort? id = null , 
            ulong? hashValue = null)
            : base(id, rect)
        {
            this.hashValue = hashValue;
        }

        public override string ToString()
        {
            string str = "";

            str += "IMG,";
            str += string.Format("0x{0:x4},", id) + fixedFlag + ",";
            str += rect.ToString() + ",";
            if (null == hashValue)
                str += "null";
            else
                str += string.Format("0x{0:h16}", hashValue);
            str += ";";

            return str;
        }

        public override bool EqualsByAllAttribute(AbstractElement targetElement)
        {
            ElementImage eleImg = targetElement as ElementImage;
            if (null == eleImg)
                return false;
            if (hashValue != eleImg.hashValue)
                return false;
            return base.EqualsByAllAttribute(targetElement);
        }
        public override bool EqualsBySingleAttribute(AbstractElement targetElement)
        {
            ElementImage eleImg = targetElement as ElementImage;
            if (null == eleImg)
                return false;
            if (hashValue!= null && hashValue == eleImg.hashValue)
                return true;
            return base.EqualsBySingleAttribute(targetElement);
        }
    }
}
