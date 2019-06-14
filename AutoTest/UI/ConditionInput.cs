using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool.DAL;
using System.IO;
using Tool.Engine;

namespace Tool.UI
{
    public struct TCWord
    {
        //public string head;
        public List<string> oneTCWord;
    }

    public struct AllTCWord
    {
        //public string TCListName;
        public List<TCWord> TCstrList;
    }

    public struct TCStrIndex
    {
        //public string head;
        public List<string> oneTCList;
        //public int index;
    }


    public partial class ConditionInput : Form
    {
        private const int _INVALIDTCINDEX = -1;
        private string tcFileName;

        private IFTBCommonAPI tree;

        ConditionSelect selectForm;
        //AllTC List
        List<TCStrIndex> allTCItem;

        //condition list
        List<string> AllConditionList;

        //进程间通讯类
        //CommunicateToPython chatopy;
        EngineDocument chatopy;

        //匹配返回的index列表
        List<int> MatchIndexList;

        //手动输入TC列表
        List<string> InputTCStr;
        //手动输入对应TC番号列表
        //List<int> InputConvertTCindex;

        //标记动态生成textbox和listbox
        TextBox currentTextbox = null;
        ListBox currentListbox = null;

        bool isUpdata = false;
        DAL.IFTBCommonAPI treeMemory = new TreeMemoryFTBCommonAggregate();
        string conditionCalibrationPath = "";
        public ConditionInput(ConditionSelect conditionSelectForm)
        {
            InitializeComponent();
            //init
            InputTCStr = new List<string>();
            //InputConvertTCindex = new List<int>();
            //update flag
            isUpdata = true;
            tcFileName = StaticEnvironInfo.getTcWordFullFileName();
            selectForm = conditionSelectForm;

            conditionCalibrationPath = StaticEnvironInfo.getConditionCalibrationFullFileName() + this.treeMemory.getSelectModel() + "-" + this.treeMemory.getSelectCountry() + ".txt";

        }

        private void CreatTCJson()
        {
            IIterator tcIterator = tree.createMccFilteredTcIterator();
            IIterator levelIterator = tree.createLevelIterator();

            AllTCWord TCwordItemToJsonList = new AllTCWord();
            TCwordItemToJsonList.TCstrList = new List<TCWord>();

            allTCItem = new List<TCStrIndex>();
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                TCWord OneTC = new TCWord();
                OneTC.oneTCWord = new List<string>();

                TCStrIndex OneTC_Index = new TCStrIndex();
                OneTC_Index.oneTCList = new List<string>();

                //OneTC_Index.index = tcIterator.currentItem();

                for (levelIterator.first(); !levelIterator.isDone(); levelIterator.next())
                {
                    string str = tree.getLevelButtonWord();
                    if (str == "")
                    {
                        continue;
                    }
                    OneTC_Index.oneTCList.Add(str.Replace("/", "//"));

                    //str大小写统一
                    string[] str_list = str.Replace('/',' ').ToLower().Split();
                    foreach (string word in str_list)
                    {
                        OneTC.oneTCWord.Add(word);
                    }
                }
                TCwordItemToJsonList.TCstrList.Add(OneTC);
                allTCItem.Add(OneTC_Index);
            }

            string jsontext = "";
            FileStream fs = new FileStream(tcFileName, FileMode.Create, FileAccess.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(tcFileName, true);

            //add mcc,condition and level in one object
            jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(TCwordItemToJsonList);
            //write json
            sw.Write("\r\n" + jsontext + "\r\n");
            sw.Flush();
            sw.Close();
            
        }

        private void ConditionInput_Load(object sender, EventArgs e)
        {
            //创建进程间通讯实例
            chatopy = new EngineDocument();
           // chatopy.SetTCLibFunc(tcFileName);

            //check update
            if (isUpdata == true)
            {
                isUpdata = false;

                //creat TC.txt，save tc
                CreatTCJson();
                chatopy.SetTCLibFunc(tcFileName);
                //get all condition list
                AllConditionList = tree.getTotalConditionList();
                //设置表格属性
                this.ConditionDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                ConditionDataGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;

                //表格加载condition数据
                for (int count = 0; count < AllConditionList.Count; count++)
                {
                    this.ConditionDataGridView.Rows.Add();
                    this.ConditionDataGridView.Rows[count].Cells[0].Value = AllConditionList[count];
                    this.ConditionDataGridView.Rows[count].HeaderCell.Value = (count + 1).ToString();
                }
                if (File.Exists(conditionCalibrationPath) == true)
                {
                    lodaCondition(conditionCalibrationPath);
                }
            }
            else
            {
                chatopy.SetTCLibFunc(tcFileName);
            }
        }

        private void ConditionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ConditionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                System.Drawing.Rectangle rec = this.ConditionDataGridView.GetCellDisplayRectangle(this.ConditionDataGridView.CurrentCell.ColumnIndex, this.ConditionDataGridView.CurrentCell.RowIndex, false);

