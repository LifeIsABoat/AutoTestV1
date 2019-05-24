using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class TotalTCCmnTestLimitRangeHandler : AbstractCmnTestHandler
    {
        AbstractTcManager tcManger;
        public TotalTCCmnTestLimitRangeHandler(AbstractTcManager tcManger)
        {
            this.tcManger = tcManger;
        }
        public override void execute()
        {
            //LimitRange
            TcSort test = new TcSort(TestRuntimeAggregate.getTreeMemory());
            test.sort(tcManger);

            base.execute();
        }
    }
}
