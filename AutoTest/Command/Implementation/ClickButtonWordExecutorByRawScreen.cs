using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    class ClickButtonWordExecutorByRawScreen : AbstractClickButtonWordExecutorMFCTP
    {
        protected override void click(string targetButtonWords)
        {
            Screen currentScreen = StaticCurrentScreen.get();

            string expMsg = string.Format("Can't find [{0}] button in current screen.", targetButtonWords);

            ElementString buttonStringElement = new ElementString(targetButtonWords);
            AbstractElement targetStringElement = currentScreen.findElement(buttonStringElement);
            if (null == targetStringElement)
                throw new FTBAutoTestException(expMsg);
            List<AbstractElement> targetImageElementList = currentScreen.getElementShipValueList(targetStringElement);
            if (null == targetImageElementList || 1 != targetImageElementList.Count)
                throw new FTBAutoTestException(expMsg);
            AbstractElement targetImageElement = targetImageElementList[0];

            if (null == targetImageElement.rect)
                throw new FTBAutoTestException(expMsg);
            Position targetPos = targetImageElement.rect.getCenter();
            tpClicker.execute(targetPos);
        }
    }
}
