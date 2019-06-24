using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FTBExcel2Script
{
    public partial class MainForm : Form
    {
        private string fileSavePath = "";
        private string fileOutputSelPath = "";
        private string ftbFileFullPath = "";
        private string modelRelateFileFullPath = "";
        private CommonFTBXlsOperationNpoi ftbXls = null;

        private List<CheckBox> allCheckBoxTC = new List<CheckBox>();
        private List<CheckBox> allCheckBoxTestView = new List<CheckBox>();

        private bool ReadFTBInfoFlag = false;
        private bool CreatScriptFlag = false;

        private bool AutoCheck = false;
        private bool ApplicationExitByUser = false;

        private List<string> SeriesInfo;
        private List<string> strArrTCFunc;
        private List<string> strArrTCViewPoint;

        private static readonly string CurrentPath = Directory.GetCurrentDirectory();
        private InitHelper iniSettings;

        private int CurSeriesSelectedIndex = 0;

        // 実行中画面
        private RunningForm ShowRunScrn;

        /*  機種情報*/
        private string modelInfo
        {
            get { return iniSettings.Get("INPUT", "modelInfo"); }
        }

        /*  選択機種情報*/
        private string selModelInfo
        {
            get { return iniSettings.Get("INPUT", "selModelInfo"); }
            set { iniSettings.Set("INPUT", "selModelInfo", value); }
        }

        /*  FTB Path*/
        private string ftbPath
        {
            get { return iniSettings.Get("INPUT", "ftbPath"); }
            set { iniSettings.Set("INPUT", "ftbPath", value); }
        }

        /*  modelRelate Path*/
        private string modelRelatePath
        {
            get { return iniSettings.Get("INPUT", "modelRelatePath"); }
            set { iniSettings.Set("INPUT", "modelRelatePath", value); }
        }

        /*  所属产品ID：XX */
        private string productID
        {
            get { return iniSettings.Get("INPUT", "productID"); }
        }

        /*  所属模块：模块名(#XX) */
        private string moduleInfo
        {
            get { return iniSettings.Get("INPUT", "moduleInfo"); }
        }

        /*  模块1：TC観点の選択*/
        private string tc_TestView_1
        {
            get { return iniSettings.Get("INPUT", "tc_View1"); }
        }

        /*  模块1：TC観点の選択*/
        private string tc_TestView_2
        {
            get { return iniSettings.Get("INPUT", "tc_View2"); }
        }

        private bool ftb2JSON
        {
            get
            {
                bool flag = false;
                string ftb2JSON = iniSettings.Get("INPUT", "ftb2JSON");
                return (true == bool.TryParse(ftb2JSON, out flag)) ? flag : false;
            }
        }

        /*  出力Path*/
        private string outPath
        {
            get { return iniSettings.Get("OUTPUT", "outPath"); }
            set { iniSettings.Set("OUTPUT", "outPath", value); }
        }

        public MainForm()
        {
            InitializeComponent();

            iniSettings = new InitHelper(CurrentPath + "\\setting.ini");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string strTempInfo = "";

            /* 读入機種情報 */
            strTempInfo = modelInfo.Substring(1);
            strTempInfo = strTempInfo.Remove(strTempInfo.Length - 1);
            SeriesInfo = new List<string>(strTempInfo.Split(','));
            strTempInfo = selModelInfo;
            for (int iloop = 0; iloop < SeriesInfo.Count; iloop++)
            {
                ComboBox_Series.Items.Add(SeriesInfo[iloop]);
                if (true == strTempInfo.Equals(SeriesInfo[iloop]))
                {
                    CurSeriesSelectedIndex = iloop;
                }
            }
            ComboBox_Series.SelectedIndex = CurSeriesSelectedIndex;

            TreeView_SelTestView.Nodes.Clear();
            /* 读入模块情報 */
            strTempInfo = moduleInfo.Substring(1);
            strTempInfo = strTempInfo.Remove(strTempInfo.Length - 1);
            strArrTCFunc = new List<string>(strTempInfo.Split(','));
            foreach (string tempstr in strArrTCFunc)
            {
                TreeView_SelTestView.Nodes.Add(tempstr);
            }

            /* 读入模块1的TC観点 */
            strTempInfo = tc_TestView_1.Substring(1);
            strTempInfo = strTempInfo.Remove(strTempInfo.Length - 1);
            strArrTCViewPoint = new List<string>(strTempInfo.Split(','));
            foreach (string tempstr in strArrTCViewPoint)
            {
                if (false == string.IsNullOrEmpty(tempstr))
                {
                    TreeView_SelTestView.Nodes[0].Nodes.Add(tempstr);
                }
            }

            /* 读入模块2的TC観点 */
            strTempInfo = tc_TestView_2.Substring(1);
            strTempInfo = strTempInfo.Remove(strTempInfo.Length - 1);
            strArrTCViewPoint = new List<string>(strTempInfo.Split(','));
            foreach (string tempstr in strArrTCViewPoint)
            {
                if (false == string.IsNullOrEmpty(tempstr))
                {
                    TreeView_SelTestView.Nodes[1].Nodes.Add(tempstr);
                }
            }
            
            //allCheckBoxTC.Add(CheckBox_TC_Default);
            //allCheckBoxTC.Add(CheckBox_TC_OptionSet);

            //allCheckBoxTestView.Add(CheckBox_View_ScrnTitle);
            //allCheckBoxTestView.Add(CheckBox_View_SubList);
            //allCheckBoxTestView.Add(CheckBox_View_MenuEnable);
            //allCheckBoxTestView.Add(CheckBox_View_ScrnSoftKey);
            //allCheckBoxTestView.Add(CheckBox_View_ScrnMachinInfo);

            CtrlStatusChanged(true,false);
            
            label_LogMessage.Text = "";

            TextBox_FTBFile.Text = ftbPath;
            //TextBox_ModelRelateFile.Text = modelRelatePath;
            TextBox_Output.Text = outPath;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (true == CreatScriptFlag)
                {
                    DialogResult DR = MessageBox.Show("スクリプトファイルが生成中です\n終了しますか?", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (DialogResult.Cancel == DR)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        ftbXls.UserCancelProcessEndFlag = true;
                        ApplicationExitByUser = true;
                        Application.Exit();
                    }
                }

                if (true == ReadFTBInfoFlag)
                {
                    ApplicationExitByUser = true;
                    e.Cancel = true;
                }
            }
        }

        private void ComboBox_Series_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_Model.Items.Clear();
            ComboBox_Country.Items.Clear();
            ComboBox_Area.Items.Clear();
            ComboBox_Language.Items.Clear();
            CtrlStatusChanged(true, false);
            BTN_ReadFTBInfo.Enabled = CheckReadFTBInfoEnable();
            if (CurSeriesSelectedIndex != ComboBox_Series.SelectedIndex)
            {
            	CurSeriesSelectedIndex = ComboBox_Series.SelectedIndex;
                if (false == string.IsNullOrEmpty(TextBox_FTBFile.Text))
                {
                    TextBox_FTBFile.Text = "";
                    MessageBox.Show("機種情報が変更しました。\n" + ComboBox_Series.Text + "機種のFTBフィアルを選択してください。");
                }
            }
        }

        private void TextBox_FTBFile_TextChanged(object sender, EventArgs e)
        {
            ftbFileFullPath = TextBox_FTBFile.Text;

            ComboBox_Model.Items.Clear();
            ComboBox_Country.Items.Clear();
            ComboBox_Area.Items.Clear();
            ComboBox_Language.Items.Clear();
            BTN_ReadFTBInfo.Enabled = CheckReadFTBInfoEnable();

            if (false == BTN_ReadFTBInfo.Enabled)
            {
                CtrlStatusChanged(true, false);
            }
            else
            {
                if (ComboBox_Language.SelectedIndex >= 0)
                {
                    CtrlStatusChanged(true, true);
                }
            }
        }

        private void BTN_ReadFTBInfo_Click(object sender, EventArgs e)
        {
            if (true == File.Exists(ftbFileFullPath))
            {
                // 判断文件的属性是否是ReadOnly
                if ((File.GetAttributes(ftbFileFullPath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // 如果是将文件的属性设置为Normal
                    File.SetAttributes(ftbFileFullPath, FileAttributes.Normal);
                }
                if (true == CheckFileOpenStt(ftbFileFullPath))
                {
                    MessageBox.Show("別のプロセスで使用されているため、プロセスはファイル\n" + ftbFileFullPath +   "\nにアクセスできません!", "エラー!");
                    return;
                }
                ReadFTBInfoFlag = true;
                CtrlStatusChanged(false, false);

                // 実行中画面を表示する
                ShowRunScrn = new RunningForm(getFormCenter());
                ShowRunScrn.Start();

                if (false == createObject(ComboBox_Series.Text))
                {
                    MessageBox.Show("FTBテスト情報を読めません!", "エラー!");
                    return;
                }
                
                //open FTB workbook
                if (true == ftbXls.openExcle(ftbFileFullPath) )
                {
                    if (true == ftbXls.ReadSelTestInfo())
                    {
                        if(null != ftbXls.CurFTBMccInfo)
                        {
                            label_LogMessage.Text = "Read FTB Test Info Success!";

                            ComboBox_Model.Items.Clear();
                            foreach (ModelData modelInfo in ftbXls.CurFTBMccInfo.modeldatalist)
                            {
                                ComboBox_Model.Items.Add(modelInfo.model_name);
                            }
                            ComboBox_Model.SelectedIndex = 0;

                            ComboBox_Area.Items.Clear();
                            foreach (ContinentCountryData ContinentInfo in ftbXls.CurFTBMccInfo.continentcountrydatalist)
                            {
                                ComboBox_Area.Items.Add(ContinentInfo.continent_name);
                            }
                            ComboBox_Area.SelectedIndex = 0;
                        }
                        else
                        {
                            label_LogMessage.Text = "Read FTB Test Info Failure!";
                            Trace.WriteLine(DateTime.Now + ":Read FTB Test Info Failure!ftbXls.CurFTBMccInfo is null");
                            CtrlStatusChanged(true, false);
                            MessageBox.Show("FTBテスト情報の読み取りに失敗しました!", "エラー!");
                        }
                    }
                    else
                    {
                        label_LogMessage.Text = "Read FTB Test Info Failure!";
                        Trace.WriteLine(DateTime.Now + ":Read FTB Test Info Failure!result of ReadSelTestInfo is false");
                        CtrlStatusChanged(true,false);
                        MessageBox.Show("FTBテスト情報の読み取りに失敗しました!", "エラー!");
                    }
                }

                // 実行中画面を閉じる
                ShowRunScrn.Abort();

                ReadFTBInfoFlag = false;
                CtrlStatusChanged(true, true);

                if (true == ApplicationExitByUser)
                {
                    Application.Exit();
                }                
            }
        }


        private bool createObject(string MachineType)
        {
            if (MachineType == "BC4")
            {
                ftbXls = new BC4(this);
            }
            else if (MachineType == "EC")
            {
                ftbXls = new EC(this);
            }
            else if (MachineType == "ECL|BHminni17")
            {
                ftbXls = new ECLAndBHminni17(this);
            }
            else if (MachineType == "BH17|DL")
            {
                ftbXls = new BH17AndDL(this);
            }
            else if (MachineType == "ELL")
            {
                ftbXls = new ELL(this);
            }
            else if (MachineType == "QL820")
            {
                ftbXls = new QL820(this);
            }
            else
            {
                ftbXls = null;
            }
            if (null != ftbXls)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BTN_SelFTB_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            List<string> country_list = new List<string>();
            dlg.Title = "Please select FTB File";
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Filter = "Excel Book|*.xls;*.xlsm;*.xlsx|ALL|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if ((false == Path.GetExtension(dlg.FileName).Equals(".xls")) &&
                    (false == Path.GetExtension(dlg.FileName).Equals(".xlsm")) &&
                    (false == Path.GetExtension(dlg.FileName).Equals(".xlsx"))
                 )
                {
                    MessageBox.Show("有効のFTBファイルを選択してください。");
                }
                else
                {
                    TextBox_FTBFile.Text = dlg.FileName;
                    ftbFileFullPath = TextBox_FTBFile.Text;
                }                
            }
        }

        private void BTN_SelModelRelate_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            List<string> country_list = new List<string>();
            dlg.Title = "Please select modelRelate File";
            dlg.InitialDirectory = Application.StartupPath;
            dlg.Filter = "CSV Files|*.csv;|ALL|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (false == Path.GetExtension(dlg.FileName).Equals(".csv"))
                {
                    MessageBox.Show("有効のmodelRelateファイルを選択してください。");
                }
                else
                {
                    TextBox_ModelRelateFile.Text = dlg.FileName;
                    modelRelateFileFullPath = TextBox_ModelRelateFile.Text;
                }
            }
        }

        private void TextBox_ModelRelateFile_TextChanged(object sender, EventArgs e)
        {
            modelRelateFileFullPath = TextBox_ModelRelateFile.Text;
            BTN_Start.Enabled = CheckStartEnable();
            BTN_Stop.Enabled = CheckStopEnable();
        }

        private void BTN_SelOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderSelect = new FolderBrowserDialog();
            folderSelect.SelectedPath = Application.StartupPath;
            if (folderSelect.ShowDialog() == DialogResult.OK)
            {
                TextBox_Output.Text = folderSelect.SelectedPath;
                fileOutputSelPath = TextBox_Output.Text;
                BTN_Start.Enabled = CheckStartEnable();
                BTN_Stop.Enabled = CheckStopEnable();
            }
        }

        private void TextBox_Output_TextChanged(object sender, EventArgs e)
        {
            fileOutputSelPath = TextBox_Output.Text;
            BTN_Start.Enabled = CheckStartEnable();
            BTN_Stop.Enabled = CheckStopEnable();
        }

        private void TreeView_SelTestView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool FirstLevelNodeCheck = false;
            if (0 == e.Node.Level)
            {
                if (true == AutoCheck)
                {
                    return;
                }
                foreach (TreeNode node in e.Node.Nodes)
                {
                    AutoCheck = true;
                    node.Checked = e.Node.Checked;
                    AutoCheck = false;
                }
            }
            else if (1 == e.Node.Level)
            {
                if (true == AutoCheck)
                {
                    return;
                }
                AutoCheck = true;
                FirstLevelNodeCheck = false;
                foreach (TreeNode node in e.Node.Parent.Nodes)
                {
                    if (true == node.Checked)
                    {
                        FirstLevelNodeCheck = true;
                        break;
                    }
                }
                if (true == FirstLevelNodeCheck)
                {
                    e.Node.Parent.Checked = FirstLevelNodeCheck;
                }                
                AutoCheck = false;
            }

            BTN_Start.Enabled = CheckStartEnable();
            BTN_Stop.Enabled = CheckStopEnable();
        }

        private void BTN_Start_Click(object sender, EventArgs e)
        {
            if (null == ftbXls)
            {
                MessageBox.Show("FTBを読むことができません", "エラー!");
                Trace.WriteLine(DateTime.Now + ":ftbXls is null");
                return;
            }

            if (false == File.Exists(ftbFileFullPath))
            {
                MessageBox.Show("下記ののファイルが存在しません!\n" + ftbFileFullPath, "エラー!");
                Trace.WriteLine(DateTime.Now + ":" + ftbFileFullPath + " not Exists");
                return;
            }

            if (true == CheckFileOpenStt(ftbFileFullPath))
            {
                MessageBox.Show("別のプロセスで使用されているため、プロセスはファイル" + ftbFileFullPath + "にアクセスできません!", "エラー!");
                return;
            }

            ftbPath = TextBox_FTBFile.Text;
            modelRelatePath = TextBox_ModelRelateFile.Text;
            outPath = TextBox_Output.Text;

            CreatScriptFlag = true;
            CtrlStatusChanged(false, false);

            ftbXls.UserCancelProcessEndFlag = false;
            ftbXls.ftb2JSONFlag = ftb2JSON;

            //open FTB workbook
            setMessage("Open FTB Excle,please wait……");
            if (ftbXls.openExcle(ftbFileFullPath) == true)
            {
                fileSavePath = fileOutputSelPath + "\\" + Path.GetFileNameWithoutExtension(ftbFileFullPath);
                fileSavePath = fileSavePath + "\\" + ComboBox_Model.Text + "\\" + ComboBox_Area.Text + "\\" + ComboBox_Country.Text + "\\" + ComboBox_Language.Text;
                //CreateDirectory
                if (false == Directory.Exists(fileSavePath))
                {
                    Directory.CreateDirectory(fileSavePath);
                }
            }
            else
            {
                MessageBox.Show("FTB ファイルを開くことができません\n" + ftbFileFullPath, "エラー!");
                Trace.WriteLine(DateTime.Now + ":" + ftbFileFullPath + ":Open ftb excel error");
                return;
            }
            TCScriptCreat scriptCreat = new TCScriptCreat();
            if (ftbFileFullPath.Contains("TP_7.0") || ftbFileFullPath.Contains("FB(7.0TP)")
                || ftbFileFullPath.Contains("FB_7.0TP") || ftbFileFullPath.Contains("FB-7.0TP"))
            {
                scriptCreat.settingsButtonExistSet = false; // TP7.0机型不存在Settings Button
            }
            else
            {
                scriptCreat.settingsButtonExistSet = true; // TP7.0以外的机型存在Settings Button
            }
            //setMessage("Read modelRelate Info...");
            //TCScriptCreat.read_modelRelateInfo(modelRelateFileFullPath);

            setMessage("Read condition replace info...");
            TCScriptCreat.read_FTB_Condition_Replace(Application.StartupPath + "\\FTB_Condition_Replace.xlsx");
            string configurePath = Directory.GetCurrentDirectory() + "\\Configure.xlsx";
            TCScriptCreat.readConditionReplaceContent(configurePath);

            TCScriptCreat.AllLanguageList = new List<string>();
            foreach (string strLang in ComboBox_Language.Items)
            {
                TCScriptCreat.AllLanguageList.Add(strLang);
            }

            int index = 0;
            bool[] tempArrChkStsAllScrn = new bool[TreeView_SelTestView.Nodes[0].Nodes.Count + 1];
            bool[] tempArrChkStsOptions = new bool[TreeView_SelTestView.Nodes[1].Nodes.Count + 1];

            tempArrChkStsAllScrn[0] = TreeView_SelTestView.Nodes[0].Checked;
            index = 1;
            foreach (TreeNode tempNode in TreeView_SelTestView.Nodes[0].Nodes)
            {
                tempArrChkStsAllScrn[index] = tempNode.Checked;
                index++;
            }

            tempArrChkStsOptions[0] = TreeView_SelTestView.Nodes[1].Checked;
            index = 1;
            foreach (TreeNode tempNode in TreeView_SelTestView.Nodes[1].Nodes)
            {
                tempArrChkStsOptions[index] = tempNode.Checked;
                index++;
            }

            TCScriptCreat.SelModelName = ComboBox_Model.Text;
            TCScriptCreat.CurTCProductID = productID;

            /* 测试观点的选择 */
            TCScriptCreat.TC_TestView_Content_SelFlag(tempArrChkStsAllScrn, tempArrChkStsOptions);
            TCScriptCreat.FuncNameAllScrnConfirm = TreeView_SelTestView.Nodes[0].Text;
            TCScriptCreat.FuncNameAllOptionSet = TreeView_SelTestView.Nodes[1].Text;

            /* 脚本文件保存的根目录 */
            TCScriptCreat.FileSaveRootPath = fileSavePath;

            setMessage("Read FTB detail info, please wait……");
            ftbXls.ReadFTBDetailInfo(fileSavePath);

            CreatScriptFlag = false;

            if (true == ApplicationExitByUser)
            {
                return;
            }

            CtrlStatusChanged(true, true);

            if (false == ftbXls.UserCancelProcessEndFlag)
            {
                MessageBox.Show("スクリプトファイルが生成完了しました！", "生成完了");
                Process.Start("explorer.exe", fileSavePath);
            }
            else
            {
                ftbXls.UserCancelProcessEndFlag = false;
                MessageBox.Show("プロセッシングが取消完了しました！", "取消完了");
            }

            fileSavePath = "";
        }

        private void BTN_Stop_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show("プロセッシングを取り消しますがよろしいですか?", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult.OK == DR)
            {
                ftbXls.UserCancelProcessEndFlag = true;
            }
            else
            {
                ftbXls.UserCancelProcessEndFlag = false;
            }
        }


        private void ComboBox_Model_SelectedIndexChanged(object sender, EventArgs e)
        {
            ftbXls.SelModelIndex = ComboBox_Model.SelectedIndex;
        }

        private void ComboBox_Area_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> countryList = ftbXls.CurFTBMccInfo.continentcountrydatalist[ComboBox_Area.SelectedIndex].country_name;
            ComboBox_Country.Items.Clear();
            foreach (string StrCountry in countryList)
            {
                ComboBox_Country.Items.Add(StrCountry);
            }
            ComboBox_Country.SelectedIndex = 0;
            ComboBox_Country.Enabled = true;

            TCScriptCreat.SelContinentIndex = ComboBox_Area.SelectedIndex;
            ftbXls.SelContinentIndex = ComboBox_Area.SelectedIndex;
        }

        private void ComboBox_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index0 = 0,index1 = 0;
            List<string> supportLangList;
            ComboBox_Language.Items.Clear();
            foreach (LangData LangSets in ftbXls.CurLangMccInfo.langdatelist)
            {
                if (true == LangSets.model_name.Contains("MFC"))
                {
                    foreach (SupportLanguageDate SupportLang in ftbXls.CurLangMccInfo.langdatelist[index0].supportlanguageinfolist)
                    {
                        if (true == SupportLang.country_name.Equals(ComboBox_Country.Text))
                        {
                            supportLangList = ftbXls.CurLangMccInfo.langdatelist[index0].supportlanguageinfolist[index1].support_language_name;
                            if ((1 == supportLangList.Count) &&
                                (true == supportLangList[0].Equals("同上")))
                            {
                                supportLangList = ftbXls.CurLangMccInfo.langdatelist[index0].supportlanguageinfolist[index1 - 1].support_language_name;
                            }

                            foreach (string StrLang in supportLangList)
                            {
                                ComboBox_Language.Items.Add(StrLang);
                            }
                            ComboBox_Language.SelectedIndex = 0;
                            ComboBox_Language.Enabled = true;

                            break;
                        }
                        index1++;
                    }
                    break;
                }
                index0++;
            }

            TCScriptCreat.SelCountryIndex = ComboBox_Country.SelectedIndex;
            ftbXls.SelCountryIndex = ComboBox_Country.SelectedIndex;
        }

        private void ComboBox_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            CtrlStatusChanged(true, true);
        }

        //private void CtrlStatusChanged(bool EnableSts1, bool EnableSts2, bool EnableSts3)
        private void CtrlStatusChanged(bool EnableSts1, bool EnableSts2)
        {
            ComboBox_Series.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : EnableSts1;
            TextBox_FTBFile.ReadOnly = !(((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : EnableSts1);
            BTN_SelFTB.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : EnableSts1;

            ComboBox_Model.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : EnableSts2;
            ComboBox_Country.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : EnableSts2;
            ComboBox_Area.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : EnableSts2;
            ComboBox_Language.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : EnableSts2;
            BTN_ReadFTBInfo.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : CheckReadFTBInfoEnable();

            //BTN_SelModelRelate.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : CheckSelModelRelateEnable();
            BTN_SelModelRelate.Enabled = false;
            TextBox_ModelRelateFile.ReadOnly = !BTN_SelModelRelate.Enabled;

            TreeView_SelTestView.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : CheckAllCheckBoxEnable();

            BTN_SelOutput.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : CheckSelOutputEnable();
            TextBox_Output.ReadOnly = !BTN_SelOutput.Enabled;

            BTN_Start.Enabled = ((true == ReadFTBInfoFlag) || (true == CreatScriptFlag)) ? false : CheckStartEnable();
            BTN_Stop.Enabled = (false == CreatScriptFlag) ? false : true;

            Application.DoEvents();
        }

        private bool CheckReadFTBInfoEnable()
        {
            if ((ComboBox_Series.SelectedIndex < 0) || (ComboBox_Series.SelectedIndex >= ComboBox_Series.Items.Count))
            {
                return false;
            }

            if ( (false == File.Exists(ftbFileFullPath)) || 
                 ( (false == Path.GetExtension(ftbFileFullPath).Equals(".xls")) &&
                   (false == Path.GetExtension(ftbFileFullPath).Equals(".xlsm")) &&
                   (false == Path.GetExtension(ftbFileFullPath).Equals(".xlsx"))
                 )
               )
            {
                return false;
            }

            return true;
        }

        private bool CheckSelModelRelateEnable()
        {
            if (false == CheckReadFTBInfoEnable())
            {
                return false;
            }

            if (false == ComboBox_Language.Enabled)
            {
                return false;
            }

            if ((ComboBox_Language.SelectedIndex < 0) || (ComboBox_Language.SelectedIndex >= ComboBox_Series.Items.Count))
            {
                return false;
            }

            return true;
        }

        private bool CheckAllCheckBoxEnable()
        {
            return CheckSelModelRelateEnable();
        }

        private bool CheckSelOutputEnable()
        {
            return CheckSelModelRelateEnable();
        }

        private bool CheckStartEnable()
        {
            bool CheckedFlg = false;
            if (false == CheckReadFTBInfoEnable())
            {
                return false;
            }

            if ((ComboBox_Language.SelectedIndex < 0) || (ComboBox_Language.SelectedIndex >= ComboBox_Series.Items.Count))
            {
                return false;
            }

            CheckedFlg = false;
            foreach (TreeNode FirstNode in TreeView_SelTestView.Nodes)
            {
                if (true == FirstNode.Checked)
                {
                    CheckedFlg = true;
                    break;
                }
            }

            if (false == CheckedFlg)
            {
                return false;
            }

            //if (false == File.Exists(modelRelateFileFullPath) || (false == Path.GetExtension(modelRelateFileFullPath).Equals(".csv")))
            //{
            //    return false;
            //}

            if (false == Directory.Exists(fileOutputSelPath))
            {
                return false;
            }

            return true;
        }

        private bool CheckStopEnable()
        {
            return (false == CreatScriptFlag) ? false : true;
        }

        /// <summary>
        /// メインフォーム中央位置を取得する
        /// </summary>
        /// <returns>中央位置[x,y]</returns>
        private int[] getFormCenter()
        {
            int[] position = new int[2];
            // 中央座標を設定する
            position[0] = (this.Left + this.Width) / 2;
            position[1] = (this.Top + this.Height) / 2;

            return position;
        }

        /* 表示情報を更新する */
        public void setMessage(string message)
        {
            if (false == string.IsNullOrEmpty(message))
            {
                label_LogMessage.Text = message;
                Application.DoEvents();
            }
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);
        private const int OF_READWRITE = 2;
        private const int OF_SHARE_DENY_NONE = 0x40;
        private readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        /* 检查文件是否处于打开状态 */
        /* true:打开中 */
        /* false:未打开 */
        private bool CheckFileOpenStt(string strFileFullPath)
        {
            if (false == File.Exists(strFileFullPath))
            {
                return false;
            }

            IntPtr vHandle = _lopen(strFileFullPath, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
            {
                return true;
            }
            CloseHandle(vHandle);
            return false;
        }
    }
}
