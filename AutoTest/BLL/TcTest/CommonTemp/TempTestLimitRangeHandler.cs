using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class TempTestLimitRangeHandler: AbstractCmnTestHandler
    {
        AbstractTcManager tcManger;
        public TempTestLimitRangeHandler(AbstractTcManager tcManger)
        {
            this.tcManger = tcManger;
        }
        public override void execute()
        {
            //LimitRange
            TempTcSort test = new TempTcSort(TestRuntimeAggregate.getTreeMemory());
            test.sort(tcManger);

            base.execute();
        }
    }
}
