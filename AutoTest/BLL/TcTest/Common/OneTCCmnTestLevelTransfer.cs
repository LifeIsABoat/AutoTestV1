using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;

namespace Tool.BLL
{
    class OneTCCmnTestLevelTransfer : AbstractCmnTestHandler
    {
        //从Home一直迁移到最下层画面的链
        AbstractCmnTestHandler handler;

        public OneTCCmnTestLevelTransfer(AbstractCmnTestHandler handler)
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
            this.handler = handler;
        }

        public override void execute()
        {
            IIterator levelIterator = treeMemory.createLevelIterator();
            levelIterator.first();
            TestRuntimeAggregate.setCurrentLevelIndex(levelIterator.currentItem());
            TestRuntimeAggregate.setCurrentTCStatus(TestOneTCStatus.Transfering);

            for (levelIterator.next(); !levelIterator.isDone(); levelIterator.next())
            {
                TestRuntimeAggregate.setCurrentLevelIndex(levelIterator.currentItem());
                if (treeMemory.isLevelOption(levelIterator.currentItem()) == true)
                {
                    break;
                }
                //do level condition
                //doLevelConditon();
                handler.execute();
            }
            base.execute();
        }
    }
}
