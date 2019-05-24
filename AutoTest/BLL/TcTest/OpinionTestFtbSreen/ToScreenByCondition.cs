using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tool.DAL;
using Tool.Command;
using System.Web.Script.Serialization;
namespace Tool.BLL
{
    class ToScreenByCondition : AbstractCmnTestHandler
    {
        public ToScreenByCondition()
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
            this.screenMemory = TestRuntimeAggregate.getScreenMemory();
            //this.ScreenIterator = screenMemory.createScreenIterator();
        }
        public override void execute()
        {
            int currentIndex = TestRuntimeAggregate.getCurrentTcIndex();

            List<string> condition = screenMemory.getScreenCondition(currentIndex);

            for (int i=0;i< condition.Count;i++)
            {
                int TcIndex = treeMemory.getTcIndexFromConditionString(condition[i]);

                if (-1 == TcIndex)
                    continue;
                //click
                clickButton(TcIndex);
                StaticCommandExecutorList.get(CommandList.move_r).execute();
            }

            base.execute();
        }
    }
}
