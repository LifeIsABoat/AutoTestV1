using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;
using System.Threading;

namespace Tool.BLL
{
    class OneTCCmnTestGoHome : AbstractCmnTestHandler
    {

        public override void execute()
        {
            TestRuntimeAggregate.setCurrentLevelIndex(0);
            TestRuntimeAggregate.setCurrentTCStatus(TestOneTCStatus.Transfering);

            StaticCommandExecutorList.get(CommandList.move_r).execute();
            if (TestRuntimeAggregate.getcurrentTcManagerName().Contains("Rsp")
                || TestRuntimeAggregate.getcurrentTcManagerName().Contains("RSP"))
            {
                while (true)//loop get NowScreen'Identify
                {
                    Screen currentRawScreen = new Screen();
                    StaticCommandExecutorList.get(CommandList.list_r).execute(currentRawScreen);
                    if (currentRawScreen.getIdentify().scrId != "SCRN_POPUP_REMOTE_SET")
                    {
                        break;
                    }
                    else
                    {
                        StaticCommandExecutorList.get(CommandList.click_k).execute(Machine.MFCTPKeyCode.STOP_KEY);
                        AbstractCommandExecutor tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
                        List<AbstractElement> imageElementList = currentRawScreen.getElementShipKeyList(ElementShipType.ImageWithString, true);
                        if (imageElementList != null)
                        {
                            if (imageElementList.Count >= 3)
                            {
                                //do Nothing
                            }
                            else if (imageElementList.Count == 2)
                            {
                                //sort by x position
                                imageElementList.Sort((img1, img2) =>
                                {
                                    return img1.rect.x - img2.rect.x;
                                });
                                Position btnPos = imageElementList[0].rect.getCenter();
                                tpClicker.execute(btnPos);
                                Thread.Sleep(200);
                            }
                        }
                    }
                }
            }

            base.execute();
        }
    }
}
