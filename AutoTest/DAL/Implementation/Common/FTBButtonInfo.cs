using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool.DAL
{
   /*
    *  Description:Button Info
    */
    public class ButtonInfo : LableInfo
    {       
        public Contidion ftbContidion{ get; set; }
           
        public ButtonInfo(string usWords,int conditionIndex=Contidion._NOCONDITION,string functionName="", string stringId="" )
            : base(usWords,functionName, stringId )
        {
            ftbContidion = new Contidion(conditionIndex);
        }
    }
}
