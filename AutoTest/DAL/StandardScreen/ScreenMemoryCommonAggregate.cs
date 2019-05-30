using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Tool.Command;
using System.Web.Script.Serialization;

namespace Tool.DAL
{
    class StandardScreen
    {
        public List<int> condition { get; set; }
        public List<string> path { get; set; }
        public List<string> words { get; set; }
        public StandardScreen()
        {
            condition = new List<int>();
            path = new List<string>();
            words = new List<string>();
        }
    }

    class ScreenMemoryCommonAggregate : IScreenCommonAPI
    {
        //Condition screenstandard of iterator
        private IIterator ScreenIterator;

        public int screenLines { get; set; }
        public List<string> conditionList { get; set; }
        public List<StandardScreen> standardScreen { get; set; }
        public static Dictionary<string, string> wordsList = new Dictionary<string, string>();
        public ScreenMemoryCommonAggregate()
        {
            conditionList = new List<string>();
            standardScreen = new List<StandardScreen>();
        }

        void IScreenCommonAggregate.importScreen(string path)
        {
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }

            ScreenMemoryCommonAggregate standardScreenInfo = new ScreenMemoryCommonAggregate();
            string buf = "";
            // Open and read file contents
            using (StreamReader sr = File.OpenText(path))
            {
                buf = sr.ReadToEnd();
                buf = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(buf, " ");
                sr.Close();
            }

            //ConditionJsonInfo result;
            try
            {
                standardScreenInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ScreenMemoryCommonAggregate>(buf);
                if(standardScreenInfo != null)
                {
                    screenLines = standardScreenInfo.screenLines;
                    conditionList = standardScreenInfo.conditionList;
                    standardScreen = standardScreenInfo.standardScreen;
                }
                else
                {
                    throw new FTBAutoTestException("DeserializeObject failed");
                }
            }
            catch (System.Threading.ThreadAbortException ex) { throw ex; }
            catch (Exception) { throw new FTBAutoTestException("Execute command error by get invalid command."); }
        }

        IIterator IScreenCommonAggregate.createScreenIterator()
        {
            ScreenIterator = new TreeMemoryFTBCommonScreenIterator(this);
            return ScreenIterator;
        }

        int IScreenCommonAPI.getScreenLines()
        {
            return screenLines;
        }
        List<string> IScreenCommonAPI.getScreenCondition(int currentIndex)
        {
            if ((currentIndex < 0) || (currentIndex >= standardScreen.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, currentIndex too big or too small");
            }
            else
            {
                List<string> currentConditionList = new List<string>();
                for (int index = 0; index < standardScreen[currentIndex].condition.Count; index++)
                {
                    int conditionIndex = standardScreen[currentIndex].condition[index];
                    string currentCondition;
                    if (conditionIndex < 0)
                    {
                        currentCondition = "!"+conditionList[0-conditionIndex];
                    }
                    else
                    {
                        currentCondition = conditionList[conditionIndex];
                    }
                    currentConditionList.Add(currentCondition);
                }
                return currentConditionList;
            }
        }
        List<string> IScreenCommonAPI.getScreenPath(int currentIndex)
        {
            string selectedCountry = Mcc.getSelectedCountry();
            bool usedBritishEnglishCountry = false;
            if ((currentIndex < 0) || (currentIndex >= standardScreen.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, currentIndex too big or too small");
            }
            else
            {
                List<string> currentPathList = new List<string>();
                for (int index = 0; index < standardScreen[currentIndex].path.Count; index++)
                {
                    string currentPath = standardScreen[currentIndex].path[index];
                    currentPathList.Add(currentPath);
                }

                UI.AmericanAndBritish americanBritishEnglish = new UI.AmericanAndBritish();
                americanBritishEnglish.loadCorrespondingInfo(StaticEnvironInfo.getCorrespondingTableFileName());
                List<string> oneList = americanBritishEnglish.getUsedBritishEnglishCountriesList();
                for (int j = 0; j < oneList.Count; j++)
                {
                    if (selectedCountry == oneList[j])
                    {
                        usedBritishEnglishCountry = true;
                    }
                }

                string nowTestArea = Mcc.getSelectedContinent(Mcc.getSelectedCountry());
                if (nowTestArea == "EUR" || usedBritishEnglishCountry == true)
                {
                    //getCorrespondingTable(StaticEnvironInfo.getCorrespondingTableFileName());
                    wordsList = americanBritishEnglish.getWordsList();
                    for (int art = 0; art < currentPathList.Count; art++)
                    {
                        if (wordsList.ContainsKey(currentPathList[art]))
                        {
                            currentPathList[art] = wordsList[currentPathList[art]];
                        }
                    }
                }
                return currentPathList;
            }
        }
        List<string> IScreenCommonAPI.getScreenWords(int currentIndex)
        {
            string selectedCountry = Mcc.getSelectedCountry();
            bool usedBritishEnglishCountry = false;
            if ((currentIndex < 0) || (currentIndex >= standardScreen.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, currentIndex too big or too small");
            }
            else
            {
                List<string> currentWordsList = new List<string>();
                for (int index = 0; index < standardScreen[currentIndex].words.Count; index++)
                {
                    string currentWords = standardScreen[currentIndex].words[index];
                    currentWordsList.Add(currentWords);
                }

                UI.AmericanAndBritish americanBritishEnglish = new UI.AmericanAndBritish();
                americanBritishEnglish.loadCorrespondingInfo(StaticEnvironInfo.getCorrespondingTableFileName());
                List<string> oneList = americanBritishEnglish.getUsedBritishEnglishCountriesList();
                for (int j = 0; j < oneList.Count; j++)
                {
                    if (selectedCountry == oneList[j])
                    {
                        usedBritishEnglishCountry = true;
                    }
                }

                string nowTestArea = Mcc.getSelectedContinent(Mcc.getSelectedCountry());
                if (nowTestArea == "EUR" || usedBritishEnglishCountry == true)
                {
                    //getCorrespondingTable(StaticEnvironInfo.getCorrespondingTableFileName());
                    wordsList = americanBritishEnglish.getWordsList();
                    for (int art = 0; art < currentWordsList.Count; art++)
                    {
                        if (wordsList.ContainsKey(currentWordsList[art]))
                        {
                            currentWordsList[art] = wordsList[currentWordsList[art]];
                        }
                    }
                }
                return currentWordsList;
            }
        }

        private void getCorrespondingTable(string path)
        {
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }
            string buf = "";
            using (StreamReader sr = File.OpenText(path))
            {
                buf = sr.ReadToEnd();
                sr.Close();
            }
            try
            {
                wordsList = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(buf);
                if (wordsList == null)
                {
                    throw new FTBAutoTestException("DeserializeObject failed");
                }
            }
            catch (System.Threading.ThreadAbortException ex) { throw ex; }
            catch (Exception) { throw new FTBAutoTestException("Execute command error by get invalid command."); }
        }

        int IScreenCommonAPI.getScreenCount()
        {
            if (standardScreen == null)
            {
                throw new FTBAutoTestException("standardScreen is null");
            }
            else
            {
                return standardScreen.Count;
            }
        }
        List<StandardScreen> IScreenCommonAPI.getNowStandardScreen()
        {
            return standardScreen;
        }
    }



}
