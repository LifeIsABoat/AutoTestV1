using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBExcel2Script
{
   /*
    *  Description:Button Info
    */
    public class FTBButtonInfo : FTBButtionTitleInfo
    {
        public int condition_index { get; set; }
        public string next_scrn_id { get; set; }
        public int cur_Level { get; set; }
        public FTBButtonInfo(string us_words="", int condition_index = 0, string function_name = "", string string_id = "", string nextscrnid = "")
            : base(us_words, function_name, string_id)
        {
            this.condition_index = condition_index;
            this.next_scrn_id = nextscrnid;
        }
    }
}
