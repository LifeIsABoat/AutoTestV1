using Tool.BLL;
using Tool.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Tool.UI
{
    public partial class ScreenTest : Form
    {
        public class ScreenInfo
        {
            public string Condition { get; set; }
            public string ConditionWords { get; set; }
            public string Path { get; set; }
            public string Words { get; set; }
            public ScreenInfo()
            {
                Condition = "";
                Path = "";
                Words = "";
                ConditionWords = "";
            }
        }
        public class ScreenInfoNoCondition
        {
            public string Path { get; set; }
            public string Words { get; set; }
            public ScreenInfoNoCondition()
            {
                Path = "";
                Words = "";
            }
        }
        public int noConditionFlag = 0;
        List<string> totalPath = null;
        List<int> changedBox = new List<int>();
        Dictionary<int, List<int>> checkBoxChanged = new Dictionary<int, List<int>>();
        DAL.IFTBCommonAPI treeMemory = null;
        ScreenMemoryCommonAggregate screenTestModel = null;
        ScreenMemoryCommonAggregate screenAllModel = null;
        List<ScreenInfo> conditionInfoList = null;
        List<StandardScreen> noConditionStandardScreen = null;
        List<StandardScreen> AllStandardScreen = null;
        ScreenInfo screenInfo = null;
        //save no condition
        ScreenInfoNoCondition screenInfoNoCondition = null;
        List<ScreenInfoNoCondition> screenInfoNoConditionList = null;
        List<string> allUswords = null;
        ScreenTestNoCondition scrtestNoCondition = null;
        ModelInfo modelInfo = null;
        bool isCreate = false;
        public bool OpinionSelectFormFlag { get; set; }
        string TotalPath = "";
        //List<string> noConditionPath = null;
        public ScreenTest(object treeMemory)
        {
            this.treeMemory = (DAL.IFTBCommonAPI)treeMemory;
            this.treeMemory.importScreenDict();
            noConditionStandardScreen = new List<StandardScreen>();
            AllStandardScreen = new List<StandardScreen>();
            modelInfo = new ModelInfo();
            modelInfo.loadModelInfo(StaticEnvironInfo.getModelInfoFullFileName());

            conditionInfoList = new List<ScreenInfo>();
            screenInfo = new ScreenInfo();
            screenInfoNoCondition = new ScreenInfoNoCondition();
            screenInfoNoConditionList = new List<ScreenInfoNoCondition>();
            allUswords = new List<string>();
            InitializeComponent();
            isCreate = false;
            TotalPath = StaticEnvironInfo.getTotalStandardScreenFileName() + this.treeMemory.getSelectModel() + "-" + this.treeMemory.getSelectCountry() + ".txt";
        }
        private void ScreenTest_Load(object sender, EventArgs e)
        {
            screenTestModel = new ScreenMemoryCommonAggregate();
            screenAllModel = new ScreenMemoryCommonAggregate();
            if (isCreate == false)
            {
                //Set machine screen line number
                if (modelInfo.screenSize.w == 800 && modelInfo.screenSize.h == 480)
                {
                    screenTestModel.screenLines = 5;
                }
                else if (modelInfo.screenSize.w == 432 && modelInfo.screenSize.h == 240)
                {
                    screenTestModel.screenLines = 4;
                }
                else if (modelInfo.screenSize.w == 320 && modelInfo.screenSize.h == 240)
                {
                    screenTestModel.screenLines = 3;
                }
                //将所有信息存储在相对的类结构中，以便于转化为Json
                setScreenTestJson();
                //若是没有之前的文件，绑定setScreenTestJson读取的内容
                if (!File.Exists(TotalPath))
                {
                    this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    //filterBlacklist in screenItemListCheckBlackList according conditionInfoList
                    List<ScreenInfo> filterBlacklist = screenItemListCheckBlackList(conditionInfoList);
                    this.dataGridView1.DataSource = filterBlacklist;
                }
                //若有，判断这个文件符不符合规范，若不符合，绑定setScreenTestJson读取的内容
                else
                {
                    if(!init())
                    {
                        this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                        dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        //filterBlacklist in screenItemListCheckBlackList according conditionInfoList
                        List<ScreenInfo> filterBlacklist = screenItemListCheckBlackList(conditionInfoList);
                        this.dataGridView1.DataSource = filterBlacklist;
                    }
                }
                if (OpinionSelectFormFlag == true)
                {
                    //set wordsCell(DataGridView) ReadOnly
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        DataGridViewTextBoxCell wordsCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Words"];
                        wordsCell.ReadOnly = true;
                    }
                }
                //只有words列才可修改
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewTextBoxCell conditionTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Condition"];
                    DataGridViewTextBoxCell pathTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                    DataGridViewTextBoxCell ConditionTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["ConditionWords"];
                    conditionTextBoxCell.ReadOnly = true;
                    pathTextBoxCell.ReadOnly = true;
                    ConditionTextBoxCell.ReadOnly = true;
                }
                //init no conditon form 
            }
            //No Condition初始化，填充到NoCondition显示界面，点击事件发生，直接显示
            scrtestNoCondition = new ScreenTestNoCondition(screenInfoNoConditionList, noConditionStandardScreen, allUswords, AllStandardScreen, treeMemory, OpinionSelectFormFlag,this);
            isCreate = true;

            ////is checkBox show，隐藏NoCondition前面的选择框或者显示
            //if (selectFlag == false)
            //{
            //    this.dataGridView1.Columns[0].Visible = false;
            //}
            //else
            //{
            //    this.dataGridView1.Columns[0].Visible = true;
            //}
        }
        private void setScreenTestJson()
        {
            //添加第0行
            screenInfo.ConditionWords = "......";
            screenInfo.Path = ".......";
            screenInfo.Words = ".......";
            screenInfo.Condition = "0";
            conditionInfoList.Add(screenInfo);
            //获取所有测试画面
            totalPath = treeMemory.getTotalPath();
            //保存所有的words，在界面上修改words时拿来比对
            saveAllWords();

            //对每条TC进行信息的整合（以TC文单位进行处理）
            for (int index = 0; index < totalPath.Count; index++)
            {
                //获取每条TC的每个画面包含的Condition的索引
                List<int> levelConditionL = treeMemory.getLevelCondition(totalPath[index]);
                foreach (int levelConditionLIndex in levelConditionL)
                {
                    Dictionary<int, List<int>> subConditionDifferenceIndex = new Dictionary<int, List<int>>();
                    //过滤掉包含Option项的画面，测试Screen时Option项没有后一个画面。
                    if (treeMemory.isLevelOption(totalPath[index], levelConditionLIndex))
                    {
                        continue;
                    }
                    screenInfo = new ScreenInfo();
                    //拿到这个path的所有condition
                    for (int conditionIndex = 0; conditionIndex < treeMemory.getLevelButtonFrontCondition(totalPath[index], levelConditionLIndex).Count; conditionIndex++)
                    {
                        screenInfo.Condition = screenInfo.Condition + treeMemory.getLevelButtonFrontCondition(totalPath[index], levelConditionLIndex)[conditionIndex] + ",";
                    }

                    //找到此path的所有子节点的condition的索引，所有文言所对应的Condition。
                    List<int> conditionList = treeMemory.getLevelSubButtonCondition(totalPath[index], levelConditionLIndex);
                    //找到这个画面下所有拥有同样条件的words的下标
                    for (int subConditionIndex = 0; subConditionIndex < conditionList.Count; subConditionIndex++)
                    {
                        //每个Button所对应的Condition的索引
                        int subIndex = conditionList[subConditionIndex];
                        //将具有相同Condition的Button索引整合到一起
                        if (subConditionDifferenceIndex.ContainsKey(subIndex))
                        {
                            subConditionDifferenceIndex[subIndex].Add(subConditionIndex);
                            continue;
                        }
                        else
                        {
                            List<int> sameConditionIndex = new List<int>();
                            sameConditionIndex.Add(subConditionIndex);
                            subConditionDifferenceIndex.Add(subIndex, sameConditionIndex);
                        }
                    }
                    //add 0 rows，把No Condition项加入到Condition项中，构成完整的画面。
                    if (subConditionDifferenceIndex.ContainsKey(0))
                    {
                        foreach (KeyValuePair<int, List<int>> bif in subConditionDifferenceIndex)
                        {
                            if (bif.Key != 0)
                            {
                                for (int i = 0; i < subConditionDifferenceIndex[0].Count; i++)
                                {
                                    subConditionDifferenceIndex[bif.Key].Add(subConditionDifferenceIndex[0][i]);
                                }
                            }
                        }
                    }
                    string tempCondition = screenInfo.Condition;

                    foreach (KeyValuePair<int, List<int>> bif in subConditionDifferenceIndex)
                    {
                        screenInfo = new ScreenInfo();
                        //添加一条TC的路径（包含Condition项）
                        screenInfo.Path = totalPath[index];
                        screenInfo.Condition = tempCondition;
                        screenInfo.Words = "";
                        //将每个Condition包含的画面的文言进行排序
                        bif.Value.Sort();
                        bool screenLinesFla = false;
                        string repeatWords = "";
                        int lastRepeatWordsIndex = 0;
                        int fistRepeatWordsIndex = 0;
                        List<int> tempValue = new List<int>();
                        //把有相同condition的所有文言添加进tempValue
                        foreach (int ButtonWordsIndex in bif.Value)
                        {
                            //根据当前画面的路径和所持有的ConditionIndex取出所有Button文言，并根据ButtonWordsIndex取出对应控件的文言
                            string optionStr = treeMemory.getLevelSubButtonWords(totalPath[index], levelConditionLIndex)[ButtonWordsIndex].Replace("/", "//");
                            if (!totalPath.Contains(totalPath[index] + @"/" + optionStr))
                            {
                                continue;
                            }

                            //获取下一个画面所包含的所有Condition
                            List<int> subLevelConditionL = treeMemory.getLevelCondition(totalPath[index] + @"/" + optionStr);

                            foreach (int subLevelConditionLIndex in subLevelConditionL)
                            {
                                if (!treeMemory.isLevelButtonVaild(totalPath[index] + @"/" + optionStr, subLevelConditionLIndex))
                                {
                                    continue;
                                }
                            }
                            tempValue.Add(ButtonWordsIndex);
                        }
                        //根据画面上能显示的行数来生成words
                        if (tempValue.Count > screenTestModel.screenLines && (tempValue.Count % screenTestModel.screenLines) != 0)
                        {
                            screenLinesFla = true;
                            //find repeat words's index
                            lastRepeatWordsIndex = tempValue[tempValue.Count - (tempValue.Count % screenTestModel.screenLines) - 1];
                            fistRepeatWordsIndex = tempValue[tempValue.Count - screenTestModel.screenLines];
                        }
                        //生成根据画面固定行数来写words，比如行数为四，words："Server","Port","Auth. for SMTP","SSL/TLS","Port","Auth. for SMTP","SSL/TLS","Verify Cert."
                        foreach (int ButtonWordsIndex in tempValue)
                        {
                            string optionStr = treeMemory.getLevelSubButtonWords(totalPath[index], levelConditionLIndex)[ButtonWordsIndex].Replace("/", "//");
                            if (screenLinesFla && (fistRepeatWordsIndex <= ButtonWordsIndex && ButtonWordsIndex <= lastRepeatWordsIndex))
                            {
                                repeatWords = repeatWords + "\"" + setStr(treeMemory.getLevelSubButtonWords(totalPath[index], levelConditionLIndex)[ButtonWordsIndex]) + "\"" + ",";
                            }
                            screenInfo.Words = screenInfo.Words + "\"" + setStr(treeMemory.getLevelSubButtonWords(totalPath[index], levelConditionLIndex)[ButtonWordsIndex]) + "\"";
                            if (!screenLinesFla && ButtonWordsIndex != treeMemory.getLevelSubButtonWords(totalPath[index], levelConditionLIndex).Count - 1)
                            {
                                screenInfo.Words = screenInfo.Words + ",";
                            }
                            if (screenLinesFla)
                            {
                                if (ButtonWordsIndex != lastRepeatWordsIndex && ButtonWordsIndex != treeMemory.getLevelSubButtonWords(totalPath[index], levelConditionLIndex).Count - 1)
                                {
                                    screenInfo.Words = screenInfo.Words + ",";
                                }
                                else if (ButtonWordsIndex == lastRepeatWordsIndex)
                                {
                                    screenInfo.Words = screenInfo.Words + "," + repeatWords;
                                }
                            }
                        }
                        if (screenInfo.Words == "")
                        {
                            continue;
                        }
                        //添加condition
                        if (bif.Key != 0)
                        {
                            screenInfo.Condition = screenInfo.Condition + bif.Key + ",";
                        }

                        //delect last "," 
                        if (screenInfo.Words.LastIndexOf(",") == screenInfo.Words.Length - 1)
                        {
                            screenInfo.Words = screenInfo.Words.Substring(0, screenInfo.Words.Length - 1);
                        }

                        if (screenInfo.Words == "\"\"")
                        {
                            continue;
                        }
                        if (screenInfo.Condition != "")
                        {
                            screenInfo.Condition = screenInfo.Condition.Substring(0, screenInfo.Condition.Length - 1);
                            for (int conditionIndex = 0; conditionIndex < screenInfo.Condition.Split(',').Length; conditionIndex++)
                            {
                                screenInfo.ConditionWords = screenInfo.ConditionWords + screenInfo.Condition.Split(',')[conditionIndex] + "\r\n" + treeMemory.getTotalConditionList()[Convert.ToInt32(screenInfo.Condition.Split(',')[conditionIndex]) - 1] + "\r\n";
                            }
                            conditionInfoList.Add(screenInfo);
                        }
                        else if (screenInfo.Condition == "" && subConditionDifferenceIndex.Count > 1)
                        {
                            List<List<string>> testCondition = new List<List<string>>();
                            int[] conditions = new int[subConditionDifferenceIndex.Count - 1];
                            ////添加多个condition的各种情况组合情况
                            int keyValuePairIndex = 0;
                            foreach (KeyValuePair<int, List<int>> keyValuePair in subConditionDifferenceIndex)
                            {
                                if (keyValuePair.Key != 0)
                                {
                                    conditions[keyValuePairIndex] = keyValuePair.Key;
                                    keyValuePairIndex++;
                                    screenInfo.ConditionWords = screenInfo.ConditionWords + keyValuePair.Key + "\r\n" + treeMemory.getTotalConditionList()[keyValuePair.Key - 1] + "\r\n";
                                    //screenInfo.Condition = screenInfo.Condition + "!" + keyValuePair.Key + ",";
                                }
                            }
                            //获取condition组合
                            List<string> conditionStr = GetCombinationAll(conditions);
                            foreach (string condition in conditionStr)
                            {
                                ScreenInfo tempScreenInfo = new ScreenInfo();
                                tempScreenInfo.ConditionWords = screenInfo.ConditionWords;
                                tempScreenInfo.Path = screenInfo.Path;
                                tempScreenInfo.Words = screenInfo.Words;
                                string tempConditionStr = condition.Replace("!", "-");
                                tempScreenInfo.Condition = tempConditionStr;
                                conditionInfoList.Add(tempScreenInfo);
                            }

                            //screenInfo.Condition = screenInfo.Condition.Substring(0, screenInfo.Condition.Length - 1);
                        }
                        //condition 0
                        else
                        {
                            screenInfoNoCondition = new ScreenInfoNoCondition();
                            screenInfoNoCondition.Path = totalPath[index];
                            screenInfoNoCondition.Words = screenInfo.Words;
                            screenInfoNoConditionList.Add(screenInfoNoCondition);
                        }
                    }
                }
            }
        }

        private List<ScreenInfo> screenItemListCheckBlackList(List<ScreenInfo> conditionInfoList)
        {
            List<ScreenInfo> list = new List<ScreenInfo>(conditionInfoList);
            List<ScreenInfo> removeList = new List<ScreenInfo>();
            removeList = conditionInfoList;
            TotalOpinionBlackList opinionBlackList = new TotalOpinionBlackList();
            opinionBlackList.loadOpinionBlackList(StaticEnvironInfo.getOpinionBlackListFullFileName());
            TestRuntimeAggregate.addOpinionBlackList(opinionBlackList.opinionBlackList);
            OpinionRunBlackListInfo black = TestRuntimeAggregate.getOpinionBlackList("FtbScreenItemListChecker");
            RunBlackList NABlackList = black.NABlackList;
            RunBlackList NTBlackList = black.NTBlackList;
            for (int i = 1; i < list.Count; i++)
            {
                if (NTBlackList != null && NTBlackList.blackList != null && NTBlackList.regulations != null
                    && NABlackList != null && NABlackList.blackList != null)
                {
                    string[] subString = list[i].Words.Split(',');
                    foreach (string pattren in NTBlackList.regulations)
                    {
                        if (Regex.IsMatch(subString[0].Replace("\"", ""), pattren, RegexOptions.IgnoreCase))
                        {
                            removeList.Remove(list[i]);
                        }
                    }
                }
            }
            return removeList;
        }

        //提交事件
        private void submit_Click(object sender, EventArgs e)
        {
            screenTestModel.conditionList.Add("no condition");
            screenAllModel.conditionList.Add("no condition");
            //添加json文件中的conditionList
            for (int conditionIndex = 0; conditionIndex < treeMemory.getTotalConditionList().Count; conditionIndex++)
            {
                screenTestModel.conditionList.Add(treeMemory.getTotalConditionList()[conditionIndex]);
                screenAllModel.conditionList.Add(treeMemory.getTotalConditionList()[conditionIndex]);
            }
            dataGridView1.EndEdit();
            int checkFlag = -1;
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["select"];
                DataGridViewTextBoxCell conditionCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Condition"];
                DataGridViewTextBoxCell pathCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                DataGridViewTextBoxCell wordsCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Words"];
                DataGridViewCheckBoxCell checkWords = this.dataGridView1.Rows[i].Cells["select"] as DataGridViewCheckBoxCell;
                //检查所有项的words是否正确，不正确的行变红
                try
                {
                    if (wordsCell.Value == null)
                    {
                        MessageBoxButtons mesgButton = MessageBoxButtons.OK;
                        string expMsg = string.Format("You Input null words [{0}]!!", wordsCell.Value);
                        DialogResult dr = MessageBox.Show(expMsg + "\nCheck InputWords corrected,Then InputWords again!!",
                            expMsg, mesgButton, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        return;
                    }
                    //分割当前行的words
                    string[] checkWordsl = wordsCell.Value.ToString().Split(',');
                    for (int checkWordslIndex = 0; checkWordslIndex < checkWordsl.Length; checkWordslIndex++)
                    {
                        //if uswords had ','
                        if (checkWordsl[checkWordslIndex].LastIndexOf('\"') != checkWordsl[checkWordslIndex].Length - 1)
                        {
                            checkWordsl[checkWordslIndex + 1] = checkWordsl[checkWordslIndex] + ',' + checkWordsl[checkWordslIndex + 1];
                            checkWordslIndex++;
                            if (checkWordsl[checkWordslIndex].LastIndexOf('\"') != checkWordsl[checkWordslIndex].Length - 1)
                            {
                                continue;
                            }
                        }
                        checkWordsl[checkWordslIndex] = checkWordsl[checkWordslIndex].Substring(1, checkWordsl[checkWordslIndex].Length - 2);
                        if (checkWordsl[checkWordslIndex] != "")
                        {
                            for (int allWordsIndex = 0; allWordsIndex < allUswords.Count; allWordsIndex++)
                            {
                                if (checkWordsl[checkWordslIndex] == allUswords[allWordsIndex])
                                {
                                    break;
                                }
                                else if (allWordsIndex == allUswords.Count - 1)
                                {
                                    throw new Exception("");
                                }
                            }
                        }
                    }
                    checkWords.ReadOnly = false;
                    checkWords.Style.BackColor = Color.White;
                }
                catch
                {
                    checkWords.ReadOnly = false;
                    checkWords.Style.BackColor = Color.Red;
                    if (checkFlag != 0)
                    {
                        MessageBox.Show("error");
                    }
                    checkFlag = 0;
                }
                //保存数据到TotalStandardScreen
                saveRow(screenAllModel, i);
                //保存选择的行
                if (Convert.ToBoolean(checkCell.EditingCellFormattedValue))
                {
                    saveRow(screenTestModel, i);
                }
            }

            //若是第0行 no condtiotn 被勾选，保存所有no condition项
            if (Convert.ToBoolean(this.dataGridView1.Rows[0].Cells["select"].Value))
            {
                for (int AllStandardScreenIndex = 0; AllStandardScreenIndex < AllStandardScreen.Count; AllStandardScreenIndex++)
                {
                    if (AllStandardScreen[AllStandardScreenIndex].condition[0] == 0)
                    {
                        screenTestModel.standardScreen.Add(AllStandardScreen[AllStandardScreenIndex]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < noConditionStandardScreen.Count; i++)
                {
                    screenTestModel.standardScreen.Add(noConditionStandardScreen[i]);
                }
            }
            //文件写入
            if (checkFlag == -1)
            {
                string jsonText = "";
                jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(screenTestModel);
                FileStream fs = null;
                StreamWriter sw = null;
                if (OpinionSelectFormFlag != false)
                {
                    fs = new FileStream(StaticEnvironInfo.getSelectedStandardScreenFileName(), FileMode.Create, FileAccess.Write);
                    fs.Close();
                    sw = new StreamWriter(StaticEnvironInfo.getSelectedStandardScreenFileName(), true);
                    sw.Write("\r\n" + jsonText + "\r\n");
                    sw.Flush();
                    sw.Close();
                    noConditionStandardScreen.Clear();
                }

                for (int index = 0; index < AllStandardScreen.Count; index++)
                {
                    screenAllModel.standardScreen.Add(AllStandardScreen[index]);
                }
                fs = new FileStream(TotalPath, FileMode.Create, FileAccess.Write);
                fs.Close();
                sw = new StreamWriter(TotalPath, true);
                jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(screenAllModel);
                sw.Write("\r\n" + jsonText + "\r\n");
                sw.Flush();
                sw.Close();
                AllStandardScreen.Clear();
                //treeMemory.setStandardScreen(screenTestModel);
                this.Close();
            }
        }
        //若是words中有\则变为\\
        private string setStr(string model)
        {
            string buttonWords = model;
            if (buttonWords.Split('\\').Length > 1)
            {
                string tempButtonWords = "";
                for (int index = 0; index < buttonWords.Split('\\').Length; index++)
                {
                    tempButtonWords = tempButtonWords + buttonWords.Split('\\')[index] + "\\\\";
                    if (index == buttonWords.Split('\\').Length - 1)
                    {
                        tempButtonWords = tempButtonWords.Substring(0, tempButtonWords.Length - 2);
                    }
                }
                buttonWords = tempButtonWords;
            }
            return buttonWords;
        }
        //把选择的项保存到screenTestModel之中
        private void saveRow(ScreenMemoryCommonAggregate screenTestModel, int i)
        {
            StandardScreen standardScreen = null;
            DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["select"];
            DataGridViewTextBoxCell conditionCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Condition"];
            DataGridViewTextBoxCell pathCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
            DataGridViewTextBoxCell wordsCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Words"];
            DataGridViewCheckBoxCell checkWords = this.dataGridView1.Rows[i].Cells["select"] as DataGridViewCheckBoxCell;
            bool pathFlage = false;
            int tempPathIndex = 0, specialPathCount = 1;
            standardScreen = new StandardScreen();
            for (int conditionIndex = 0; conditionIndex < conditionCell.Value.ToString().Split(',').Length; conditionIndex++)
            {
                if (conditionCell.Value.ToString().Split(',')[conditionIndex].Contains("!"))
                {
                    standardScreen.condition.Add(Convert.ToInt32("-" + conditionCell.Value.ToString().Split(',')[conditionIndex].Split('!')[1]));
                }
                else
                {
                    standardScreen.condition.Add(Convert.ToInt32(conditionCell.Value.ToString().Split(',')[conditionIndex]));
                }
            }
            for (int pathIndex = 0; pathIndex < pathCell.Value.ToString().Split('/').Length; pathIndex++)
            {
                if (pathCell.Value.ToString().Split('/')[pathIndex] == "" && pathIndex != 0 && pathIndex != pathCell.Value.ToString().Split('/').Length - 1)
                {
                    if (tempPathIndex == 0)
                    {
                        tempPathIndex = pathIndex - specialPathCount;
                        specialPathCount = specialPathCount + 2;
                    }
                    standardScreen.path[tempPathIndex] = standardScreen.path[tempPathIndex] + "/";
                    pathFlage = true;
                }
                else if (pathFlage)
                {
                    standardScreen.path[tempPathIndex] = standardScreen.path[tempPathIndex] + pathCell.Value.ToString().Split('/')[pathIndex];
                    tempPathIndex = 0;
                    pathFlage = false;
                }
                else
                {
                    standardScreen.path.Add(pathCell.Value.ToString().Split('/')[pathIndex]);
                }
            }
            for (int wordsIndex = 0; wordsIndex < wordsCell.Value.ToString().Split(',').Length; wordsIndex++)
            {
                if (!wordsCell.Value.ToString().Split(',')[wordsIndex].Contains("\""))
                {
                    continue;
                }
                if (wordsCell.Value.ToString().Split(',')[wordsIndex].IndexOf("\"") == wordsCell.Value.ToString().Split(',')[wordsIndex].LastIndexOf("\""))
                {
                    standardScreen.words.Add(wordsCell.Value.ToString().Split(',')[wordsIndex] + ',' + wordsCell.Value.ToString().Split(',')[wordsIndex + 1]);
                    wordsIndex++;
                    continue;
                }
                else
                {
                    standardScreen.words.Add(wordsCell.Value.ToString().Split(',')[wordsIndex]);
                }
            }
            for (int standardScreenIndex = 0; standardScreenIndex < standardScreen.words.Count; standardScreenIndex++)
            {
                standardScreen.words[standardScreenIndex] = standardScreen.words[standardScreenIndex].Substring(1, standardScreen.words[standardScreenIndex].Length - 2);
            }
            screenTestModel.standardScreen.Add(standardScreen);
        }
        //show 没有condition的界面
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex >= 2)
            {
                //如果nocondition已勾选，则noCondition画面打开时所有项都已勾选
                DataGridViewCheckBoxCell checkBox = this.dataGridView1.Rows[0].Cells["select"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(checkBox.EditingCellFormattedValue))
                {
                    noConditionFlag = 1;
                }
                else
                {
                    noConditionFlag = 0;
                }
                //noConditionStandardScreen = new List<StandardScreen>();
                noConditionStandardScreen.Clear();
                scrtestNoCondition.ShowDialog();
            }
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("dk....." + e.RowIndex);
            changeColor(e.RowIndex, e.ColumnIndex);
        }
        //
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OpinionSelectFormFlag == true)
            {
                //set wordsCell(DataGridView) ReadOnly
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewTextBoxCell wordsCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Words"];
                    wordsCell.ReadOnly = true;
                }
            }
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewTextBoxCell conditionTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Condition"];
                DataGridViewTextBoxCell pathTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                DataGridViewTextBoxCell ConditionTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["ConditionWords"];
                conditionTextBoxCell.ReadOnly = true;
                pathTextBoxCell.ReadOnly = true;
                ConditionTextBoxCell.ReadOnly = true;
            }
            //排序
            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {
                ScreenInfo tempScreen = new ScreenInfo();
                for (int i = 1; i < conditionInfoList.Count; i++)
                {
                    for (int j = i; j < conditionInfoList.Count; j++)
                    {
                        int tempCondition = 0;
                        int tempCondition1 = 0;
                        if (conditionInfoList[j].Condition.Split(',')[0].Contains("!"))
                        {
                            tempCondition = Convert.ToInt32(conditionInfoList[j].Condition.Split(',')[0].Replace("!", ""));
                        }
                        else
                        {
                            tempCondition = Convert.ToInt32(conditionInfoList[j].Condition.Split(',')[0]);
                        }
                        if (conditionInfoList[i].Condition.Split(',')[0].Contains("!"))
                        {
                            tempCondition1 = Convert.ToInt32(conditionInfoList[i].Condition.Split(',')[0].Replace("!", ""));
                        }
                        else
                        {
                            tempCondition1 = Convert.ToInt32(conditionInfoList[i].Condition.Split(',')[0]);
                        }
                        if (tempCondition1 > tempCondition)
                        {
                            tempScreen = conditionInfoList[i];
                            conditionInfoList[i] = conditionInfoList[j];
                            conditionInfoList[j] = tempScreen;
                        }
                    }
                }
                for (int conditionInfoIndex = 0; conditionInfoIndex < conditionInfoList.Count; conditionInfoIndex++)
                {
                    conditionInfoList[conditionInfoIndex].Condition = conditionInfoList[conditionInfoIndex].Condition.Replace("!", "-");
                }
                List<ScreenInfo> filterBlacklist = screenItemListCheckBlackList(conditionInfoList);
                this.dataGridView1.DataSource = filterBlacklist;
            }
            //若点击select按钮，则进行是否改变颜色的判断
            else if (e.ColumnIndex == 0 && e.RowIndex > 0)
            {
                changeColor(e.RowIndex, e.ColumnIndex);
            }
        }
        //load saved text file
        private void load_Click(object sender, EventArgs e)
        {
            init();
        }
        private bool init()
        {
            ScreenMemoryCommonAggregate loadScreenTestModel = new ScreenMemoryCommonAggregate();
            FileStream filestream = new FileStream(TotalPath, FileMode.Open);
            StreamReader streamReader = new StreamReader(filestream);
            string fileStr = streamReader.ReadToEnd();
            //若有文件，把Json转为Object
            JavaScriptSerializer js = new JavaScriptSerializer();
            loadScreenTestModel = js.Deserialize<ScreenMemoryCommonAggregate>(fileStr); //Deserialize become dynamic model
            streamReader.Close();
            filestream.Close();
            //检查其的words是否符合
            bool matchFlag = false;
            for (int standardScreenIndex = 0; standardScreenIndex < loadScreenTestModel.standardScreen.Count; standardScreenIndex++)
            {
                string pathStr = "";
                //检查condition
                for (int conditionIndex = 0; conditionIndex < loadScreenTestModel.standardScreen[standardScreenIndex].condition.Count; conditionIndex++)
                {
                    if (loadScreenTestModel.standardScreen[standardScreenIndex].condition[conditionIndex] < 0)
                    {
                        if ((0 - loadScreenTestModel.standardScreen[standardScreenIndex].condition[conditionIndex]) > treeMemory.getTotalConditionList().Count)
                        {
                            matchFlag = true;
                            break;
                        }
                    }
                    else
                    {
                        if (loadScreenTestModel.standardScreen[standardScreenIndex].condition[conditionIndex] > treeMemory.getTotalConditionList().Count)
                        {
                            matchFlag = true;
                            break;
                        }
                    }
                }
                //检查path
                foreach (string path in loadScreenTestModel.standardScreen[standardScreenIndex].path)
                {
                    string tempPath = "";
                    tempPath = path.Replace("/", "//");
                    pathStr = pathStr + tempPath + "/";
                }
                pathStr = pathStr.Substring(0, pathStr.Length - 1);
                if (!treeMemory.getTotalPath().Contains(pathStr))
                {
                    matchFlag = true;
                    break;
                }
                //检查 words
                foreach (string words in loadScreenTestModel.standardScreen[standardScreenIndex].words)
                {
                    if (!allUswords.Contains(words))
                    {
                        matchFlag = true;
                        break;
                    }
                }
                if (matchFlag)
                {
                    break;
                }
            }
            //若是文件不符合，则重新读取
            if (!matchFlag)
            {
                List<ScreenInfo> loadStandarScreen = new List<ScreenInfo>();
                ScreenInfo firstScreenInfo = new ScreenInfo();
                firstScreenInfo.Condition = "0";
                firstScreenInfo.ConditionWords = "......";
                firstScreenInfo.Path = "......";
                firstScreenInfo.Words = "......";
                loadStandarScreen.Add(firstScreenInfo);
                foreach (StandardScreen standarScreen in loadScreenTestModel.standardScreen)
                {
                    ScreenInfo screenInfo = new ScreenInfo();
                    if (standarScreen.condition[0] == 0)
                    {
                        continue;
                    }
                    //添加condition和conditionStr
                    for (int conditionIndex = 0; conditionIndex < standarScreen.condition.Count; conditionIndex++)
                    {
                        if (conditionIndex == standarScreen.condition.Count - 1)
                        {
                            screenInfo.Condition = screenInfo.Condition + standarScreen.condition[conditionIndex] + "";
                            if (standarScreen.condition[conditionIndex] < 0)
                            {
                                screenInfo.ConditionWords = screenInfo.ConditionWords + (0 - standarScreen.condition[conditionIndex]) + "\r\n" + treeMemory.getTotalCondition(0 - standarScreen.condition[conditionIndex]);
                            }
                            else
                            {
                                screenInfo.ConditionWords = screenInfo.ConditionWords + standarScreen.condition[conditionIndex] + "\r\n" + treeMemory.getTotalCondition(standarScreen.condition[conditionIndex]);
                            }
                        }
                        //若不是最后一个condition，则加换行
                        else
                        {
                            screenInfo.Condition = screenInfo.Condition + standarScreen.condition[conditionIndex] + ",";
                            if (standarScreen.condition[conditionIndex] < 0)
                            {
                                screenInfo.ConditionWords = screenInfo.ConditionWords + (0 - standarScreen.condition[conditionIndex]) + "\r\n" + treeMemory.getTotalCondition(0 - standarScreen.condition[conditionIndex]) + "\r\n";
                            }
                            else
                            {
                                screenInfo.ConditionWords = screenInfo.ConditionWords + standarScreen.condition[conditionIndex] + "\r\n" + treeMemory.getTotalCondition(standarScreen.condition[conditionIndex]) + "\r\n";
                            }
                        }
                    }
                    //添加path
                    for (int pathIndex = 0; pathIndex < standarScreen.path.Count; pathIndex++)
                    {
                        standarScreen.path[pathIndex] = standarScreen.path[pathIndex].Replace("/", "//");
                        if (pathIndex == standarScreen.path.Count - 1)
                        {
                            screenInfo.Path = screenInfo.Path + standarScreen.path[pathIndex];
                        }
                        else
                        {
                            screenInfo.Path = screenInfo.Path + standarScreen.path[pathIndex] + "/";
                        }
                    }
                    //添加words
                    for (int wordIndex = 0; wordIndex < standarScreen.words.Count; wordIndex++)
                    {
                        if (wordIndex == standarScreen.words.Count - 1)
                        {
                            screenInfo.Words = screenInfo.Words + @"""" + standarScreen.words[wordIndex] + @"""";
                        }
                        else
                        {
                            screenInfo.Words = screenInfo.Words + @"""" + standarScreen.words[wordIndex] + @"""" + ",";
                        }
                    }
                    loadStandarScreen.Add(screenInfo);
                }//end foreach
                //filterBlacklist in screenItemListCheckBlackList according conditionInfoList
                List<ScreenInfo> filterBlacklist = screenItemListCheckBlackList(loadStandarScreen);
                this.dataGridView1.DataSource = filterBlacklist;
                //设置只有words列才能被修改
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewTextBoxCell conditionTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Condition"];
                    DataGridViewTextBoxCell pathTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                    DataGridViewTextBoxCell ConditionTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["ConditionWords"];
                    conditionTextBoxCell.ReadOnly = true;
                    pathTextBoxCell.ReadOnly = true;
                    ConditionTextBoxCell.ReadOnly = true;
                }
                this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                return true;
            }
            else
            {
                MessageBox.Show("text error!");
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //勾选变灰
        private void changeColor(int row,int col)
        {
            if (row != -1)
            {
                DataGridViewCheckBoxCell checkBox = this.dataGridView1.Rows[row].Cells["select"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(checkBox.EditingCellFormattedValue))
                {
                    if (col == 0 && row != -1)
                    {
                        changedBox = new List<int>();
                        List<int> conditionIndex = new List<int>();
                        string pathStr = this.dataGridView1.Rows[row].Cells["Path"].Value.ToString();
                        string conditionStr = this.dataGridView1.Rows[row].Cells["Condition"].Value.ToString();
                        string[] conditionStrList = conditionStr.Split(',');
                        //if condition is !
                        List<string> notConditionList = new List<string>();
                        List<string> positiveConditionList = new List<string>();
                        for (int conditionStrListIndex = 0; conditionStrListIndex < conditionStrList.Length; conditionStrListIndex++)
                        {
                            if (conditionStrList[conditionStrListIndex].Contains("-"))
                            {
                                notConditionList.Add(conditionStrList[conditionStrListIndex].Replace("-", ""));
                            }
                            else
                            {
                                positiveConditionList.Add(conditionStrList[conditionStrListIndex]);
                            }
                        }
                        int lastIndex = pathStr.LastIndexOf("/");
                        if (lastIndex != -1)
                        {
                            pathStr = pathStr.Substring(0, lastIndex);
                            for (int i = 1; i < dataGridView1.Rows.Count; i++)
                            {
                                //changedBoxFlag = -1;
                                DataGridViewTextBoxCell conditionCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Condition"];
                                string path = this.dataGridView1.Rows[i].Cells["Path"].Value.ToString();
                                string[] conditionCellList = conditionCell.Value.ToString().Split(',');
                                List<int> sameConditionBrother = new List<int>();
                            }
                            for (int i = 1; i < dataGridView1.Rows.Count; i++)
                            {
                                DataGridViewTextBoxCell conditionCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Condition"];
                                bool flag = false;
                                string[] conditionCellList = conditionCell.Value.ToString().Split(',');

                                //if node 14 brother node condition is 46,condition 46 change grey
                                for (int conditionCellListIndex = 0; conditionCellListIndex < conditionCellList.Length; conditionCellListIndex++)
                                {
                                    //if 14,condition !14 change grey
                                    for (int conditionStrListIndex = 0; conditionStrListIndex < conditionStrList.Length; conditionStrListIndex++)
                                    {
                                        if (!conditionStrList[conditionStrListIndex].Contains("-") && conditionCellList[conditionCellListIndex].Contains("-"))
                                        {
                                            string tempCondition = conditionCellList[conditionCellListIndex].Replace("-", "");
                                            if (tempCondition.Equals(conditionStrList[conditionStrListIndex]))
                                            {
                                                flag = true;
                                                break;
                                            }
                                        }
                                    }
                                    //if condition is !14,condition 14change grey
                                    for (int notConditionListIndex = 0; notConditionListIndex < notConditionList.Count; notConditionListIndex++)
                                    {
                                        if (conditionCellList[conditionCellListIndex] == notConditionList[notConditionListIndex])
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    if (flag)
                                    {
                                        break;
                                    }
                                }
                                if (flag || changedBox.Contains(i))
                                {
                                    DataGridViewCheckBoxCell check = this.dataGridView1.Rows[i].Cells["select"] as DataGridViewCheckBoxCell;
                                    if (!changedBox.Contains(i))
                                    {
                                        changedBox.Add(i);
                                    }
                                    check.Value = false;
                                    check.ReadOnly = true;
                                    check.Style.BackColor = Color.Gray;
                                    // flag = false;
                                }
                            }
                        }
                    }
                    //save changed cell
                    if (!checkBoxChanged.ContainsKey(row))
                    {
                        checkBoxChanged.Add(row, changedBox);
                    }
                    else
                    {
                        for (int index = 0; index < changedBox.Count; index++)
                        {
                            if (!checkBoxChanged[row].Contains(changedBox[index]))
                            {
                                checkBoxChanged[row].Add(changedBox[index]);
                            }
                        }
                    }
                }
                else if (checkBox.ReadOnly==false)
                {
                    if (checkBoxChanged.ContainsKey(row))
                    {
                        changedBox = checkBoxChanged[row];
                    }
                    List<int> removeRow = new List<int>();
                    foreach (int i in changedBox)
                    {
                        bool flag = false;
                        //如果与所有勾选的项都不冲突则变白
                        foreach (KeyValuePair<int, List<int>> kev in checkBoxChanged)
                        {
                            if (kev.Key == row)
                            {
                                continue;
                            }
                            if (kev.Value.Contains(i))
                            {
                                flag = true;
                                break;
                            }
                        }
                        removeRow.Add(i);
                        if (!flag)
                        {
                            DataGridViewCheckBoxCell check = this.dataGridView1.Rows[i].Cells["select"] as DataGridViewCheckBoxCell;
                            check.ReadOnly = false;
                            check.Style.BackColor = Color.White;
                        }
                    }
                    //删除取消勾选项中的冲突项
                    for (int index = 0; index < removeRow.Count; index++)
                    {
                        checkBoxChanged[row].Remove(removeRow[index]);
                    }
                }
            }
        }
        //保存所有的words项，在修改保存后拿来比对修改后的words是否符合要求
        private void saveAllWords()
        {
            allUswords.Add("");
            for (int totalPathIndex = 0; totalPathIndex < totalPath.Count; totalPathIndex++)
            {
                List<int> levelConditionL = treeMemory.getLevelCondition(totalPath[totalPathIndex]);
                for (int levelConditionLIndex = 0; levelConditionLIndex < levelConditionL.Count; levelConditionLIndex++)
                {
                    List<string> tempAllWords = treeMemory.getLevelSubButtonWords(totalPath[totalPathIndex], levelConditionL[levelConditionLIndex]);
                    for (int i = 0; i < tempAllWords.Count; i++)
                    {
                        if (tempAllWords[i] != "")
                            allUswords.Add(tempAllWords[i]);
                    }
                }
            }
        }
        //Permutation and combination of correlation functions
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        private static void GetCombination(ref List<int[]> list, int[] t, int n, int m, int[] b, int M)
        {
            for (int i = n; i >= m; i--)
            {
                b[m - 1] = i - 1;
                if (m > 1)
                {
                    GetCombination(ref list, t, i - 1, m - 1, b, M);
                }
                else
                {
                    if (list == null)
                    {
                        list = new List<int[]>();
                    }
                    int[] temp = new int[M];
                    for (int j = 0; j < b.Length; j++)
                    {
                        temp[j] = t[b[j]];
                    }
                    list.Add(temp);
                }
            }
        }

        private static List<List<int>> GetCombination(int[] t, int n)
        {
            if (t.Length < n)
            {
                return null;
            }
            List<List<int>> listResult = new List<List<int>>();

            List<int[]> list = new List<int[]>();
            int[] temp = new int[n];
            GetCombination(ref list, t, t.Length, n, temp, n);
            foreach (int[] listn in list)
            {
                List<int> tmp = listn.ToList();
                foreach (int tn in t)
                {
                    if (!listn.Contains(tn))
                        tmp.Add(-tn);
                }
                listResult.Add(tmp);
            }
            return listResult;
        }

        public static List<string> GetCombinationAll(int[] t)
        {
            List<string> listResult = new List<string>();

            for (int i = 1; i <= t.Length; i++)
            {
                List<List<int>> tmp = new List<List<int>>();
                tmp = GetCombination(t, i);
                foreach (List<int> tmpn in tmp)
                {
                    string str = string.Join(",", tmpn.ToArray());
                    listResult.Add(str.Replace("-", "!"));
                }
            }

            listResult.Add("!" + string.Join(",!", t));

            return listResult;
        }
    }
}