                int X = ConditionDataGridView.Location.X + rec.X;
                int Y = ConditionDataGridView.Location.Y + rec.Y;

                //创建一个textbox
                currentTextbox = new TextBox();
                currentTextbox.Multiline = true;//多行
                //覆盖当前表格中的单元格
                currentTextbox.Location = new Point(X, Y);
                currentTextbox.Size = new System.Drawing.Size(this.ConditionDataGridView.CurrentCell.Size.Width, this.ConditionDataGridView.CurrentCell.Size.Height);
                //加入控件
                this.Controls.Add(currentTextbox);
                //将控件显示到最前端
                currentTextbox.BringToFront();

                if (this.ConditionDataGridView.CurrentCell.Value != null)
                {
                    //复制当前表格内容到textbox
                    currentTextbox.Text = this.ConditionDataGridView.CurrentCell.Value.ToString();
                }

                //记录生成的textbox对应的单元格的行和列
                currentTextbox.Tag = new Point(e.RowIndex, e.ColumnIndex);
                //将焦点移到textbox上
                currentTextbox.Focus();
                currentTextbox.ScrollBars = ScrollBars.Vertical;
                //textchanged事件
                currentTextbox.TextChanged += currentTextbox_TextChanged;

                //创建一个listbox
                currentListbox = new ListBox();
                //位置置于textbox下方
                currentListbox.Location = new Point(X, Y + this.ConditionDataGridView.CurrentCell.Size.Height);
                currentListbox.Size = new System.Drawing.Size(currentTextbox.Size.Width, 100);
                this.Controls.Add(currentListbox);
                //显示到最前端
                currentListbox.BringToFront();
                //双击事件
                currentListbox.DoubleClick += currentListbox_DoubleClick;
                //水平滚动条
                currentListbox.HorizontalScrollbar = true;

                //获取当前的Condition
                string condition_str = ConditionDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                //匹配
                MatchIndexList = chatopy.SetMatchTCFunc(condition_str.ToLower());
                if (currentTextbox.Text != null && currentTextbox.Text != "")
                {
                    MatchIndexList = chatopy.SetReTCMatchFunc(currentTextbox.Text.ToLower());
                }

