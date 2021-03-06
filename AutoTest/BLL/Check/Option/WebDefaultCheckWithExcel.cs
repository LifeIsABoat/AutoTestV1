using Tool.DAL;
using Tool.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tool.BLL.Check
{
    class EwsDefaultCheckWithFTB : AbstractChecker
    {
        int currentTcIndex;
        public override void check()
        {
            treeMemory = TestRuntimeAggregate.getTreeMemory();
            IIterator tcIterator = treeMemory.createSelectedTcIterator();
            opinion = this.GetType().Name;
            //report log
            StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion + " Check Start.");
            //do for
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                if (treeMemory.isEwsValid(tcIterator.currentItem()) == false)////if EWSFTB is "N"
                    continue;

                currentTcIndex = tcIterator.currentItem();
                if (TestRuntimeAggregate.getTreeMemory().isTcValid(currentTcIndex) == false)
                {
                    continue;
                }
                TestCheckResult checkResult = TestCheckResult.NG;
                //CurrentTcIndex
                int LevelCount = treeMemory.getLevelCount(tcIterator.currentItem());
                if (0 == LevelCount)
                    throw new FTBAutoTestException("getLevelCount failed by 0 LevelCount.");

                StaticLog4NetLogger.reportLogger.Info("TC-" + tcIterator.currentItem() + " LevelCount: " + LevelCount.ToString());
                if (isBlackList(ref checkResult, tcIterator.currentItem(), LevelCount) == false)
                {
                    try
                   	{
                        List<int> indexList = treeMemory.getOptionLevelBrotherButtonIndex(currentTcIndex);
                        foreach (int tcIndex in indexList)
                        {
                            string ftbButtonWord = treeMemory.getLevelButtonWord(LevelCount, -1, tcIndex);
                            if (currentTcIndex == tcIndex)
                            {
                                //get NowOptionWords FromEws
                                string EwsTargen = TestRuntimeAggregate.getEwsOptionWords(currentTcIndex, opinion, LevelCount);

                                //excemple:RspOptionWords:[OK]Off
                                if (EwsTargen != null && EwsTargen.Substring(0, 4) == "[OK]")
                                {
                                    string Starr = @"(?<=\[OK\]).+";
                                    ////OptionWords:[OK]Off.Match and delete "[OK]",save the off
                                    Match oneOptionWord = Regex.Match(EwsTargen, Starr, RegexOptions.IgnoreCase);

                                    StaticLog4NetLogger.reportLogger.Info("TC-" + currentTcIndex + " EwsOptionSet: " + oneOptionWord.ToString());
                                    bool result = checkOptionSettingWithPanelFromEws(oneOptionWord.ToString(), currentTcIndex);
                                    //if Compare's return is true
                                    //checkResult is TestCheckResult.OK
                                    checkResult = (result == true) ? TestCheckResult.OK : TestCheckResult.NG;
                                }
                                else
                                {
                                    checkResult = TestCheckResult.NG;
                                }
                            }
                        }
                    }
                    catch (FTBAutoTestException ex)
                    {
                        StaticLog4NetLogger.reportLogger.Warn("TC-" + tcIterator.currentItem() + "check " + opinion + " failed by " + ex.Message);
                        StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                    }
                }
                TestRuntimeAggregate.setCheckResult(tcIterator.currentItem(), opinion, checkResult, LevelCount);
                //report log
                StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion +
                                                    "TC-" + tcIterator.currentItem() +
                                                    "level-" + LevelCount +
                                                    "result-" + checkResult.ToString());
            }//end for loop
            //report log end
            StaticLog4NetLogger.reportLogger.Info("opinionName-" + opinion + " Check End");
        }

        private bool checkOptionSettingWithPanelFromEws(string EwsTargen, int tcIndex)
        {
            string PanelSetStr = treeMemory.getOptionWords(tcIndex);
            //if 'PanelSetStr == null' or 'RspTargen == null'
            //return false;
            if (PanelSetStr == null || EwsTargen == null)
            {
                return false;
            }
            StaticLog4NetLogger.reportLogger.Info("TC-" + tcIndex + " Now Machine Set: " + PanelSetStr.ToString());
            //Panel's SetResult == Rsp's SetResult
            //return true;
            if (PanelSetStr == EwsTargen)
            {
                return true;
            }
            return false;
        }
    }
}
