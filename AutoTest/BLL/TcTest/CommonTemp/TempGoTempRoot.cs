using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;
using System.Text.RegularExpressions;
using Tool.DAL;

namespace Tool.BLL
{
    class TempGoTempRoot : AbstractCmnTestHandler
    {
        private static string homeScrId;
        private static bool isDoCondition = false;
        const string tempOpinionsStr = "Options";
        const string copyStartScrId = "SCRN_COPY_START";
        const string scanToEmailServerScrId = "SCRN_SCAN_EMLSV_DESTINATION";
        string currentPath;

        public static void setHomeScrId(string scrId)
        {
            TempGoTempRoot.homeScrId = scrId;         
        }
        public override void execute()
        {
            int tcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            if(tcIndex == -1)
            {
                isDoCondition = !isDoCondition;
                StaticCommandExecutorList.get(CommandList.move_r).execute();
                return;
            }

            currentPath = TestRuntimeAggregate.getTreeMemory().getTcDir();

            if (isDoCondition == false)
            {
                StaticCommandExecutorList.get(CommandList.move_r).execute();
                TestRuntimeAggregate.setCurrentLevelIndex(0);
            }
            else
            {
                moveToTempRoot();
            }
            base.execute();
        }

        private void moveToTempRoot()
        {
            int tempRootLevelIndex = 0;
            bool isfind = false;
            MatchCollection matchedStrings = Regex.Matches(currentPath, @"[^/]{1,}(/{2,})*[^/]*");
            //int levelcount = TestRuntimeAggregate.getTreeMemory().getLevelCount() + 1;
            for (tempRootLevelIndex = 0; tempRootLevelIndex < matchedStrings.Count; tempRootLevelIndex++)
            {
                if (matchedStrings[tempRootLevelIndex].ToString() == tempOpinionsStr)
                {
                    isfind = true;
                    break;
                }
            }
            if (isfind == false)
            {
                //run copy
                tempRootLevelIndex = findScrIdIndex(matchedStrings.Count,copyStartScrId);
                //run scan to emailserver
                if(tempRootLevelIndex == -1)
                {
                    tempRootLevelIndex = findScrIdIndex(matchedStrings.Count, scanToEmailServerScrId);
                }
                if (tempRootLevelIndex == -1)
                {
                    // the way to deal with a fucking situation
                    TestRuntimeAggregate.setCurrentLevelIndex(0);
                    StaticCommandExecutorList.get(CommandList.move_r).execute();
                    return;
                }
            }

            string targetScrId = TestRuntimeAggregate.getTreeMemory().getLevelButtonToScreenId(tempRootLevelIndex);
            string currentScrId = null;
            string preScrId = null;

            Screen currentScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_r).execute(currentScreen);
            currentScrId = currentScreen.getIdentify().scrId;
            while (currentScrId != targetScrId)
            {
                if (currentScrId == homeScrId || currentScrId == copyStartScrId || currentScrId == scanToEmailServerScrId)
                {
                    tempRootLevelIndex = findScrIdIndex(matchedStrings.Count, currentScrId);
                    break;
                }
                StaticCommandExecutorList.get(CommandList.move_b).execute();
                preScrId = currentScrId;
                currentScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_r).execute(currentScreen);
                currentScrId = currentScreen.getIdentify().scrId;
                
                if (currentScrId == preScrId)
                {
                    StaticCommandExecutorList.get(CommandList.move_r).execute();
                    break;
                }
            }
            TestRuntimeAggregate.setCurrentLevelIndex(tempRootLevelIndex);
        }

        private int findScrIdIndex(int levelCount, string targetScrId)
        {
            for(int i = 0;i< levelCount;i++)
            {
                string scrId = TestRuntimeAggregate.getTreeMemory().getLevelButtonToScreenId(i);
                if(targetScrId == scrId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
