using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool.DAL
{
   /*
    *  Description:FTB LableInfo
    */
    public class LableInfo
    {
        public string functionName { get; set; }
        public string usWords { get; set; }
        public string stringId { get; set; }

        public LableInfo(string usWords,string functionName="", string stringId="" )
        {
            this.functionName = functionName;
            this.stringId = stringId;
            this.usWords = usWords;
        }
    }
}