                if (MatchIndexList != null && MatchIndexList.Count > 0)
                {
                    foreach (int i in MatchIndexList)
                    {
                        if (i < 0)
                        {
                            break;
                        }
                        string matchTC = "";
                        foreach (string str in allTCItem[i].oneTCList)
                        {
                            matchTC += (str + "/");
                        }
                        matchTC = matchTC.Trim('/');
                        //向listbox中添加项目
                        this.currentListbox.Items.Add(matchTC);
                    }
                }
                if (saveButton.Text == "Close")
                {
                    saveButton.Text = "Save";
                    cancelButton.Visible = true;
                }
                this.saveButton.Enabled = false;
                this.cancelButton.Enabled = false;
                this.loadButton.Enabled = false;
            }
        }

        void currentListbox_DoubleClick(object sender, EventArgs e)
        {
            if (this.currentListbox.SelectedItem != null)
            {
                this.currentTextbox.Text = this.currentListbox.SelectedItem.ToString();
            }
        }

        void currentTextbox_TextChanged(object sender, EventArgs e)
        {
            //获取textbox的内容
            string inputStr = this.currentTextbox.Text;
            if (inputStr == "")
            {
                //如果没有内容，则以condition字符串重新匹配
                int row = ((Point)currentTextbox.Tag).X;
                inputStr = ConditionDataGridView.Rows[row].Cells[0].Value.ToString();
                MatchIndexList = chatopy.SetMatchTCFunc(inputStr.ToLower());
            }
            else
            {
                //若有内容则将textbox的内容进行二次匹配
                MatchIndexList = chatopy.SetReTCMatchFunc(inputStr.ToLower());
            }

            this.currentListbox.Items.Clear();
            if (MatchIndexList != null && MatchIndexList.Count > 0)
            {
                foreach (int i in MatchIndexList)
                {
                    if (i < 0)
                    {
                        //匹配分数为0的项，跳过
                        break;
                    }
                    string matchTC = "";
                    foreach (string str in allTCItem[i].oneTCList)
                    {
                        matchTC += (str + "/");
                    }
                    matchTC = matchTC.Trim('/');
                    //添加项目
                    this.currentListbox.Items.Add(matchTC);
                }
            }
        }

        private void ConditionDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (currentTextbox != null)
            {
                int row = ((Point)currentTextbox.Tag).X;
                int col = ((Point)currentTextbox.Tag).Y;
                if (currentTextbox.Text != null || currentTextbox.Text != "")
                {
                    //将textbox内容复制到单元格中
                    ConditionDataGridView.Rows[row].Cells[col].Value = currentTextbox.Text;
                    ConditionDataGridView.Rows[row].Cells[col].Style.ForeColor = Color.Black;
                }
                //删除textbox
                this.Controls.Remove(currentTextbox);
                currentTextbox = null;

                this.saveButton.Enabled = true;
                this.cancelButton.Enabled = true;
                this.loadButton.Enabled = true;
            }
            if (currentListbox != null)
            {
                //删除listbox
                this.Controls.Remove(currentListbox);
                currentTextbox = null;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.CloseForm();
        }

        //取得手动输入内容
        private void GetInputText()
        {
            //清空
            InputTCStr.Clear();
            //InputConvertTCindex.Clear();
            for (int i = 0; i < this.AllConditionList.Count; i++)
            {
                if (this.ConditionDataGridView.Rows[i].Cells[1].Value != null)
                {
                    //取得输入内容
                    string inputstr = this.ConditionDataGridView.Rows[i].Cells[1].Value.ToString();
                    //设置字体为黑色（红色）
                    ConditionDataGridView.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                    this.InputTCStr.Add(inputstr);
                }
                else
                {
                    this.InputTCStr.Add("");
                }
                //tc番号设置为-1
                //InputConvertTCindex.Add(_INVALIDTCINDEX);
            }
        }

        private bool getTCIndexFormInput()
        {
            bool ret = false;
            //复制一个匹配列表
            List<string> tempinputstr = new List<string>(this.InputTCStr);

            for (int TCindex = 0; TCindex < this.allTCItem.Count; TCindex++)
            {
                if (tempinputstr.Count <= 0 ||  tempinputstr.All(string.IsNullOrEmpty))
                {
                    //列表为空或者全为空字符串，则结束
                    break;
                }
                //取得一条TC
                List<string> currentTC = this.allTCItem[TCindex].oneTCList;

                for (int inputindex = 0; inputindex < InputTCStr.Count;inputindex++)
                {
                    if (InputTCStr[inputindex] == "")// || InputConvertTCindex[inputindex] != _INVALIDTCINDEX)
                    {
                        //检查的内容为空或者已经匹配TC番号，则跳过
                        continue;
                    }

                    //比较当前TC和输入的TC是否一致
                    if (InputTCStr[inputindex] == string.Join("/", currentTC))
                    {
                        //一致则将内容从匹配列表中移出
                        tempinputstr.Remove(InputTCStr[inputindex]);
                        //记录对应TC番号
                        //InputConvertTCindex[inputindex] = this.allTCItem[TCindex].index;
                        //字体变红
                        ConditionDataGridView.Rows[inputindex].Cells[1].Style.ForeColor = Color.Black;
                    }
                }
            }
            if (tempinputstr.Count <= 0 || tempinputstr.All(string.IsNullOrEmpty))
            {
                //匹配列表为空或全为空字符串，则所有输入项正确
                ret = true;
            }
            return ret;
        }

        public void set_tree(object t)
        {
            this.tree = (IFTBCommonAPI)t;
        }       

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (saveButton.Text == "Save")
            {
                //获取手动输入内容
                GetInputText();
                //检查输入是否正确并得到关联的tc番号列表
                bool isgetcomplete = getTCIndexFormInput();
                if (isgetcomplete == true)
                {
                    //AllConditionList InputConvertTCindex
                    ConditionCalibration conditionCalibration = new ConditionCalibration();
                    conditionCalibration.setConditionList(AllConditionList, InputTCStr);
                    conditionCalibration.creatJson(conditionCalibrationPath);
                    if (selectForm != null)
                    {
                        //update
                        selectForm.setUpdateFlag(false);
                    }
                    //saveButton.Text = "Close";
                    //cancelButton.Visible = false;
                }
                else
                {
                    //有输入项错误
                    MessageBox.Show("Please Modify the Invalid Path that With Red Font.");
                }
            }
        }

        private void CloseForm()
        {
            if (this.chatopy != null)
            {
                chatopy.CloseAppHalt();
                this.chatopy = null;
            }
            this.Close();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            string fileName = ofd.FileName;

            if (File.Exists(fileName) == false)
            {
                MessageBox.Show("没什么好load的");
            }
            else
            {
                lodaCondition(fileName);
            }

            GetInputText();
            bool isgetcomplete = getTCIndexFormInput();
            if (isgetcomplete == false)
            {
                MessageBox.Show("Please Modify the Invalid Path that With Red Font.");
            }
        }

        private void lodaCondition(string fileName)
        {
            ConditionCalibration conditionCalibration = new ConditionCalibration();
            List<ConditionInfo> conditionList = conditionCalibration.loadConditionInfo(fileName);
            if (conditionList.Count != AllConditionList.Count)
            {
                MessageBox.Show("select file wrong!");
            }
            else
            {
                for (int count = 0; count < AllConditionList.Count; count++)
                {
                    this.ConditionDataGridView.Rows[count].Cells[1].Value = conditionList[count].tcPath;
                }
            }
        }
        private void ConditionInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseForm();
        }
    }
}
