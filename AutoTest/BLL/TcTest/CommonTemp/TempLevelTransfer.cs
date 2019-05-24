using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class TempLevelTransfer : AbstractCmnTestHandler
    {
        //IFTBCommonAPI treeMemory;
        AbstractCmnTestHandler handler;

        public TempLevelTransfer(AbstractCmnTestHandler handler)
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
            this.handler = handler;
        }

        public override void execute()
        {
            IIterator levelIterator = treeMemory.createLevelIterator();

            int targetLevel = TestRuntimeAggregate.getCurrentLevelIndex();
            levelIterator.first();
            TestRuntimeAggregate.setCurrentLevelIndex(levelIterator.currentItem());
            
            for(int level = 0;level < targetLevel;level++)
            {
                levelIterator.next();
            }

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
