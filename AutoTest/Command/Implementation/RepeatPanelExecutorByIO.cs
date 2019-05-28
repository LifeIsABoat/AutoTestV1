using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Machine;
using Tool.BLL;
using System.Threading;

namespace Tool.Command
{
    class RepeatPanelExecutorByIO : AbstractRepeatPanelExecutorMFCTP
    {
        private AbstractComponentMachineIO io;
        private AbstractComponentTouchPanelMFCTP touchPanel;
        public RepeatPanelExecutorByIO(AbstractMachineMFCTP machine)
        {
            this.io = machine.io;
            touchPanel = machine.touchPanel;
        }

        public override void longClick(Position pos)
        {
            if (StaticEnvironInfo.isTPBvboardTested() == true)
            {
                //push and repeat
                touchPanel.LongTPPush(pos);
                Thread.Sleep(100);
                touchPanel.TPRelease(pos);
            }
            else
            {
                //Open Log
                if (io.isReadOFF())
                    io.readON();

                //Load Standard Screen
                LogScreenChangeChecker.load();

                //push and repeat
                touchPanel.LongTPPush(pos);

                Thread.Sleep(100);

                touchPanel.TPRelease(pos);

                //Close Log Reader
                if (io.isReadON())
                    io.readOFF();
            }
        }
    }
}
