using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Machine;
using Tool.BLL;

namespace Tool.Command
{
    class ClickMachineTPExecutorMFCTPByIO : AbstractClickMachineTPExecutorMFCTP
    {
        private AbstractComponentMachineIO io;
        private AbstractComponentTouchPanelMFCTP touchPanel;
        public ClickMachineTPExecutorMFCTPByIO(AbstractMachineMFCTP machine)
        {
            this.io = machine.io;
            touchPanel = machine.touchPanel;
        }
        /*
         *  Description: touchpanel click
         *  Param: Position-coordinate 
         *  Return: 
         *  Exception: 
         *  Example: click(new Position(135, 107))
         */
        protected override void click(Position pos)
        {
            if (StaticEnvironInfo.isTPBvboardTested() == true)
            {
                touchPanel.TPClick(pos);
                System.Threading.Thread.Sleep(1600);
            }
            else
            {
                //Open Log Reader
                if (io.isReadOFF())
                    io.readON();

                //Load Standard Screen
                LogScreenChangeChecker.load();

                //Click Key
                touchPanel.TPClick(pos);

                //Check Screen Change
                LogScreenChangeChecker.check(600);

                //Close Log Reader
                if (io.isReadON())
                    io.readOFF();
            }
        }
    }
}
