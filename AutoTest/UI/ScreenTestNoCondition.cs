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
using static Tool.UI.ScreenTest;

namespace Tool.UI
{
    public partial class ScreenTestNoCondition : Form
    {
        ScreenTest screenTest = null;
        List<ScreenInfoNoCondition> screenInfoNoConditionList = null;
        List<StandardScreen> screenSelectedModel = null;
        List<string> allUswords = null;
        List<StandardScreen> screenAllModel = null;
        DAL.IFTBCommonAPI treeMemory = null;
        int checkAllFlage = -1;
        bool fromOpinionSelectFormFlag;
        List<int> notNullRow= new List<int>();
        public ScreenTestNoCondition()
        {
            InitializeComponent();
        }
        public ScreenTestNoCondition(List<ScreenInfoNoCondition> screenInfoNoConditionList, object screenSelectedList, List<string> allWordsl, object screenAllModel, object treeMemory, bool selectFlag, ScreenTest screenTest)
        {
            this.screenTest = screenTest;
            this.treeMemory =(DAL.IFTBCommonAPI)treeMemory;
            this.screenAllModel = (List<StandardScreen>)screenAllModel;
            this.screenSelectedModel = (List<StandardScreen>)screenSelectedList;
            List<ScreenInfoNoCondition> filterBlacklist = screenItemListCheckBlackList(screenInfoNoConditionList);
            this.screenInfoNoConditionList = filterBlacklist;
            this.fromOpinionSelectFormFlag = selectFlag;
            this.allUswords = allWordsl;
            InitializeComponent();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //若是存在文件则从文件中读取，若没有则在Dal中读取
            if (!File.Exists(StaticEnvironInfo.getTotalStandardScreenFileName()))
            {
                this.dataGridView1.DataSource = filterBlacklist;
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewTextBoxCell pathTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                    pathTextBoxCell.ReadOnly = true;
                }
            }
            else
            {
                init();
            }
            //设置只有words才可以修改
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewTextBoxCell pathTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                pathTextBoxCell.ReadOnly = true;
            }
            if (this.screenAllModel.Count == 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    saveRow(this.screenAllModel, i);
                }
            }
        }

        private void ScreenTestNoCondition_Load(object sender, EventArgs e)
        {
            //dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            if (fromOpinionSelectFormFlag == true)
            {
                //set wordsCell(DataGridView) ReadOnly
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewTextBoxCell wordsCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Words"];
                    wordsCell.ReadOnly = true;
                }
            }
            //set Path(DataGridView) ReadOnly
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewTextBoxCell pathTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                pathTextBoxCell.ReadOnly = true;
            }
            //If you select the noconditionCheckBox in ScreenTest,when open it.All dataGridView1 will select.
            if (screenTest.noConditionFlag == 1)
            {
                this.dataGridView1.Columns[0].DefaultCellStyle.NullValue = true;
            }
            else
            {
                this.dataGridView1.Columns[0].DefaultCellStyle.NullValue = false;
            }
            if (fromOpinionSelectFormFlag == false)
            {
                this.dataGridView1.Columns[0].Visible = false;
                this.checkAll.Visible = false;
            }
            else
            {
                this.dataGridView1.Columns[0].Visible = true;
                this.checkAll.Visible = true;
            }
        }

        private List<ScreenInfoNoCondition> screenItemListCheckBlackList(List<ScreenInfoNoCondition> conditionInfoList)
        {
            List<ScreenInfoNoCondition> list = new List<ScreenInfoNoCondition>(conditionInfoList);
            List<ScreenInfoNoCondition> removeList = new List<ScreenInfoNoCondition>();
            removeList = conditionInfoList;
            TotalOpinionBlackList opinionBlackList = new TotalOpinionBlackList();
            opinionBlackList.loadOpinionBlackList(StaticEnvironInfo.getOpinionBlackListFullFileName());
            TestRuntimeAggregate.addOpinionBlackList(opinionBlackList.opinionBlackList);
            OpinionRunBlackListInfo black = TestRuntimeAggregate.getOpinionBlackList("FtbScreenItemListChecker");
            RunBlackList NABlackList = black.NABlackList;
            RunBlackList NTBlackList = black.NTBlackList;
            for (int i = 0; i < list.Count; i++)
            {
                if (NTBlackList != null && NTBlackList.blackList != null && NTBlackList.regulations != null
                    && NABlackList != null && NABlackList.blackList != null)
                {
                    if (NTBlackList != null && NTBlackList.blackList != null
                        && NTBlackList.blackList.Contains(list[i].Path))
                    {
                        removeList.Remove(list[i]);
                    }
                    if (NABlackList != null && NABlackList.blackList != null
                       && NABlackList.blackList.Contains(list[i].Path))
                    {
                        removeList.Remove(list[i]);
                    }

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

        //save按钮
        private void submit_Click(object sender, EventArgs e)
        {
            int checkFlag = -1;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["select"];
                DataGridViewTextBoxCell pathCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[1];
                DataGridViewTextBoxCell wordsCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[2];
                DataGridViewCheckBoxCell checkWords = this.dataGridView1.Rows[i].Cells["select"] as DataGridViewCheckBoxCell;
                if (wordsCell.Value == null)
                {
                    MessageBoxButtons mesgButton = MessageBoxButtons.OK;
                    string expMsg = string.Format("You Input null words [{0}]!!", wordsCell.Value);
                    DialogResult dr = MessageBox.Show(expMsg + "\nCheck InputWords corrected,Then InputWords again!!",
                        expMsg, mesgButton, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    checkWords.ReadOnly = false;
                    checkWords.Style.BackColor = Color.Red;
                    if (checkFlag != 0)
                    {
                        MessageBox.Show("Error,You Input null words!");
                    }
                    checkFlag = 0;
                    return;
                }
                //检查words是否符合规范
                try
                {
                    string[] checkWordsl = wordsCell.Value.ToString().Split(',');
                    for (int checkWordslIndex = 0; checkWordslIndex < checkWordsl.Length; checkWordslIndex++)
                    {
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
                    //若是不符合则变红
                    checkWords.ReadOnly = false;
                    checkWords.Style.BackColor = Color.Red;
                    if (checkFlag != 0)
                    {
                        MessageBox.Show("error");
                    }
                    checkFlag = 0;
                }
                if (Convert.ToBoolean(checkCell.Value))
                {
                    saveRow(screenSelectedModel, i);
                }
            }
            
            //no fault
            if (checkFlag == -1)
            {
                this.Close();
            }
            //if alert words,init screenAllModel again
            screenAllModel.Clear();
            if (this.screenAllModel.Count == 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    saveRow(this.screenAllModel, i);
                }
            }
        }
        //全选事件
        private void checkAll_Click(object sender, EventArgs e)
        {
            if (checkAllFlage== -1)
            {
                this.dataGridView1.Columns[0].DefaultCellStyle.NullValue =true;
                foreach (int i in notNullRow)
                {
                    DataGridViewCheckBoxCell checkCell=(DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["select"] ;
                    checkCell.Value = true;
                }
                checkAllFlage = 0;
            }
            else
            {
                foreach (int i in notNullRow)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["select"];
                    checkCell.Value = false;
                }
                this.dataGridView1.Columns[0].DefaultCellStyle.NullValue = false;
                checkAllFlage = -1;
            }
        }
        //保存当前行数据到screenTestModel
        private void saveRow(List<StandardScreen> screenTestModel, int i)
        {
            StandardScreen standardScreen = null;
            DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells["select"];
            DataGridViewTextBoxCell pathCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[1];
            DataGridViewTextBoxCell wordsCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[2];
            DataGridViewCheckBoxCell checkWords = this.dataGridView1.Rows[i].Cells["select"] as DataGridViewCheckBoxCell;
            bool pathFlage = false;
            int tempPathIndex = 0, specialPathCount = 1;
            standardScreen = new StandardScreen();
            standardScreen.condition.Add(0);
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
            screenTestModel.Add(standardScreen);

        }

        private void load_Click(object sender, EventArgs e)
        {
            init();
        }
        //dataGridView 初始化
        private void init()
        {
            ScreenMemoryCommonAggregate loadScreenTestModel = new ScreenMemoryCommonAggregate();
            if (File.Exists(StaticEnvironInfo.getTotalStandardScreenFileName()))
            {
                FileStream filestream = new FileStream(StaticEnvironInfo.getTotalStandardScreenFileName(), FileMode.Open);
                StreamReader streamReader = new StreamReader(filestream);
                string fileStr = streamReader.ReadToEnd();
                //json to object
                JavaScriptSerializer js = new JavaScriptSerializer();
                loadScreenTestModel = js.Deserialize<ScreenMemoryCommonAggregate>(fileStr); //Deserialize become dynamic model
                streamReader.Close();
                filestream.Close();
                List<ScreenInfoNoCondition> loadStandarScreen = new List<ScreenInfoNoCondition>();
                ScreenInfo firstScreenInfo = new ScreenInfo();
                foreach (StandardScreen standarScreen in loadScreenTestModel.standardScreen)
                {
                    ScreenInfoNoCondition screenInfo = new ScreenInfoNoCondition();
                    if (standarScreen.condition[0] != 0)
                    {
                        continue;
                    }
                    //add path
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
                    //add  words
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
                this.dataGridView1.DataSource = loadStandarScreen;
                //only words can write
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewTextBoxCell pathTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                    pathTextBoxCell.ReadOnly = true;

                }
            }
            else
            {
                MessageBox.Show("select file wrong!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //set pathTextBoxCell(DataGridView) ReadOnly
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewTextBoxCell pathTextBoxCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Path"];
                pathTextBoxCell.ReadOnly = true;
            }
            if (fromOpinionSelectFormFlag == true)
            {
                //set wordsCell(DataGridView) ReadOnly
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewTextBoxCell wordsCell = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells["Words"];
                    wordsCell.ReadOnly = true;
                }
            }
            //保存不为null的cell，在全选的时候遍历

            if (!notNullRow.Contains(e.RowIndex))
            {
                notNullRow.Add(e.RowIndex);
            }
        }
    }
}
