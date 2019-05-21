using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class LogControl
    {
        public const string _BTNTYPE = "BTN", _STRTYPE = "STR";
        public const string _SELECTEDSTAT = "SN", _VALIDSTAT = "N", _INVALIDSTAT1 = "G", _INVALIDSTAT2 = "SG";

        public UInt16 layerID { get; set; }
        public UInt16 dataHolderID { get; set; }
        public string ctl_type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string btnStatus { get; set; }
        public UInt16 istrHMIid { get; set; }
        public string text { get; set; }

        public Rectangle getRect()
        {
            return new Rectangle(x, y, w, h);
        }
    }
}
