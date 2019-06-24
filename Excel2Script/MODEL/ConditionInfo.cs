using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTBExcel2Script
{
    /*
     *  Description:condition Info
     */
    class FTBConditionInfo
    {   
        
        public List<string> conditions_list { get; set; }
        public FTBConditionInfo()
        {
            conditions_list = new List<string>();
        }
    }
}
