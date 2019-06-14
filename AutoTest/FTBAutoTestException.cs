using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
   /*
    *  Description: user defined exception
    */
    class FTBAutoTestException : ApplicationException
    {
        public FTBAutoTestException(string message) : base(message) { }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
    }
}
