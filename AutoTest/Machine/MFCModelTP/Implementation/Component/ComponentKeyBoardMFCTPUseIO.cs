using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;

namespace Tool.Machine
{
    class ComponentKeyBoardMFCTPUseIO : AbstractComponentKeyBoardMFCTP
    {
        private AbstractComponentMachineIO io;

        public ComponentKeyBoardMFCTPUseIO(AbstractComponentMachineIO io)
        {
            this.io = io;
        }
        
        //send key command to MFC
        public override void sendKey(string code)
        {
            if (!keyCmdDt.ContainsKey(code))
            {
                throw new FTBAutoTestException("TargetKey[" + code + "] is invalid.");
            }
            string sendbuf = ((char)keyCmdDt[code]).ToString();
            io.write(sendbuf);
        }
    }//class KeyBoardMFCTP
}
