using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;

namespace Tool.BLL
{
    class TcManagerScreenCheck : AbstractTcManager
    {
        AbstractCmnTestHandler first;
        IScreenCommonAPI screenMemory;
        public TcManagerScreenCheck()
        {
            //import
            screenMemory = new ScreenMemoryCommonAggregate();
            string pathJson = StaticEnvironInfo.getSelectedStandardScreenFileName();
            screenMemory.importScreen(pathJson);
            TestRuntimeAggregate.setScreenMemory(screenMemory);

            List<AbstractChecker> checkList = new List<AbstractChecker>();
            checkList.Add(new Check.FtbScreenItemListChecker());
            //checkList.Add(new BLL.Check.FtbScreenTitleChecker());

            //AbstractCmnTestHandler toScreenByCondition = new ToScreenByCondition();
            AbstractCmnTestHandler toScreenByPath = new ToScreenByPath();
            //toScreenByCondition.setHandler(toScreenByPath);

            AbstractCmnTestHandler screenRunHandler = new ScreenTransfer(toScreenByPath);
            AbstractCmnTestHandler tcOpinionChecker = new TotalTCCmnTestOpinionChecker(checkList);
            screenRunHandler.setHandler(tcOpinionChecker);

            first = screenRunHandler;
        }
        public override void run()
        {
            first.execute();
        }
    }
}
