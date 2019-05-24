using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;
using System.Text.RegularExpressions;

namespace Tool.BLL
{
    class InputToOption : AbstractCmnTestHandler
    {
        const string opinion = "InputToOptionChecker";
        ControlInput inputButton = new ControlInput();
        private ScreenType type;

        public override void execute()
        {
            Screen tmpScreen = new Screen();
            DAL.IFTBCommonAPI treeMemory = TestRuntimeAggregate.getTreeMemory();
            string ftbUsWords = treeMemory.getOptionWords(TestRuntimeAggregate.getCurrentTcIndex());
            object screen = TestRuntimeAggregate.getLogScreen(TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());
            List<AbstractControl> inputList = ((Screen)screen).getControlList(typeof(ControlInput));
            //first if ftbUsWords Contains "Manual:" or "BRNManual:"
            if (Regex.IsMatch(ftbUsWords, "^(BRN)?Manual:", RegexOptions.IgnoreCase))
            {
                string pat = @"(?<=(BRN)?Manual:).+";
                Match matchStr = Regex.Match(ftbUsWords, pat, RegexOptions.IgnoreCase);
                string limit = Regex.Match(matchStr.ToString(), @"(?<=Limit:)\d*", RegexOptions.IgnoreCase).ToString();
                for (int i = 0; i <= Convert.ToInt32(limit); i++)
                {
                    StaticCommandExecutorList.get(CommandList.click_s).execute(ControlSoftkeyStatus.delete);
                }
                inputContentAccordingToManualLimit(matchStr.ToString(), screen);
            }
            else if (Regex.IsMatch(ftbUsWords, @"^\[\d+-\d+\]/1", RegexOptions.IgnoreCase))
            {
                //first if ftbUsWords Contains "[xxx-xxxxx]"
                //excample: IP Adress:000.000.000.000
                for (int i = 0; i <= 13; i++)
                {
                    StaticCommandExecutorList.get(CommandList.click_s).execute(ControlSoftkeyStatus.delete);
                }
                inputContentForSpecialNumScreen(ftbUsWords, screen);
            }

            base.execute();
        }//end public override void execute()

