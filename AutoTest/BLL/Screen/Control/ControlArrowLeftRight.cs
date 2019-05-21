using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlArrowLeftRight : AbstractControlArrow
    {
        public ControlArrowLeftRight(List<ControlButton> arrowList)
            :base(arrowList)
        {

        }

        public override AbstractScreenAggregate createAggregate()
        {
            if (StaticCurrentScreen.get().getIdentify().scrId == AbstractControlArrow.homeScrId)
            {
                return new ScreenCircularDoublyLinkedList();
            }
            else
            {
                return new ScreenDoublyLinkedList();
            }
        }

        public override string ToString()
        {
            string str = "";

            str += "ArrowLeftRight:{";
            if (null == rect)
                str += "null,\r\n";
            else
                str += rect.ToString() + ",\r\n";
            str += "{";
            string tmpStr = "";
            foreach (ControlButton arrow in arrowList)
                tmpStr += "\r\n" + arrow.ToString();
            str += tmpStr.Replace("\r\n", "\r\n\t");
            str += "\r\n};";

            return str;
        }
    }
}
