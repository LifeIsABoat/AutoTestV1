using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Tool.DAL
{
   /*
    *  Description:Option Info
    */
    public class OptionInfo : ButtonInfo
    {
        public string comment { get; set; }
        public string factorySetting { get; set; }
        public Mcc ftbMcc{ get; set; }
        public string rsp { get; set; }
        public string ews { get; set; }

        public OptionInfo( string usWords, int conditionIndex, BigInteger tcMcc, string comment="", string factorySetting="",string functionName="", string stringId="", string rsp = "", string ews = "")
            : base( usWords, conditionIndex,functionName, stringId)
        {
            ftbMcc = new Mcc(tcMcc);
            this.comment = comment;
            this.factorySetting = factorySetting;
            this.rsp = rsp;
            this.ews = ews;
        }
    }
}
