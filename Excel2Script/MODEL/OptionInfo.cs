using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBExcel2Script
{
    /*
    *  Description:Option Info
    */
    public class FTBOptionInfo : FTBButtonInfo
    {
        public string comment { get; set; }
        public string factory_setting { get; set; }
        public string tc_mcc { get; set; }
        public string rsp { get; set; }
        public string ews { get; set; }

        public FTBOptionInfo(string usWords, int conditionIndex, string tcMcc, string comment = "", string factorySetting = "", string functionName = "", string stringId = "")
            : base(usWords, conditionIndex, functionName, stringId)
        {
            this.tc_mcc = tcMcc;
            this.comment = comment;
            this.factory_setting = factorySetting;
        }
        public FTBOptionInfo(string usWords="", int conditionIndex=0, string comment = "", string factorySetting = "", string functionName = "", string stringId = "")
            : base(usWords, conditionIndex, functionName, stringId)
        {
            this.tc_mcc = tc_mcc;
            this.comment = comment;
            this.factory_setting = factorySetting;
        }
    }
}
