using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Tool.Machine
{
    class MachineMFCTPUseIO:AbstractMachineMFCTP
    {
        public MachineMFCTPUseIO(AbstractComponentMachineIO io, 
                                 AbstractComponentKeyBoardMFCTP keyBoardMFCTP, 
                                 AbstractComponentTouchPanelMFCTP touchPanelMFCTP,
                                 string machineConfig)
            :base(io, keyBoardMFCTP, touchPanelMFCTP,machineConfig)
        {
        }
    }//class MachineMFCTP
    
}//namespace
