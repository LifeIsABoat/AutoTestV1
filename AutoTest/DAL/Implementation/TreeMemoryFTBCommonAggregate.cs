using System;
using System.Collections;
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
    /*
     *  Description: Tree memory accessor,Implementation interface IDataAccessAPI and IAggregate
     */
    class TreeMemoryFTBCommonAggregate : IFTBCommonAPI
    {
        private static IFTBCommonAggregateImport importer;
        private static AbstractScreenComponent root;
        private static Dictionary<string, List<AbstractScreenComponent>> pathComponentDict = new Dictionary<string, List<AbstractScreenComponent>>();        // tcLeaf indexes
        public static List<OptionLeaf> tcLeafIndexes = new List<OptionLeaf>();

        public static List<int> sortedTcLeafIndexes = new List<int>();

        public List<string> pathLists = new List<string>();
        public string strBuf = null;

        // levelComposite indexes
        public List<AbstractScreenComponent> levelCompositeIndexes = new List<AbstractScreenComponent>();

        private int preTcIndex;
        // tcLeaf indexes of iterator
        private IIterator tcIterator;
        // levelComposite indexes of iterator
        private IIterator levelIterator;
        //Condition screenstandard of iterator
        //private IIterator ScreenIterator;

        //Take node according to path
        public static Dictionary<string, string> wordList = new Dictionary<string, string>();

        public static void setImporter(IFTBCommonAggregateImport importer)
        {
            TreeMemoryFTBCommonAggregate.importer = importer;
        }
        /* 
         *  Description:The constructor
         *  Param:importer - Import data class objects
         *  Return:
         *  Exception:
         *  Example:
         */
        public TreeMemoryFTBCommonAggregate()
        {
            if (TreeMemoryFTBCommonAggregate.importer == null)
            {
                throw new FTBAutoTestException("create error! importer is null.");
            }
        }

        void IFTBCommonAggregate.importTree()
        {
            importer.import(this);
            creatIndexes(root);
        }

        void IFTBCommonAggregate.importScreenDict()
        {
            setScreenInfo();
        }

        IIterator IFTBCommonAggregate.createMccFilteredTcIterator()
        {
            tcIterator = new TreeMemoryFTBCommonTcIterator(this);
            return tcIterator;
        }
        IIterator IFTBCommonAggregate.createSelectedTcIterator()
        {
            tcIterator = new TreeMemoryFTBCommonSelectedTcIterator(this);
            return tcIterator;
        }
        IIterator IFTBCommonAggregate.createLevelIterator()
        {
            //if (null == levelIterator)
            {
                levelIterator = new TreeMemoryFTBCommonLevelIterator(this);
                return levelIterator;
            }
            //else
            //{
            //    return levelIterator;
            //}
        }

        /* 
         *  Description:get mcc model list
         *  Param:model_list - Save ModelList's key
         *  Return:List<string>
         *  Exception:
         *  Example:ftbmcc.getModelList(modelList);
         */
        List<string>IFTBCommonAPI.getTotalModelList()
        {
            return Mcc.getModelList();
        }
        /*
        *  Description:get mcc continent list
        *  Param:continent_list - Save ContinentList's key
        *  Return:List<string>
        *  Exception: 
        *  Example: ftbmcc.getContinentList(continentList); 
        */
        List<string>  IFTBCommonAPI.getTotalFilterContinentList(string model)
        {
            return Mcc.getContinentList(model);
        }
        void IFTBCommonAPI.getTotalContinentList(List<string> continentList)
        {
            Mcc.getAllContinentList(continentList);
        }
        /* 
        *  Description:get mcc continent list
        *  Param:continent - ContinentList's key
        *  Return:List<string>
        *  Exception:FTBAutoException
        *  Example: ftbmcc.getMccContinentCountryList(continent, continentCountryList);
        */
         List<string>IFTBCommonAPI.getTotalFilterCountryList(string continent,string model)
        {
             return Mcc.getMccContinentCountryList(continent,model);
        }
        void IFTBCommonAPI.getTotalCountryList(string continent, List<string> continentCountryList)
        {
            Mcc.getCountryList(continent, continentCountryList);
        }
        /* 
        *  Description:get  model list
        *  Param:model_list - Save ModelList's key
        *  Return:List<string>
        *  Exception:
        *  Example:ftbMcc.getModelname(Modelname)
        */
        List<string> IFTBCommonAPI.getTotalLanguageModelList()
        {
            return Language.getModelname();
        }
        /* 
        *  Description:get mcc language list
        *  Param:language_list - Save languageList's key
        *  Param:model - model's key
        *  Param:country - country's key
        *  Return:List<string>
        *  Exception:
        *  Example:ftbMcc.addlanguageData(languagelist,"MFC", "USA")
        */
        List<string> IFTBCommonAPI.getTotalLanguageList(string model, string country)
        {
            return Language.getlanguage(model, country);
        }
        /* 
         *  Description:set model mcc
         *  Param:model - Form selected model's word
         *  Param:country - Form selected country's word
         *  Return:
         *  Exception:FTBAutoException
         *  Example:ftbMcc.setModelAndCountry("MFC-J893N","US")
         */
        void IFTBCommonAPI.setTotalModelAndCountry(string model, string country)
        {
            Mcc.setModelAndCountry(model, country);
        }
        string IFTBCommonAPI.getSelectModel()
        {
            return Mcc.getSelectedModel();
        }
        string IFTBCommonAPI.getSelectContinent()
        {
            return Mcc.getSelectedContinent(Mcc.getSelectedCountry());
        }
        string IFTBCommonAPI.getSelectCountry()
        {
            return Mcc.getSelectedCountry();
        }
        void IFTBCommonAPI.setTotalSelected()
        {
            foreach (OptionLeaf left in tcLeafIndexes)
                left.tcStatus = true;
        }
        void IFTBCommonAPI.setTotalUnselected()
        {
            foreach (OptionLeaf left in tcLeafIndexes)
                left.tcStatus = false;
        }
        string IFTBCommonAPI.getTotalCondition(int conditionIndex)
        {
            if (Contidion._NOCONDITION == conditionIndex)
                return "";
            else
                return Contidion.getCondition(conditionIndex);
        }
        List<string> IFTBCommonAPI.getTotalPath()
        {
            List<string> path = new List<string>();
            foreach (string kerStr in pathComponentDict.Keys)
            {
                path.Add(kerStr);
            }
            return path;
        }
        List<string> IFTBCommonAPI.getTotalConditionList()
        {
            return Contidion.getConditionList();
        }

        void IFTBCommonAPI.setTotalContidionType()
        {
            Contidion.clearAllConditionType();
        }

        void IFTBCommonAPI.setTotalContidionIndex()
        {
            Contidion.clearAllConditionTcIndex();
        }

        void IFTBCommonAPI.setTotalContidionUnselect()
        {
            Contidion.clearAllConditionSelectedStatus();
        }

        void IFTBCommonAPI.setTotalHardwareDevice(int index)
        {
            Contidion.setConditionTypeToHardwareDevice(index);
        }

        void IFTBCommonAPI.setTotalOptionSetting(int index, int MatchTCIndex)
        {
            Contidion.setConditionTypeToOptionSetting(index, MatchTCIndex);
        }

        void IFTBCommonAPI.setTotalOptionSettingSelected()
        {
            Contidion.selectOptionSettingCondition();
        }

        void IFTBCommonAPI.setHardwareDeviceSelected(int conditionIndex)
        {
            Contidion.selectHardwareDeviceCondition(conditionIndex);
        }

        void IFTBCommonAPI.setTotalNoConditionSelected()
        {
            Contidion.selectNoCondition();
        }

        void IFTBCommonAPI.setSortedTcSelected(List<int> tcIndexList)
        {
            sortedTcLeafIndexes.Clear();
            sortedTcLeafIndexes = tcIndexList;
        }

        /*
         *  Description: Update the contents of the Leaf index
         *  Return: 
         *  Exception: 
         *  Example: SetLeafValueEnable(100);
         */
        void IFTBCommonAPI.setTcSelected(int tcIndex)
        {
            tcLeafIndexes[tcIndex].tcStatus = true;
        }
        /*
         *  Description: Update the contents of the Leaf index
         *  Return: 
         *  Exception: 
         *  Example: SetLeafValueEnable(100);
         */
        void IFTBCommonAPI.setTcUnselected(int tcIndex)
        {
            tcLeafIndexes[tcIndex].tcStatus = false;
        }

        string IFTBCommonAPI.getTcFactorySetting(int tcIndex)
        {
            if (tcIterator == null)
            {
                throw new FTBAutoTestException("tcIterator is null");
            }
            if (tcIndex < -1 || tcIndex >= tcLeafIndexes.Count)
            {
                throw new FTBAutoTestException("tcIndex overstep the boundary");
            }
            if (-1 == tcIndex)
            {
                tcIndex = tcIterator.currentItem();
            }
            return ((OptionInfo)tcLeafIndexes[tcIndex].ftbButton).factorySetting;
        }
        string IFTBCommonAPI.getTcComment(int tcIndex)
        {
            if (tcIterator == null)
            {
                throw new FTBAutoTestException("tcIterator is null");
            }
            if (tcIndex < -1 || tcIndex >= tcLeafIndexes.Count)
            {
                throw new FTBAutoTestException("tcIndex overstep the boundary");
            }
            if (-1 == tcIndex)
            {
                tcIndex = tcIterator.currentItem();
            }
            return ((OptionInfo)tcLeafIndexes[tcIndex].ftbButton).comment;
        }
        
        bool IFTBCommonAPI.isTcSelected(int tcIndex)
        {
            return false != tcLeafIndexes[tcIndex].tcStatus;
        }
        public bool isTcConditionSelected(int tcIndex)
        {
            bool isSelected = false;
            List<int> conditionList = getTotalLevelConditionIndexList(tcIndex);
            foreach (int oneConditionIndex in conditionList)
            {
                isSelected = Contidion.getConditionSelected(oneConditionIndex);
                if (isSelected == false)
                {
                    return isSelected;
                }
            }
            return isSelected;
        }
        public List<int> getTotalLevelConditionIndexList(int tcIndex)
        {
            checkLevelComposite(tcIndex);
            List<int> conditionList = new List<int>();
            foreach (AbstractScreenComponent screen in levelCompositeIndexes)
            {
                int conditionIndex = screen.ftbButton.ftbContidion.conditionIndex;
                ConditionType type = Contidion.getConditionType(conditionIndex);
                if (type != ConditionType.NoCondition)
                {
                    conditionList.Add(conditionIndex);
                }

            }
            if (conditionList.Count == 0)
            {
                conditionList.Add(0);
            }
            return conditionList;
        }
        public bool isTcValid(int tcIndex, int time)
        {
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }
            if (time > 10)
            {
                //check time > 10 -> false
                return false;
            }
            List<int> conditionIndexList = getTotalLevelConditionIndexList(tcIndex);
            if (time > 0 && conditionIndexList.Count == 1 && conditionIndexList[0] == 0)
            {
                //tc is no condition type -> true
                return true;
            }
            if (false == tcLeafIndexes[tcIndex].tcStatus)
                return false;
            if (isTcConditionSelected(tcIndex) == false)
                return false;

            foreach (int oneConditionindex in conditionIndexList)
            {
                int tempTcIndex = Contidion.getConditionTcIndex(oneConditionindex); ;
                if (tempTcIndex >= 0)
                {
                    if (isTcValid(tempTcIndex, time + 1) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        bool IFTBCommonAPI.isRspValid(int tcIndex)
        {
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }
            string RspResult = ((OptionInfo)(tcLeafIndexes[tcIndex].ftbButton)).rsp;
            if (RspResult == "Y")
            {
                return true;
            }
            return false;
        }
        bool IFTBCommonAPI.isEwsValid(int tcIndex)
        {
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }
            string EwsResult = ((OptionInfo)(tcLeafIndexes[tcIndex].ftbButton)).ews;
            if (EwsResult == "Y")
            {
                return true;
            }
            return false;
        }
        string IFTBCommonAPI.getTcRsp(int tcIndex )
        {
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }
            if (tcIndex < -1 || tcIndex >= tcLeafIndexes.Count)
            {
                throw new FTBAutoTestException("tcIndex overstep the boundary");
            }
            return ((OptionInfo)(tcLeafIndexes[tcIndex].ftbButton)).rsp;
        }
        string IFTBCommonAPI.getTcEws(int tcIndex)
        {
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }
            if (tcIndex < -1 || tcIndex >= tcLeafIndexes.Count)
            {
                throw new FTBAutoTestException("tcIndex overstep the boundary");
            }
            return ((OptionInfo)(tcLeafIndexes[tcIndex].ftbButton)).ews;
        }
        //conditionIndex -> TcIndex
        int IFTBCommonAPI.getTcIndexFromConditionTndex(int conditionIndex)
        {
            return Contidion.getConditionTcIndex(conditionIndex);
        }
        //condition buff -> TcIndex
        int IFTBCommonAPI.getTcIndexFromConditionString(string condition)
        {
            return Contidion.getTcIndex(condition);
        }

        //conditionIndex -> ConditionType
        ConditionType IFTBCommonAPI.getConditionType(int conditionIndex)
        {
            return Contidion.getConditionType(conditionIndex);
        }

        bool IFTBCommonAPI.isLevelButtonVaild(int levelIndex, int btnIndex, int tcIndex)
        {
            bool isLeafMccVaild = true;

            checkLevelComposite(tcIndex);
            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            if (levelCompositeIndexes[levelIndex].GetType() == typeof(ButtonComposite))
            {
                if (-1 == btnIndex)
                {
                    throw new NotImplementedException();//todo
                }
                else if ((btnIndex < 0) || (btnIndex >= ((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList.Count))
                {
                    throw new FTBAutoTestException("Parameter is abnormal, btnIndex too big or too small");
                }
                else
                {
                    if ((((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList[btnIndex]).GetType() == typeof(OptionLeaf))
                    {
                        OptionLeaf oneTcLeaf = (OptionLeaf)(((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList[btnIndex]);
                        if (((OptionInfo)(oneTcLeaf.ftbButton)).ftbMcc.isMccValid() == false)
                        {
                            isLeafMccVaild = false;
                        }
                    }
                }

            }
            return isLeafMccVaild;
        }
        public bool isLevelButtonVaild(string path,int condition)
        {

            AbstractScreenComponent screenCom=findComponent(pathComponentDict[path], condition);
            
            List<AbstractScreenComponent> screenComponent = new List<AbstractScreenComponent>();
            //IFTBCommonAPI fib = new TreeMemoryFTBCommonAggregate();
            getAllLeafComposite(path, screenComponent, condition);
            for (int index = 0; index < screenComponent.Count; index++)
            {
                if (((OptionInfo)(screenComponent[index].ftbButton)).ftbMcc.isMccValid() == true)
                {
                    return true;
                }
                if (index == screenComponent.Count - 1)
                {
                    return false;
                }
            }
            return ((OptionInfo)(screenCom.ftbButton)).ftbMcc.isMccValid();
        }

        string IFTBCommonAPI.getTcDir(int tcIndex)
        {
            string path = "";
            //checkLevelComposite(tcIndex);
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }

            List<string> tempTcStrList = new List<string>();
            for (AbstractScreenComponent node = TreeMemoryFTBCommonAggregate.tcLeafIndexes[tcIndex];
                    node.parents != null; node = node.parents)
            {
                string buttonWord = node.ftbButton.usWords.Replace("/", "//");
                tempTcStrList.Add(buttonWord);
            }
            tempTcStrList.Reverse();
            path += string.Join("/", tempTcStrList.ToArray());
            
            return path;
        }

        public void setTcTestResult(bool result, int testOpinionIndex, int levelIndex = -1, int tcIndex = -1)
        {
            if (-1 == tcIndex)
            {
                tcIndex = tcIterator.currentItem();
            }
            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            if (result == true)
            {
                tcLeafIndexes[tcIndex].tcTestResult[testOpinionIndex] |= (short)(1 << levelIndex);
            }
        }

        public void setTcStatus(bool TcConclusion, int tcIndex)
        {
            if (-1 == tcIndex)
            {
                tcIndex = tcIterator.currentItem();
            }
            tcLeafIndexes[tcIndex].tcStatus = TcConclusion;
        }
        bool IFTBCommonAPI.getTcStatus(int tcIndex)
        {
            if (-1 == tcIndex)
            {
                tcIndex = tcIterator.currentItem();
            }
            return tcLeafIndexes[tcIndex].tcStatus;
        }

        int IFTBCommonAPI.getLevelButtonIndex(string btnWords, int levelIndex, int tcIndex)
        {
            checkLevelComposite(tcIndex);

            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            AbstractScreenComponent buttonComposite = levelCompositeIndexes[levelIndex];
            if (null == buttonComposite.parents)
            {
                return 0;
            }
            else
            {
                buttonComposite = buttonComposite.parents;
                int index = 0;
                foreach (AbstractScreenComponent node in ((ButtonComposite)buttonComposite).screenComponentList)
                {
                    if (btnWords == node.ftbButton.usWords)
                    {
                        return index;
                    }
                    index++;
                }
                return -1;
            }
        }
        int IFTBCommonAPI.getLevelButtonCount(int levelIndex, int tcIndex)
        {
            checkLevelComposite(tcIndex);
            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            AbstractScreenComponent buttonComposite = levelCompositeIndexes[levelIndex];
            if (null == buttonComposite.parents)
            {
                return 1;
            }
            else
            {
                buttonComposite = buttonComposite.parents;
                return ((ButtonComposite)buttonComposite).screenComponentList.Count;
            }
        }
        List<string> IFTBCommonAPI.getLevelButtonHelpInfo(int levelIndex, int tcIndex)
        {
            checkLevelComposite(tcIndex);
            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            AbstractScreenComponent buttonComposite = levelCompositeIndexes[levelIndex];
            return buttonComposite.stringHelpInfoList;
        }
        List<string> IFTBCommonAPI.getLevelButtonWords(int levelIndex, int tcIndex)
        {
            checkLevelComposite(tcIndex);

            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                //throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
                return null;
            }
            AbstractScreenComponent buttonComposite = levelCompositeIndexes[levelIndex];
            List<string> btnWordsList = new List<string>();
            if (null == buttonComposite.parents)
            {
                btnWordsList.Add(buttonComposite.ftbButton.usWords);
            }
            else
            {
                buttonComposite = buttonComposite.parents;
                foreach (AbstractScreenComponent node in ((ButtonComposite)buttonComposite).screenComponentList)
                {
                    btnWordsList.Add(node.ftbButton.usWords);
                }
            }
            return btnWordsList;
        }
        List<int> IFTBCommonAPI.getOptionLevelBrotherButtonIndex(int tcIndex)
        {
            //checkLevelComposite(tcIndex);
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }
            AbstractScreenComponent buttonComposite = tcLeafIndexes[tcIndex];
            List<int> btnIndex = new List<int>();
            if (null == buttonComposite.parents)
            {
                return null;
            }
            else
            {
                buttonComposite = buttonComposite.parents;
                foreach (AbstractScreenComponent node in ((ButtonComposite)buttonComposite).screenComponentList)
                {
                    if (node is OptionLeaf)
                    {
                        int targetIndex = tcLeafIndexes.FindIndex(n => n == node);
                        if (isTcValid(targetIndex, 0) == true)
                        {
                            btnIndex.Add(targetIndex);
                        }
                    }
                }
            }

            return (btnIndex.Count == 0) ? null : btnIndex;
        }
        string IFTBCommonAPI.getLevelButtonWord(int levelIndex, int btnIndex, int tcIndex, string continent)
        {
            string btnWords = "";
            string selectedCountry = Mcc.getSelectedCountry();
            bool usedBritishEnglishCountry = false;
            if (continent == null)
            {
                continent = Mcc.getSelectedContinent(Mcc.getSelectedCountry());
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

            checkLevelComposite(tcIndex);

            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            if (-1 == btnIndex)
            {
                btnWords = levelCompositeIndexes[levelIndex].ftbButton.usWords;
            }
            else if ((btnIndex < 0) || (btnIndex >= ((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, btnIndex too big or too small");
            }
            else
            {
                btnWords = ((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList[btnIndex].ftbButton.usWords;
            }
            //wordList
            if (continent != "US")
            {
                if (continent == "EUR" || usedBritishEnglishCountry == true)
                {
                    //getCorrespondingTable(StaticEnvironInfo.getCorrespondingTableFileName());
                    wordList = americanBritishEnglish.getWordsList();
                    if (wordList.ContainsKey(btnWords))
                    {
                        btnWords = wordList[btnWords];
                    }
                }
            }
            return btnWords;
        }

        string IFTBCommonAPI.getNowScreenLevelButtonWords(string path,int conditionIndex)
        {
            if (pathComponentDict.ContainsKey(path))
            {
                AbstractScreenComponent screenCom = findComponent(pathComponentDict[path], conditionIndex);
                if (screenCom == null)
                {
                    return null;
                }
                return screenCom.ftbButton.usWords;
            }
            else
            {
                return null;
            }
        }
        List<int> IFTBCommonAPI.getPathTcIndexList(string path)
        {
            if (pathComponentDict.ContainsKey(path))
            {
                List<int> tcIndexList = new List<int>();
                List<AbstractScreenComponent> componentList = pathComponentDict[path];
                foreach(AbstractScreenComponent oneComponent in componentList)
                {
                    if(oneComponent is OptionLeaf)
                    {
                        int targetIndex = tcLeafIndexes.FindIndex(n => n == oneComponent);
                        tcIndexList.Add(targetIndex);
                    }
                }
                return tcIndexList;
            }
            else
            {
                return null;
            }
        }

        List<string> IFTBCommonAPI.getLevelSubButtonWords(string path,int conditionIndex)
        {
            List<string> subWords = new List<string>();
            AbstractScreenComponent buttonComposite = null;
            AbstractScreenComponent screenCom = findComponent(pathComponentDict[path], conditionIndex);
            List<AbstractScreenComponent> leafOption = new List<AbstractScreenComponent>();
            if (pathComponentDict.ContainsKey(path))
            {
                if (screenCom.GetType() == typeof(ButtonComposite))
                {
                    buttonComposite = screenCom;
                        foreach (AbstractScreenComponent node in ((ButtonComposite)buttonComposite).screenComponentList)
                    {
                        if (node is OptionLeaf)
                        {
                            //get isMccValid subNode
                            if (((OptionInfo)(node.ftbButton)).ftbMcc.isMccValid())
                            {
                                subWords.Add(node.ftbButton.usWords);
                            }
                        }
                        else if(node is ButtonComposite)
                        {
                            leafOption = new List<AbstractScreenComponent>();
                            bool subNodeFla = false;
                            getAllLeafComposite(node, leafOption, conditionIndex);
                            foreach (AbstractScreenComponent subNode in leafOption)
                            {
                                if (((OptionInfo)(subNode.ftbButton)).ftbMcc.isMccValid())
                                {
                                    subNodeFla = true;
                                }
                            }
                            if (subNodeFla)
                            {
                                subWords.Add(node.ftbButton.usWords);
                            }
                        }
                    }
                }
                return subWords;
            }
            else
            {
                return null;
            }
        }
        List<int> IFTBCommonAPI.getLevelSubButtonCondition(string path,int conditionIndex)
        {
            List<int> subCondition = new List<int>();
            AbstractScreenComponent buttonComposite = null;
            AbstractScreenComponent screenCom = findComponent(pathComponentDict[path], conditionIndex);
            List<AbstractScreenComponent> leafOption = new List<AbstractScreenComponent>();
            if (pathComponentDict.ContainsKey(path))
            {
                if (screenCom.GetType() == typeof(ButtonComposite))
                {
                    buttonComposite = screenCom;
                    bool onlyOptionLeafFlag = false;
                    foreach (AbstractScreenComponent node in ((ButtonComposite)buttonComposite).screenComponentList)
                    {
                        if (node is OptionLeaf)
                        {
                            onlyOptionLeafFlag = true;
                            break;
                        }
                    }
                    foreach (AbstractScreenComponent node in ((ButtonComposite)buttonComposite).screenComponentList)
                    {
                        if (node is OptionLeaf)
                        {
                            //get isMccValid subNode
                            if (((OptionInfo)(node.ftbButton)).ftbMcc.isMccValid())
                            {
                                subCondition.Add(node.ftbButton.ftbContidion.conditionIndex);
                            }
                        }
                        else if(!onlyOptionLeafFlag)
                        {
                            bool subNodeFla = false;
                            leafOption = new List<AbstractScreenComponent>();
                            getAllLeafComposite(node, leafOption, conditionIndex);
                            foreach (AbstractScreenComponent subNode in leafOption)
                            {
                                if (((OptionInfo)(subNode.ftbButton)).ftbMcc.isMccValid())
                                {
                                    subNodeFla = true;
                                }
                            }
                            if (subNodeFla)
                            {
                                subCondition.Add(node.ftbButton.ftbContidion.conditionIndex);
                            }
                        }
                        
                    }
                }
                return subCondition;
            }
            else
            {
                return null;
            }
        }
        List<int> IFTBCommonAPI.getLevelButtonFrontCondition(string path,int conditionIndex)
        {
            List<int> frontCondition = new List<int>();
            AbstractScreenComponent screenComponent = null;
            AbstractScreenComponent screenCom = findComponent(pathComponentDict[path], conditionIndex);
            if (pathComponentDict.ContainsKey(path))
            {
                if (screenCom.ftbButton.ftbContidion.conditionIndex != 0)
                {
                    frontCondition.Add(screenCom.ftbButton.ftbContidion.conditionIndex);
                }
                screenComponent = screenCom.parents;
                while (screenComponent != null)
                {
                    if (screenComponent.ftbButton.ftbContidion.conditionIndex != 0)
                    {
                        frontCondition.Add(screenComponent.ftbButton.ftbContidion.conditionIndex);
                    }
                    screenComponent = screenComponent.parents;
                }
                frontCondition.Reverse();
                return frontCondition;
            }
            else
            {
                return null;
            }
        }
        string IFTBCommonAPI.getLevelButtonToScreenTitle(string path,int conditionIndex)
        {
            if (pathComponentDict.ContainsKey(path))
            {
                AbstractScreenComponent screenCom = findComponent(pathComponentDict[path], conditionIndex);
                if (screenCom.GetType() == typeof(ButtonComposite))
                {
                    return ((ButtonComposite)screenCom).ftbButtonTitle.usWords;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return null;
            }
        }
        string IFTBCommonAPI.getLevelButtonToScreenId(string path, int conditionIndex)
        {
            if (pathComponentDict.ContainsKey(path))
            {
                AbstractScreenComponent screenCom = findComponent(pathComponentDict[path], conditionIndex);
                if (screenCom.GetType() == typeof(ButtonComposite))
                {
                    return ((ButtonComposite)screenCom).ftbButtonTitle.functionName;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return null;
            }
        }
        string IFTBCommonAPI.getLevelButtonToScreenTitle(int levelIndex, int btnIndex, int tcIndex)
        {
            string btnToScrTitle = "";

            checkLevelComposite(tcIndex);
            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            if (levelCompositeIndexes[levelIndex].GetType() == typeof(ButtonComposite))
            {
                LableInfo title;
                if (-1 == btnIndex)
                {
                    title = ((ButtonComposite)levelCompositeIndexes[levelIndex]).ftbButtonTitle;
                }
                else if ((btnIndex < 0) || (btnIndex >= ((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList.Count))
                {
                    throw new FTBAutoTestException("Parameter is abnormal, btnIndex too big or too small");
                }
                else
                {
                    title = ((ButtonComposite)((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList[btnIndex]).ftbButtonTitle;
                }
                if (null != title)
                {
                    btnToScrTitle = title.usWords;
                }
            }
            return btnToScrTitle;
        }
        string IFTBCommonAPI.getLevelButtonToScreenId(int levelIndex, int btnIndex, int tcIndex)
        {
            string btnToScrId = "";

            checkLevelComposite(tcIndex);
            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            if (levelCompositeIndexes[levelIndex].GetType() == typeof(ButtonComposite))
            {
                LableInfo title;
                if (-1 == btnIndex)
                {
                    title = ((ButtonComposite)levelCompositeIndexes[levelIndex]).ftbButtonTitle;
                }
                else if ((btnIndex < 0) || (btnIndex >= ((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList.Count))
                {
                    throw new FTBAutoTestException("Parameter is abnormal, btnIndex too big or too small");
                }
                else
                {
                    title = ((ButtonComposite)((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList[btnIndex]).ftbButtonTitle;
                }
                if (null != title)
                {
                    btnToScrId = title.functionName;
                }
            }
            return btnToScrId;
        }
        string IFTBCommonAPI.getLevelDir(int levelIndex, int btnIndex, int tcIndex)
        {
            string path = "";

            checkLevelComposite(tcIndex);
            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            for (int i = 0; i < levelIndex; i++)
            {
                path = path + levelCompositeIndexes[i].ftbButton.usWords.Replace("/", "//") + "/";
            }
            if (-1 == btnIndex)
            {
                path = path + levelCompositeIndexes[levelIndex].ftbButton.usWords.Replace("/", "//");
            }
            else
            {
                path = path + ((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList[btnIndex].ftbButton.usWords;
            }
            return path;
        }
        int IFTBCommonAPI.getLevelCount(int tcIndex)
        {
            checkLevelComposite(tcIndex);
            if (levelCompositeIndexes.Count == 0)
            {
                throw new FTBAutoTestException("Parameter is abnormal, AbstractScreenComponent count is 0");
            }
            return levelCompositeIndexes.Count - 1;
        }
        int IFTBCommonAPI.getLevelCondition(int levelIndex, int btnIndex, int tcIndex)
        {
            int conditionIndex = 0;

            checkLevelComposite(tcIndex);
            if (-1 == levelIndex)
            {
                levelIndex = levelIterator.currentItem();
            }
            else if ((levelIndex < 0) || (levelIndex >= levelCompositeIndexes.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, levelIndex too big or too small");
            }
            if (-1 == btnIndex)
            {
                conditionIndex = levelCompositeIndexes[levelIndex].ftbButton.ftbContidion.conditionIndex;
            }
            else if ((btnIndex < 0) || (btnIndex >= ((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList.Count))
            {
                throw new FTBAutoTestException("Parameter is abnormal, btnIndex too big or too small");
            }
            else
            {
                conditionIndex = ((ButtonComposite)levelCompositeIndexes[levelIndex - 1]).screenComponentList[btnIndex].ftbButton.ftbContidion.conditionIndex;
            }
            return conditionIndex;
        }
        List<int> IFTBCommonAPI.getLevelCondition(string path)
        {
            List<AbstractScreenComponent> screenComponent = null;
            List<int> conditionList = new List<int>();
            if (pathComponentDict.ContainsKey(path))
            {
                screenComponent = pathComponentDict[path];
                for (int index = 0; index < screenComponent.Count; index++)
                {
                    conditionList.Add(screenComponent[index].ftbButton.ftbContidion.conditionIndex);
                }
                return conditionList;
            }
            else
            {
                return null;
            }

        }
        bool IFTBCommonAPI.isLevelOption(int levelIndex, int tcIndex)
        {
            checkLevelComposite(tcIndex);

            if (levelIndex < levelCompositeIndexes.Count - 1 && levelIndex >= 0)
                return false;
            else if (levelCompositeIndexes.Count - 1 == levelIndex)
                return true;
            else
                throw new FTBAutoTestException("Check whether option by index error, invalid level index input.");

        }
        bool IFTBCommonAPI.isLevelOption(string path,int conditionIndex)
        {
            AbstractScreenComponent screenCom = findComponent(pathComponentDict[path], conditionIndex);
            if (screenCom is ButtonComposite)
            {
                return false;
            }
            return true;
        }
        string IFTBCommonAPI.getFactorySetting(int tcIndex)
        {
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }
            OptionInfo button = TreeMemoryFTBCommonAggregate.tcLeafIndexes[tcIndex].ftbButton as OptionInfo;
            if (button != null)
            {
                return button.factorySetting;
            }
            else
            {
                return null;
            }
        }
        string IFTBCommonAPI.getOptionWords(int tcIndex)
        {
            if (tcIndex == -1)
            {
                tcIndex = tcIterator.currentItem();
            }
            OptionInfo button = TreeMemoryFTBCommonAggregate.tcLeafIndexes[tcIndex].ftbButton as OptionInfo;
            if (button != null)
            {
                return button.usWords;
            }
            else
            {
                return null;
            }
        }
        /*
         *  Description: Set the value of the object "AbstractScreenComponent" of the "root" type
         *  Param: root - New value to set
         *  Return: 
         *  Exception: 
         *  Example: treeMemoryAggregate.setRoot(root);
         */
        public void setRoot(AbstractScreenComponent root)
        {
            TreeMemoryFTBCommonAggregate.root = root;
        }
        public static AbstractScreenComponent getRoot()
        {
            return TreeMemoryFTBCommonAggregate.root;
        }

        /*
         *  Description: Create index the object  of the AbstractScreenComponent type
         *  Param: tree - object the create index 
         *  Return: 
         *  Exception: 
         *  Example: creatIndexes(tree;)
         */
        private void creatIndexes(AbstractScreenComponent tree)
        {
            if (tree.GetType() == typeof(OptionLeaf))
            {
                tcLeafIndexes.Add((OptionLeaf)tree);
            }
            else
            {
                foreach (AbstractScreenComponent node in ((ButtonComposite)tree).screenComponentList)
                {
                    creatIndexes(node);
                }
            }
        }//creatIndexes

        private void checkLevelComposite(int selectedTcIndex)
        {
            if (selectedTcIndex == -1)
            {
                selectedTcIndex = tcIterator.currentItem();
            }
            if (selectedTcIndex != preTcIndex)
            {
                preTcIndex = selectedTcIndex;
                this.levelCompositeIndexes.Clear();
                for (AbstractScreenComponent node = TreeMemoryFTBCommonAggregate.tcLeafIndexes[selectedTcIndex];
                     node.parents != null; node = node.parents)
                {
                    this.levelCompositeIndexes.Add(node);
                }
                this.levelCompositeIndexes.Reverse();
            }
        }
        private void setScreenInfo()
        {
            tcIterator = ((IFTBCommonAPI)this).createMccFilteredTcIterator();
            levelIterator = ((IFTBCommonAPI)this).createLevelIterator();
            string path = "";
            pathComponentDict.Clear();
            List<AbstractScreenComponent> screenComponent = null;
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                for (levelIterator.first(); !levelIterator.isDone(); levelIterator.next())
                {
                    screenComponent = new List<AbstractScreenComponent>();
                    path = ((IFTBCommonAPI)this).getLevelDir(levelIterator.currentItem(), -1, tcIterator.currentItem());
                    //path = path.Substring(0, path.Length - 1);
                    if (pathComponentDict.ContainsKey(path) == false)
                    {
                        screenComponent.Add(levelCompositeIndexes[levelIterator.currentItem()]);
                        pathComponentDict.Add(path, screenComponent);
                    }
                    else
                    {
                        for (int index = 0; index < pathComponentDict[path].Count; index++)
                        {
                            if (pathComponentDict[path][index] != levelCompositeIndexes[levelIterator.currentItem()]&&index== pathComponentDict[path].Count-1)
                            {
                                pathComponentDict[path].Add(levelCompositeIndexes[levelIterator.currentItem()]);
                            }
                        }
                    }
                }
                path = "";
            }
        }
        private void getAllLeafComposite(string path, List<AbstractScreenComponent> leafOption,int conditionIndex)
        {
            AbstractScreenComponent screenCom = findComponent(pathComponentDict[path], conditionIndex);
            if (screenCom is ButtonComposite)
            {
                foreach (AbstractScreenComponent screenComponent in ((ButtonComposite)screenCom).screenComponentList)
                {
                    if (screenComponent is OptionLeaf)
                    {
                        leafOption.Add(screenComponent);
                    }
                    else
                    {
                        foreach (KeyValuePair<string, List<AbstractScreenComponent>> kvp in pathComponentDict)
                        {
                            if (findComponent(kvp.Value, screenComponent.ftbButton.ftbContidion.conditionIndex) == screenComponent)
                            {
                                getAllLeafComposite(kvp.Key, leafOption, screenComponent.ftbButton.ftbContidion.conditionIndex);
                            }
                        }
                    }
                }
            }
        }
        private void getAllLeafComposite(AbstractScreenComponent buttonComposite, List<AbstractScreenComponent> leafOption,int conditionIndex)
        {
            if (buttonComposite is ButtonComposite)
            {
                foreach (AbstractScreenComponent screenComponent in ((ButtonComposite)buttonComposite).screenComponentList)
                {
                    if (screenComponent is OptionLeaf)
                    {
                        leafOption.Add(screenComponent);
                    }
                    else
                    {
                        foreach (KeyValuePair<string, List<AbstractScreenComponent>> kvp in pathComponentDict)
                        {
                            if (findComponent(kvp.Value, screenComponent.ftbButton.ftbContidion.conditionIndex) == screenComponent)
                            {
                                getAllLeafComposite(kvp.Key, leafOption, screenComponent.ftbButton.ftbContidion.conditionIndex);
                            }
                        }
                    }
                }
            }
        }
        private AbstractScreenComponent findComponent(List<AbstractScreenComponent> screenComponentList, int conditionIndex)
        {
            if (conditionIndex == -1)
            {
                return screenComponentList[0];
            }
            foreach (AbstractScreenComponent screenComponent in screenComponentList)
            {
                if (screenComponent.ftbButton.ftbContidion.conditionIndex == conditionIndex)
                {
                    return screenComponent;
                }
            }
            return null;
        }//class

        private void getCorrespondingTable(string path)
        {
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }

            string buf = "";
            // Open and read file contents
            using (StreamReader sr = File.OpenText(path))
            {
                buf = sr.ReadToEnd();
                sr.Close();
            }

            //ConditionJsonInfo result;
            try
            {
                wordList = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(buf);
                if (wordList == null)
                {
                    throw new FTBAutoTestException("DeserializeObject failed");
                }
            }
            catch (System.Threading.ThreadAbortException ex) { throw ex; }
            catch (Exception) { throw new FTBAutoTestException("Execute command error by get invalid command."); }
        }

        public void setModelAndCountrySelectInfoList(List<string> para)
        {
            pathLists = para;
        }
        public List<string> getModelAndCountrySelectInfoList()
        {
            return pathLists;
        }

        public void setTcSelectInfoList(List<string> para)
        {
            pathLists.Clear();
            pathLists = para;
        }
        public List<string> getTcSelectInfoList()
        {
            return pathLists;
        }

        public void setStringBuff(string para)
        {
            strBuf = para;
        }
        public string getStringBuff()
        {
            return strBuf;
        }


      
    }
}
