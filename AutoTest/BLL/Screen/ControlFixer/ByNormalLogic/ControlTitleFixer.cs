using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlTitleFixer : AbstractControlFixer
    {
        //Fix Title
        private bool isTitleByPosition(Size scrSize, Position titlePos)
        {
            if (null == scrSize || null == titlePos)
                throw new FTBAutoTestException("Titile fixer error by empty input param.");

            if (titlePos.x > scrSize.w / 5 || titlePos.y > scrSize.h / 5)
                return false;

            return true;
        }
        
        protected override void doFix(Screen rawScreen)
        {
            //Screen Protected
            if (null == rawScreen)
                throw new FTBAutoTestException("Fix control error by input empty screen.");

            List<AbstractElement> stringElementList = null;

            stringElementList = rawScreen.getElementShipKeyList(ElementShipType.StringOnly, true);
            if (null != stringElementList)
            {
                ElementString titleElementString = null;
                foreach (ElementString stringElement in stringElementList)
                {
                    //Title Or Lable
                    if (false == isTitleByPosition(Screen.screenSize, 
                                                   stringElement.rect.getLeftTopPos()))
                        continue;
                    if (null == titleElementString)
                    {
                        titleElementString = stringElement;
                    }
                    else if(titleElementString.rect.getLeftTopPos().x >= stringElement.rect.getLeftTopPos().x
                        && titleElementString.rect.getLeftTopPos().y >= stringElement.rect.getLeftTopPos().y)
                    {
                        titleElementString = stringElement;
                    }
                }
                if (null != titleElementString)
                {
                    //gotTitle
                    ControlTitle controlTitle = new ControlTitle(titleElementString);
                    controlTitle.hasFixed();
                    rawScreen.addControl(controlTitle);
                }
            }
        }
    }
}
