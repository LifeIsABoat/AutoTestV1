using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Threading;

namespace Tool.Machine
{
    class ComponentTouchPanelMFCTPUseIO : AbstractComponentTouchPanelMFCTP
    {
        //*
        private AbstractComponentMachineIO io;

        //todo remove path
        public ComponentTouchPanelMFCTPUseIO(AbstractComponentMachineIO io)
        {
            this.io = io;
        }
        //*/
        public override void TPClick(Position position)
        {
            string clickCmd = generateTPCommand(MFCTPTpCode.TCHPNL_TYPE_PUSHED, position.x, position.y)
                             + generateTPCommand(MFCTPTpCode.TCHPNL_TYPE_RELEASED, position.x, position.y);
            io.write(clickCmd);
        }

        public override void LongTPPush(Position position)
        {
            string pushCmd = generateTPCommand(MFCTPTpCode.TCHPNL_TYPE_PUSHED, position.x, position.y);
            io.write(pushCmd);
            Thread.Sleep(50);
            for (int i = 0 ; i < 10; i++)
            {
                Thread.Sleep(200);
                string apushCmd = generateTPCommand(MFCTPTpCode.TCHPNL_TYPE_REPEAT, position.x, position.y);
                io.write(apushCmd);
            }
        }

        public override void TPPush(Position position)
        {
            string pushCmd = generateTPCommand(MFCTPTpCode.TCHPNL_TYPE_PUSHED, position.x, position.y);
            io.write(pushCmd);
        }
        public override void TPRelease(Position position)
        {
            string releaseCmd = generateTPCommand(MFCTPTpCode.TCHPNL_TYPE_RELEASED, position.x, position.y);
            io.write(releaseCmd);
        }
        private string generateTPCommand(string cmd, int x, int y)
        {
            string sendbuf;
            sendbuf = tpCmdTable[MFCTPTpCode.TCHPNL_HEAD_POINT] + tpCmdTable[cmd] + String.Format("{0:d3}{1:d3}", x, y);
            return sendbuf;
        }

    }//class TchpuielMFCTP

}