        private void inputContentAccordingToManualLimit(string manualStr, object screen)
        {
            string limit = Regex.Match(manualStr, @"(?<=Limit:)\d*", RegexOptions.IgnoreCase).ToString();
            string charaset = Regex.Match(manualStr, @"(?<=Charaset:)\w*", RegexOptions.IgnoreCase).ToString();
            int len = Convert.ToInt32(limit);
            List<AbstractControl> inputList = ((Screen)screen).getControlList(typeof(ControlInput));
            //get now currenTcIndex and LevelCount
            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            int LevelCount = TestRuntimeAggregate.getLevelInfoListCount(currentTcIndex);
            //
            if (screen is Screen)
            {
                List<string> numStrList = new List<string>() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
                List<string> englishStrList = new List<string>() { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P"};
                List<string> anyList = new List<string>();
                string anysoftkey = null;
                //type:[(EnglishOnly,0) (//EnglishWithNumber,1) (//NumberOnly,2)]
                type = ((ControlInput)inputList[0]).screenType;
                if (len <= 10)
                {
                    if (Convert.ToInt32(type) == 2)
                    {
                        for (int i = 1; i <= len; i++)
                        {
                            anyList.Add(numStrList[i]);
                            anysoftkey = string.Join("", anyList.ToArray());
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= len; i++)
                        {
                            anyList.Add(englishStrList[i]);
                            anysoftkey = string.Join("", anyList.ToArray());
                        }
                    }
                }//end if(len <= 10)
                else//len>10
                {
                    if (Convert.ToInt32(type) == 2)
                    {
                        anysoftkey = "0123456789";
                    }
                    else
                    {
                        anysoftkey = "QWERTYUIOP";
                    }
                }
                StaticCommandExecutorList.get(CommandList.click_s).execute(anysoftkey);
                TestRuntimeAggregate.setInputContent(anysoftkey, TestRuntimeAggregate.getCurrentTcIndex(), opinion, TestRuntimeAggregate.getCurrentLevelIndex());
                StaticCommandExecutorList.get(CommandList.click_s).execute(ControlSoftkeyStatus.OK);
                setOpinionInfo(currentTcIndex, LevelCount - 1/*,false*/);
            }
        }//end private void inputContentAccordingToManualLimit

        private void inputContentForSpecialNumScreen(string ftbUsWords, object screen)
        {
            string pat = @"\[([^\]])*\]";
            MatchCollection modularRet = Regex.Matches(ftbUsWords, pat, RegexOptions.IgnoreCase);
            //get now currenTcIndex and LevelCount
            int currentTcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            int LevelCount = TestRuntimeAggregate.getLevelInfoListCount(currentTcIndex);
            //
            if (modularRet.Count > 0)
            {
                string numModular = modularRet[0].ToString();//numModular: [xxxx-xxxx] 
                int numModularCount = splitSpecialNumStr(numModular); //numModularCount: x
                List<AbstractControl> inputList = ((Screen)screen).getControlList(typeof(ControlInput));

                if (screen is Screen)
                {
                    List<string> numStrList = new List<string>() { "1", "2", "0", "1", "2", "0", "1", "2", "0", "1", "2", "0", "1", "2", "0", "1", "2", "0", "1", "2" };
                    List<string> anyList = new List<string>();
                    string anysoftkey = null;
                    //type:[(EnglishOnly,0) (//EnglishWithNumber,1) (//NumberOnly,2)]
                    type = ((ControlInput)inputList[0]).screenType;
                    if (Convert.ToInt32(type) == 2)
                    {
                        for (int i = 0; i < numModularCount * modularRet.Count; i++)
                        {
                            anyList.Add(numStrList[i]);
                            anysoftkey = string.Join("", anyList.ToArray());
                        }
                    }
                    StaticCommandExecutorList.get(CommandList.click_s).execute(anysoftkey);
                    TestRuntimeAggregate.setInputContent(anysoftkey, TestRuntimeAggregate.getCurrentTcIndex(), opinion, TestRuntimeAggregate.getCurrentLevelIndex());
                    StaticCommandExecutorList.get(CommandList.click_s).execute(ControlSoftkeyStatus.OK);
                    setOpinionInfo(currentTcIndex, LevelCount - 1/*,false*/);
                }
            }
        }//end private void inputContentForSpecialNumScreen

        private void setOpinionInfo(int tcIndex, int levelIndex/*, bool isFixAll = true*/)
        {
            Screen currentScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_f).execute(currentScreen);
            AbstractScreenAggregate screenAggregate = null;

            if (currentScreen.isScrollable()/* && isFixAll == true*/)
            {
                screenAggregate = AbstractScreenAggregate.import(currentScreen);
                Screen addScreen = screenAggregate.toFirstScreen(currentScreen);
                while (!screenAggregate.isScreenContains(addScreen))
                {
                    screenAggregate.appendScreen(addScreen);
                    parseSingleScreen(addScreen, tcIndex, levelIndex);
                    screenAggregate.moveToNextScreen(addScreen);
                    addScreen = new Screen();
                    StaticCommandExecutorList.get(CommandList.list_f).execute(addScreen);
                }
            }
            else
            {
                parseSingleScreen(currentScreen, tcIndex, levelIndex);
            }
            //
            if (screenAggregate == null)
            {
                TestRuntimeAggregate.setOpinionScreen(currentScreen, tcIndex, opinion, levelIndex);
            }
            else
            {
                TestRuntimeAggregate.setOpinionScreen(screenAggregate, tcIndex, opinion, levelIndex);
            }
        }//end private void setOpinionInfo

        private void parseSingleScreen(Screen screen, int tcIndex, int levelIndex)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
        }

        private int splitSpecialNumStr(string numModular)
        {
            string[] splitRet = numModular.Split(new char[] { '-', '[', ']' });
            List<string> splitList = new List<string>();
            for (int i = 0; i < splitRet.Count(); i++)
            {
                if (splitRet[i] != "")
                {
                    splitList.Add(splitRet[i]);
                }
            }
            return splitList[splitList.Count - 1].Length;
        }
    }//end class InputToOption
}
