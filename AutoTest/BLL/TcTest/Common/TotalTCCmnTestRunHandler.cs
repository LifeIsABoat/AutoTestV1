using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class TotalTCCmnTestRunHandler : AbstractCmnTestHandler
    {
        int TcCount = 0;
        AbstractCmnTestHandler handler;
        string tcMangerName = null;
        public TotalTCCmnTestRunHandler(AbstractCmnTestHandler handler, string tcMangerName = null)
        {
            this.handler = handler;
            this.tcMangerName = tcMangerName;
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
        }
        public override void execute()
        {
            IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            IIterator tcIterator = treeMemory.createSelectedTcIterator();
            //RunAllTCs
            if (tcMangerName != null)
            {
                StaticLog4NetLogger.reportLogger.Info("Run" + tcMangerName);
            }
            //which Tc is Selected
            int tcSelected = TreeMemoryFTBCommonAggregate.sortedTcLeafIndexes.Count;
            //
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                int tcIndex = tcIterator.currentItem();
                if(isRunCurrentTc(tcIndex) == false)
                {
                    TestRuntimeAggregate.setTcMessage(Math.Round(100.0 * (TcCount++ + 1) / tcSelected, 2) + "%");
                    continue;
                }
                TestRuntimeAggregate.setCurrentTcIndex(tcIndex);
                if (tcIndex >= 0)
                {
                    TestRuntimeAggregate.setMachineLogPath(tcIndex, StaticEnvironInfo.getMachineLogPath(tcIndex));
                    TestRuntimeAggregate.setCommandLogPath(tcIndex, StaticEnvironInfo.getCommandLogPath(tcIndex));
                    TestRuntimeAggregate.setOcrLogPath(tcIndex, StaticEnvironInfo.getOcrLogPath(tcIndex));
                    TestRuntimeAggregate.setScreenImagePath(tcIndex, StaticEnvironInfo.getScreenImagePath(tcIndex));
                }
                try
                {
                    handler.execute();
                    StaticLog4NetLogger.reportLogger.Info("TC-" + tcIndex + "------>OK");
                }
                catch (FTBAutoTestException ex)
                {
                    StaticLog4NetLogger.reportLogger.Warn("TC-" + tcIndex + "------>NG\r\nReason:" + ex.Message + "\r\n");
                    StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                }
                catch (System.Threading.ThreadAbortException)
                {
                    base.execute();
                    break;
                }
                catch (Exception ex)
                {
                    StaticLog4NetLogger.reportLogger.Warn("TC-" + tcIndex + "------>Expection\r\nReason:" + ex.Message + "\r\n");
                    StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                }
                finally
                {
                    TestRuntimeAggregate.setTcMessage(Math.Round(100.0 * (TcCount++ + 1) / tcSelected, 2) + "%");
                }
            }

            base.execute();
        }

        private bool isRunCurrentTc(int tcIndex = -1)
        {
            List<string> opinionList = TestRuntimeAggregate.getTcRunManagerOpinion(tcMangerName);
            if(opinionList.Contains("FtbStringChecker"))
            {
                return true;
            }
            string path = treeMemory.getTcDir(tcIndex);
            string optionStr = treeMemory.getOptionWords(tcIndex);
            foreach (string opinion in opinionList)
            {
                if(isBlackList(opinion, path, optionStr) == false)
                {
                    return true;
                }
            }

            return false;
        }

        private bool isBlackList(string opinion, string path, string optionStr)
        {
            OpinionRunBlackListInfo black = TestRuntimeAggregate.getOpinionBlackList(opinion);
            if (black == null)
            {
                return false;
            }
            RunBlackList NABlackList = black.NABlackList;
            RunBlackList NTBlackList = black.NTBlackList;
            if (NTBlackList != null && NTBlackList.blackList != null && NTBlackList.blackList.Contains(path))
            {
                return true;
            }
            else if (NABlackList != null && NABlackList.blackList != null && NABlackList.blackList.Contains(path))
            {
                return true;
            }
            else if (NTBlackList != null && NTBlackList.regulations != null)
            {
                foreach (string pattren in NTBlackList.regulations)
                {
                    if (Regex.IsMatch(optionStr, pattren, RegexOptions.IgnoreCase))
                    {
                        return true;
                    }
                }
            }
            else if (NABlackList != null && NABlackList.regulations != null)
            {
                foreach (string pattren in NABlackList.regulations)
                {
                    if (Regex.IsMatch(optionStr, pattren, RegexOptions.IgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
