using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tool.Machine;
using Tool.BLL;

namespace Tool.Command
{
    class ClickMachineKeyExecutorMFCTPByIO : AbstractClickMachineKeyExecutorMFCTP
    {
        private AbstractComponentMachineIO io;
        private AbstractComponentKeyBoardMFCTP keyBoard;
        
        public ClickMachineKeyExecutorMFCTPByIO(AbstractMachineMFCTP machine)
        {
            this.io = machine.io;
            keyBoard = machine.keyBoard;
        }

        /*
         *  Description: Keyboard Click
         *  Param: string -keyboard command
         *  Return: 
         *  Exception: 
         *  Example: click(mKeyBoardIO.TEN5_KEY)
         */
        protected override void click(string keyCode) 
        {
            if (StaticEnvironInfo.isTPBvboardTested() == true)
            {
                keyBoard.sendKey(keyCode);
                System.Threading.Thread.Sleep(750);
            }
            else
            {
                //Open Log Reader
                if (io.isReadOFF())
                    io.readON();

                //Load Standard Screen
                LogScreenChangeChecker.load();

                //Click Key
                keyBoard.sendKey(keyCode);

                //Check Screen Change
                LogScreenChangeChecker.check(600);

                //Close Log Reader
                if (io.isReadON())
                    io.readOFF();
            }
        }
    }

}
