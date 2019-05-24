using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;

namespace Tool.BLL
{
    class TempTcSort
    {
        class OptionSettingListCollection
        {
            public List<int> conditionList = new List<int>();
            public List<int> tcIndexList = new List<int>();
        }

        protected IFTBCommonAPI ftbCmnAccessor = null;

        protected IIterator tcIterator = null;

        protected List<int> noConditionList;
        protected List<int> hardwareDeviceList;
        protected List<int> optionSettingList;
        List<OptionSettingListCollection> optionSettingListCollection;

        public TempTcSort(IFTBCommonAPI ftbCmnAccessor)
        {
            this.ftbCmnAccessor = ftbCmnAccessor;
            tcIterator = ftbCmnAccessor.createMccFilteredTcIterator();
        }

        public void sort(AbstractTcManager tcManger = null)
        {
            noConditionList = new List<int>();
            hardwareDeviceList = new List<int>();
            optionSettingList = new List<int>();
            optionSettingListCollection = new List<OptionSettingListCollection>();
            //List<List<int>> 
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                if (ftbCmnAccessor.isTcValid(tcIterator.currentItem()) == false)
                {
                    continue;
                }
                if (tcManger != null)
                {
                    if (tcManger.isTcValid(tcIterator.currentItem()) == false)
                    {
                        //set NA
                        continue;
                    }
                }
                ConditionType conditionType = getTcConditionType();

                if (conditionType == ConditionType.NoCondition)
                {
                    noConditionList.Add(tcIterator.currentItem());
                }
                else if (conditionType == ConditionType.HardwareDevice)
                {
                    hardwareDeviceList.Add(tcIterator.currentItem());//to do
                }
                else
                {
                    List<int> currentOptionSettingList = gettotalConditionList();

                    bool hasfind = false;
                    foreach (OptionSettingListCollection oneOptionSettingList in optionSettingListCollection)
                    {
                        if (oneOptionSettingList.conditionList.All(currentOptionSettingList.Contains) && oneOptionSettingList.conditionList.Count == currentOptionSettingList.Count)
                        {
                            oneOptionSettingList.tcIndexList.Add(tcIterator.currentItem());
                            hasfind = true;
                        }
                    }
                    if (hasfind == false)
                    {
                        OptionSettingListCollection oneOptionSettingList = new OptionSettingListCollection();
                        oneOptionSettingList.conditionList = currentOptionSettingList;

                        foreach (int conditionIndex in currentOptionSettingList)
                        {
                            int tempTcIndex = ftbCmnAccessor.getTcIndexFromConditionTndex(conditionIndex);
                            oneOptionSettingList.tcIndexList.Add(tempTcIndex);
                        }
                        //test.creat();
                        oneOptionSettingList.tcIndexList.Add(tcIterator.currentItem());

                        optionSettingListCollection.Add(oneOptionSettingList);
                    }
                }
            }
            foreach (OptionSettingListCollection oneOptionSettingList in optionSettingListCollection)
            {
                if (oneOptionSettingList.conditionList.Count == oneOptionSettingList.tcIndexList.Count)
                {
                    //no tc
                    continue;
                }
                optionSettingList.Add(-1);
                optionSettingList.AddRange(oneOptionSettingList.tcIndexList);
                optionSettingList.Add(-1);
            }

            List<int> tempSortedTcIndexList = new List<int>();
            tempSortedTcIndexList.AddRange(this.noConditionList);
            tempSortedTcIndexList.AddRange(this.optionSettingList);
            tempSortedTcIndexList.AddRange(this.hardwareDeviceList);
            ftbCmnAccessor.setSortedTcSelected(tempSortedTcIndexList);
        }
        /// <summary>
        /// get (tc,tc->condition->tc,... ) -> ConditionList
        /// </summary>
        /// <param name="tcIndex"></param>
        /// <returns></returns>
        private List<int> gettotalConditionList(int tcIndex = -1)
        {
            List<int> allConditionList = new List<int>();
            List<int> conditionIndexList = ftbCmnAccessor.getTotalLevelConditionIndexList(tcIndex);
            foreach (int oneConditionindex in conditionIndexList)
            {
                List<int> tempAllConditionList;
                int tempTcIndex = ftbCmnAccessor.getTcIndexFromConditionTndex(oneConditionindex);
                if (tempTcIndex == -1)
                {
                    continue;
                }
                else
                {
                    tempAllConditionList = gettotalConditionList(tempTcIndex);
                }
                if (tempAllConditionList.Count > 0)
                {
                    allConditionList.AddRange(tempAllConditionList);
                }
                allConditionList.Add(oneConditionindex);
            }
            return allConditionList;
        }
        /// <summary>
        /// get Tc Condition Type
        /// </summary>
        /// <returns></returns>
        private ConditionType getTcConditionType(int tcIndex = -1)
        {
            ConditionType typeRet = ConditionType.NoCondition;
            List<int> conditionIndexList = ftbCmnAccessor.getTotalLevelConditionIndexList(tcIndex);
            foreach (int oneConditionindex in conditionIndexList)
            {
                ConditionType conditionType = ftbCmnAccessor.getConditionType(oneConditionindex);
                if (ConditionType.HardwareDevice == conditionType)
                {
                    //typeRet = ConditionType.HardwareDevice;
                    //break;

                    //ftbCmnAccessor.isTcConditionSelected();//getConditionSelected(oneConditionindex)
                    continue;
                }
                else if (ConditionType.OptionSetting == conditionType)
                {
                    typeRet = ConditionType.OptionSetting;
                }
            }
            return typeRet;
        }
    }
}
