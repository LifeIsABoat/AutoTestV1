using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    class ClickButtonIdExecutorByFixAllFirst : AbstractClickButtonIdExecutorMFCTP
    {
        protected override void click(ControlButtonIdentify targetButtonIdentify)
        {
            Screen currentScreen = StaticCurrentScreen.get();

            string expMsg = string.Format("Can't find [{0}] button in current screen.", targetButtonIdentify.btnWordsStr);

            ControlButton targetButton = currentScreen.findButton(targetButtonIdentify);
            if (null == targetButton)
                throw new FTBAutoTestException(expMsg);
            if (null == targetButton.imageList || 1 != targetButton.imageList.Count)
                throw new FTBAutoTestException(expMsg);

            if (null == targetButton.imageList[0].rect)
                throw new FTBAutoTestException(expMsg);
            Position targetPos = targetButton.imageList[0].rect.getCenter();
            tpClicker.execute(targetPos);
        }
    }
}
