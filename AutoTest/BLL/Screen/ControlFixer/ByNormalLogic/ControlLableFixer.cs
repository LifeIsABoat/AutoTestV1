using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class ControlLableFixer : AbstractControlFixer
    {
        //Fix Lable
        private bool isLableByFixTitleFirst(ElementString stringElement)
        {
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
                foreach (ElementString stringElement in stringElementList)
                {
                    //Lable Or Title
                    if (false == isLableByFixTitleFirst(stringElement))
                        continue;

                    //Lable
                    ControlLabel controlLabel = new ControlLabel(stringElement);
                    controlLabel.hasFixed();
                    rawScreen.addControl(controlLabel);
                }
            }
        }
    }
}
