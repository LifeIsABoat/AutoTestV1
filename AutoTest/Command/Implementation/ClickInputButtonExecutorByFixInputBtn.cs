using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    class ClickInputButtonExecutorByFixInputBtn : AbstractClickInputButtonExecutorMFCTP
    {
        protected override void click(string inputBtnWord)
        {
            if (null == inputBtnWord)
                throw new FTBAutoTestException("Can't click softKeybutton by null inputButton.");

            Screen softKeyScreen = StaticCurrentScreen.get();
            ControlInput softkeyButton;
            //example:inputButtonWord = "A"
            List<AbstractControl> inputCount = softKeyScreen.getControlList(typeof(ControlInput));
            if (inputCount != null)
            {
                softkeyButton = (ControlInput)inputCount[0];
            }
            else
            {
                throw new FTBAutoTestException("Can't click softKeybutton by null inputCount.");
            }

            string expMsg = string.Format("Can't find inputButton in this currentScreen.", inputBtnWord);
            //if InputButton is " ", then assgin "Space" to the inputBtnWord.
            if (inputBtnWord == " ")
            {
                inputBtnWord = "Space";
            }
            for (int i = 0; i < softkeyButton.imageWithStringButtonList.Count; i++)
            {
                //if nowInputButton is "A-Z"||"a-z"||"0-9"||"Space" button
                if (softkeyButton.imageWithStringButtonList[i].stringList[0].str == inputBtnWord)
                {
                    //get "A-Z"||"a-z"||"0-9"||"Space" button's pos
                    Position oneInputPos = softkeyButton.imageWithStringButtonList[i].rect.getCenter();
                    //do Click
                    tpClicker.execute(oneInputPos);
                    return; 
                }
            }//end for
            //if the for loop is disable
            throw new FTBAutoTestException(expMsg);
        }//end click

        protected override void specialBtnClick(ControlSoftkeyStatus specialBtn)
        {
            Screen softKeyScreen = StaticCurrentScreen.get();
            List<AbstractControl> inputList = softKeyScreen.getControlList(typeof(ControlInput));
            //((ControlInput)inputList[0]).specialBtnAggregate;
            //Dictionary<ControlSoftkeyStatus, ControlButton> specialAggregate = new Dictionary<ControlSoftkeyStatus, ControlButton>();
            if (null == inputList)
                throw new FTBAutoTestException("Execute error by null specialAggregate.");

            if (((ControlInput)inputList[0]).specialBtnAggregate.ContainsKey(ControlSoftkeyStatus.delete))
            {
                ControlButton specialButton = ((ControlInput)inputList[0]).specialBtnAggregate[specialBtn];
                //example:okButtonInput = "OK"
                if (null == specialButton)
                    throw new FTBAutoTestException("Can't click softKeybutton by null specialBtn.");
                string expMsg = string.Format("Can't find inputButton in this currentScreen.", specialButton);

                if (null == specialButton.imageList || 1 != specialButton.imageList.Count)
                    throw new FTBAutoTestException(expMsg);

                if (null == specialButton.imageList[0].rect)
                    throw new FTBAutoTestException(expMsg);
                Position targetPos = specialButton.imageList[0].rect.getCenter();
                tpClicker.execute(targetPos);
            }
            else
            {
                throw new FTBAutoTestException("Can't find specialBtn in this currentScreen.");
            }
        }//end protected override void specialBtnClick
    }//end class ClickInputButtonExecutorByFixInputBtn
}
