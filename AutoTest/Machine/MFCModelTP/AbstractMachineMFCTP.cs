using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Tool.Parser;

namespace Tool.Machine
{
    abstract class AbstractMachineMFCTP
    {
        public AbstractComponentMachineIO io;
        public AbstractComponentKeyBoardMFCTP keyBoard;
        public AbstractComponentTouchPanelMFCTP touchPanel;
        public AbstractMachineMFCTP(AbstractComponentMachineIO io,
                                    AbstractComponentKeyBoardMFCTP keyBoard, 
                                    AbstractComponentTouchPanelMFCTP touchPanel,
                                    string machineConfig)
        {
            this.io = io;
            this.keyBoard = keyBoard;
            this.touchPanel = touchPanel;
            InitMachine(machineConfig);
        }

        void InitMachine(string iniFileName)
        {
            if (null == this.keyBoard || null == this.touchPanel)
                throw new FTBAutoTestException("InitMachine Failed by null keyBoard or null touchPanel.");
            if (!File.Exists(iniFileName))
                throw new FTBAutoTestException("InitMachine Failed by invalid iniFileDir.");

            List<string> keylist = new List<string>();
            INIParser.getKeyList("MFCKeyCode", iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                //get value
                string value = INIParser.getvalue("MFCKeyCode", iniFileName, keylist[i]);
                if (value != "")
                {
                    keyBoard.addHardKey(keylist[i], Convert.ToInt32(value));
                }
            }
            keylist.Clear();
            INIParser.getKeyList("TCHPNL_Type", iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                //get value
                string value = INIParser.getvalue("TCHPNL_Type", iniFileName, keylist[i]);
                if (value != "")
                {
                    touchPanel.addTouchCmd(keylist[i], value);
                }
            }
        }
    }
    public class MFCTPLogCode
    {
        public const string StartAutoTest =  "\xae";
    }
}
