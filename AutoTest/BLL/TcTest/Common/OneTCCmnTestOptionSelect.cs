using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;

namespace Tool.BLL
{
    class OneTCCmnTestOptionSelect : AbstractCmnTestHandler
    {
        AbstractCmnTestHandler handler;
        public OneTCCmnTestOptionSelect(AbstractCmnTestHandler handler)
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
            this.handler = handler;
        }
        public override void execute()
        {        
            ControlButton ftbButton = loadFTBButtonControl(TestRuntimeAggregate.getCurrentLevelIndex());
            if (ftbButton.getIdentify().isValid() == false)
            {
                //ok
                return;
            }
            //do level condition
            //doLevelConditon();
            handler.execute();

            base.execute();
        }
    }
}
