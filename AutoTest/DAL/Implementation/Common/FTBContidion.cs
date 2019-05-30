using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool.DAL
{
   /*
    *  Description:Condition Info
    */
    public enum ConditionType
    {
        NoCondition,
        OptionSetting,
        HardwareDevice
    }
    public class ConditionInfo
    {
        public const int _INVALIDTCINDEX = -1;
        
        public string str;
        public ConditionType type;
        public int tcIndex;
        public bool isSelected;

        public ConditionInfo(string str, ConditionType type)
        {
            this.str = str;
            this.type = type;   
            this.tcIndex = _INVALIDTCINDEX;
            this.isSelected = false;

        }
    }
    public class Contidion
    {
        private static List<ConditionInfo> conditionsList = new List<ConditionInfo>();
        //if this cell no condition ,condition index is _NOCONDITION
        public const int _NOCONDITION = 0;
        public const string _NOCONDITIONSTR = "no condition";
        public int conditionIndex { get; set; }

        public Contidion(int conditionIndex = Contidion._NOCONDITION) 
        {
            this.conditionIndex = conditionIndex;
        }
       /* 
        *  Description:Add condition in condition list
        *  Param:conditions_list - Save all the condition's list
        *  Param:condition_word - condition's word
        *  Return:
        *  Exception:
        *  Example:
        */
        public static void addConditionList(string conditionWord)
        {
            ConditionType type = ConditionType.HardwareDevice;
            if (conditionsList.Count == 0)
            {
                if (conditionWord != _NOCONDITIONSTR)
                {
                    throw new FTBAutoTestException("first condion string is not 'no condition'");
                }
                else
                {
                    type = ConditionType.NoCondition;
                }
            }
            ConditionInfo OneCondition = new ConditionInfo(conditionWord, type);
            conditionsList.Add(OneCondition);
        }

        /* 
        *  Description:Get condition
        *  Param:conditions_list - Save all the condition's list
        *  Param:index - condition's index
        *  Return:string
        *  Exception:FTBAutoException
        *  Example:
        */
        public static string getCondition(int index)
        {
            if (index >= 0 && index < conditionsList.Count)
            {
                return conditionsList[index].str;
            }
            else
            {
                throw new FTBAutoTestException("index out of range or not a Positive");
            }
        }

        /* 
        *  Description:Get condition
        *  Param:conditions_list - Save all the condition's list
        *  Param:index - condition's index
        *  Return:string
        *  Exception:FTBAutoException
        *  Example:
        */
        public static List<string> getConditionList()
        {
            List<string> AllconditionList = new List<string>();
            foreach (ConditionInfo oneCondition in conditionsList)
            {
                if (oneCondition.type == ConditionType.NoCondition)
                {
                    continue;
                }
                AllconditionList.Add(oneCondition.str);
            }
            return AllconditionList;               
        }

        public static void clearAllConditionType()
        {
            foreach (ConditionInfo oneCondition in conditionsList)
            {
                if (oneCondition.type != ConditionType.NoCondition)
                {                
                    oneCondition.type = ConditionType.HardwareDevice;
                }
            }
        }

        public static void clearAllConditionTcIndex()
        {
            foreach (ConditionInfo oneCondition in conditionsList)
            {
                oneCondition.tcIndex = ConditionInfo._INVALIDTCINDEX;
            }
        }

        public static void clearAllConditionSelectedStatus()
        {
            foreach (ConditionInfo oneCondition in conditionsList)
            {
                oneCondition.isSelected = false;
            }
        }

        public static void setConditionTypeToHardwareDevice(int index)
        {
            conditionsList[index].type = ConditionType.HardwareDevice;
        }

        public static void setConditionTypeToOptionSetting(int index, int MatchTCIndex)
        {
            conditionsList[index].type = ConditionType.OptionSetting;
            conditionsList[index].tcIndex = MatchTCIndex;
            Console.WriteLine("setConditionTypeToOptionSetting {0}", conditionsList[index].str);
        }

        public static void selectOptionSettingCondition()
        {
            foreach (ConditionInfo oneCondition in conditionsList)
            {
                if (oneCondition.type == ConditionType.OptionSetting)
                {
                    oneCondition.isSelected = true;
                    Console.WriteLine("selectOptionSettingCondition {0}", oneCondition.str);
                }
            }
        }

        public static void selectHardwareDeviceCondition(int conditionIndex)
        {
            ConditionInfo oneCondition = conditionsList[conditionIndex];            
            if (oneCondition.type == ConditionType.HardwareDevice)
            {
                oneCondition.isSelected = true;
            }
        }

        public static void selectNoCondition()
        {
            foreach (ConditionInfo oneCondition in conditionsList)
            {
                if (oneCondition.type == ConditionType.NoCondition)
                {
                    oneCondition.isSelected = true;
                }
            }
        }

        public static bool getConditionSelected(int index)
        {
            return conditionsList[index].isSelected;
        }

        public static int getConditionTcIndex(int index)
        {
            return conditionsList[index].tcIndex;
        }

        public static ConditionType getConditionType(int index)
        {
            return conditionsList[index].type;
        }

        /* 
        *  Description:Get TcIndex  through the string of condition
        *  Param:condition - Save a condition's list
        *  Param:index - condition's index
        *  Return:Tc index
        *  Exception:FTBAutoException
        *  Example:
        */
        public static int getTcIndex(string condition)
        {
            int index = 0;
            for (int i = 0;i < conditionsList.Count;i++)
            {
                if (conditionsList[i].str == condition && conditionsList[i].type == ConditionType.OptionSetting)
                {
                    index = i;
                    break;
                }
            }
            return conditionsList[index].tcIndex;
        }
    }
}
