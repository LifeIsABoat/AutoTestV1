using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBExcel2Script
{
    /*
    *  Description:FTB on sheet all data
    */
    class FTBOneSheetInfo
    {
        public FTBMccInfo ftb_mcc_attribute { get; set; }
        public FTBConditionInfo ftb_condition_attribute { get; set; }
        public LevelNode levelnode { get; set; }
        public FTBOneSheetInfo(FTBMccInfo ftbMccAttribute, FTBConditionInfo ftbConditionAttribute, LevelNode levelnode)
        {
            this.ftb_condition_attribute = ftbConditionAttribute;
            this.ftb_mcc_attribute = ftbMccAttribute;
            this.levelnode = levelnode;
        }
    }
}
