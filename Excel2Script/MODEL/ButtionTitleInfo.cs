using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBExcel2Script
{
    /*
     *  Description:FTBButtion Title Info
     */
    public class FTBButtionTitleInfo
    {
        public string function_name { get; set; }
        public string us_words { get; set; }
        public string string_id { get; set; }
        public string cur_scrn_id { get; set; }

        public FTBButtionTitleInfo(string usWords="", string functionName = "", string stringId = "", string curscrnid = "")
        {
            this.function_name = functionName;
            this.string_id = stringId;
            this.us_words = usWords;
            this.cur_scrn_id = curscrnid;
        }
    }
}
