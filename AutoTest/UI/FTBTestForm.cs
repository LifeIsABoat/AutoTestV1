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
using Tool.BLL;
using System.IO;
using Tool.Engine;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Tool.Machine;
using System.Net;
using System.Net.Sockets;
using Tool.Command;
using System.Management;

namespace Tool.UI
{
    public partial class FTBTestForm : Form
    {
        IFTBCommonAPI treeMemory;
        public string JsonFileName;
        MachineMessage machineMesReadRet;
        TcRelevant tcRelevantJsonReadRet;
        OpinionInformation opinionInfoReadRet;
        //TcSelect
        private TCSelectForm viewform;
        private USAUKEnglishCorrespond usaUKEnglishCorrespond;
        private ConditionSelect ConditionForm;

        private OpinionSelectForm opinionSelectForm;

        ConditionInput ConditionInput_Form;
        ScreenTest screenTest;
        string[] arguments = null;
        //model
        List<string> modelList = new List<string>();
        string selectmode;

        //地区
        List<string> continentList = new List<string>();
        string selectcontinent;

        //国家
        List<string> continentCountryList = new List<string>();
        string selectcountry;

        //type
        List<string> machinetypeList = new List<string>();
        string selectmachinetype;

        //语言
        List<string> languageList = new List<string>();
        string selectlanguage;

        List<string> profileNameList = new List<string>();

        //thread
        TotalManageRun test = null;
        TcRunThread thread = null;

        //timer
        System.Timers.Timer timer;

        //
        TestModelInfo testModelInfo;
        List<TestModelInfo> testModelInfoList;
        TestCaseInfo testCaseInfo;
        TestOpinionInfo testOpinionInfo;
        List<TestJsonPath> jsonFilePathData;


        //userName
        public static string userName;

        //select language
        public static string language;

        //select machine type
        public static string machineType;

        public FTBTestForm(string[] paramter)
        {
            InitializeComponent();
            this.MachineTypeComboBox.Enabled = false;
            this.LanguageComboBox.Enabled = false;
            this.arguments = paramter;
            //set communicate 
            EngineDocument.setEngineCommunicator(new EngineCommunicatorByShareMemory(StaticEnvironInfo.getDocumentShareMemoryName(),
                                                                                     StaticEnvironInfo.getDocumentWriteSemaphoreName(),
                                                                                     StaticEnvironInfo.getDocumentReadSemaphoreName(),
                                                                                     StaticEnvironInfo.getDocumentShareMemorySize()));//to do 

        }

        public string getUserName()
        {
            return userName;
        }

        public string getLanguage()
        {
            return language;
        }

        public string getMachineType()
        {
            return machineType;
        }
        public string ReverseD(string text)
        {
            return new string(text.ToCharArray().Reverse().ToArray());
        }
        //获取Json文件并反序列化
        private void FTBFileBrowseButton_Click(object sender, EventArgs e)
        {
            //open OpenFileDialog to select FTB file
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Please select Json File";
            dlg.InitialDirectory = Application.StartupPath + @"\Input";
            dlg.Filter = "Text File|*.txt;*.csv|ALL|*.*";
            string Key = "[Basic Functions]/All Settings/General Setup/Tray Setting/Paper Type/MP Tray/Recycled Paper";

            string[] aa = Key.Split('/');
            Array.Reverse(aa);
            Array.Reverse(aa);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FTBFileTextBox.Text = dlg.FileName;
                if (File.Exists(FTBFileTextBox.Text))
                {
                    JsonFileName = FTBFileTextBox.Text;
                    TreeMemoryFTBCommonAggregate.setImporter(new TreeMemoryFTBCommonImportFormJson(JsonFileName));
                    treeMemory = new TreeMemoryFTBCommonAggregate();
                    treeMemory.importTree();
                    treeMemory.importScreenDict();
                    setDirectoryName(dlg.FileName);
                    List<string> totalPath = treeMemory.getTotalPath();
                    StaticEnvironInfo.setFilePath(dlg.FileName);
                    TestRuntimeAggregate.import();
                    TestRuntimeAggregate.setTreeMemory(treeMemory);
                    modelList.Clear();
                    this.ModelComboBox.Items.Clear();
                    try
                    {
                        modelList = treeMemory.getTotalModelList();
                        //添加下拉选项
                        this.ModelComboBox.Items.AddRange(modelList.ToArray());
                        //设置默认第一个值
                        this.ModelComboBox.Text = modelList[0];
                    }
                    catch
                    {
                        MessageBox.Show("Can't Find Some Model In FTB Sheet[Model x Country]");
                    }
                    this.JsonFileBrowseButton.Enabled = false;
                }
            }
        }

        private void setDirectoryName(string filePath)
        {
            string modelPath = System.IO.Path.GetDirectoryName(filePath);
            if (filePath.Contains(@"\Input\"))
            {
                string rootPath = filePath.Substring(0, filePath.LastIndexOf(@"\Input\"));
                StaticEnvironInfo.setPath(modelPath, rootPath);
            }
            else
            {
                throw new FTBAutoTestException("Dictory Input is not exist");
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {

        }

        private void creatJson(string jsonPath, object data)
        {
            string jsontext = "";
            FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(jsonPath, true);
            jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            string jsonContent = ConvertJsonString(jsontext);
            //write json
            sw.Write("\r\n" + jsonContent + "\r\n");
            sw.Flush();
            sw.Close();
        }
        private string ConvertJsonString(string str)
        {
            //格式化json字符串
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            TextReader tr = new StringReader(str);
            Newtonsoft.Json.JsonTextReader jtr = new Newtonsoft.Json.JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else { return str; }
        }

        public string getTextJson(string path)
        {
            string buf = "";
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }
            // Open and read file contents
            using (StreamReader sr = File.OpenText(path))
            {

                buf = sr.ReadToEnd();
                buf = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(buf, " ");
                sr.Close();
            }
            return buf;
        }
        private void checkSelectModelCorrect()
        {
            AbstractComponentMachineIO machineIO;
            if (StaticEnvironInfo.isTPBvboardTested() == false)
            {
                //Initial Serial Config
                machineIO = new ComponentIOSerial(new SerialConfig(SerialPortComboBox.Text));
            }
            else
            {
                machineIO = new ComponentIOSocket("127.0.0.1", "1023");
            }
            AbstractComponentKeyBoardMFCTP machineKeyBoard = new ComponentKeyBoardMFCTPUseIO(machineIO);
            AbstractComponentTouchPanelMFCTP machineTouchPanel = new ComponentTouchPanelMFCTPUseIO(machineIO);
            string machineConfigFilename = StaticEnvironInfo.getMachineConfigFileName();
            AbstractMachineMFCTP machine = new MachineMFCTPUseIO(machineIO, machineKeyBoard, machineTouchPanel, machineConfigFilename);
            LogScreenChangeChecker.setIO(machineIO);
            machine.io.connect();
            machine.io.write(MFCTPLogCode.StartAutoTest);
            machine.io.write("\x14");
            Thread.Sleep(100);
            machine.io.write("ref_minfo\r\n");
            Thread.Sleep(300);
            string ref_minfoStr = machine.io.read();
            machine.io.write("exit\r\n");
            Thread.Sleep(100);
            machine.io.disconnect();
            if (ref_minfoStr.Contains("Model Name"))
            {
                string select = splitSpecialNumStr(ref_minfoStr);
                if (select != null && !select.Contains(this.ModelComboBox.Text))
                {
                    string expMsg = string.Format("間違った国を選択した!!!\n現在の機種は[{0}]です!!!", select.Replace("\t", ""));
                    DialogResult dr = MessageBox.Show(expMsg + "\nテストのモデルをCheckして、再度選択ください!!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (DialogResult.OK == dr)
                    {
                        return;
                    }
                }
            }
        }
        private void ModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectmode = this.ModelComboBox.Text;
            continentList.Clear();
            this.ContinentComboBox.Items.Clear();
            treeMemory.getTotalContinentList(continentList);//添加下拉选项
            foreach (string continent in continentList)
            {
                this.ContinentComboBox.Items.Add(continent);
            }
            //设置默认第一个值
            this.ContinentComboBox.Text = continentList[0];
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;//set mouse wait
            //checkSelectModelCorrect();
            this.Cursor = System.Windows.Forms.Cursors.Arrow;//set mouse normal
        }

        private void ContinentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> filterContinentList = new List<string>();
            //get filter continent
            filterContinentList = treeMemory.getTotalFilterContinentList(this.ModelComboBox.Text);
            if (!filterContinentList.Contains(this.ContinentComboBox.Text))
            {
                string expMsg = string.Format("FTBによると,[{0}]の代表国[{1}]が支持しないです!!!\nFTBを参照し,テスト地域再確認をお願いします!!!",
                ModelComboBox.Text, ContinentComboBox.Text);
                MessageBox.Show(expMsg, "エラー!!!テスト地域再確認をお願いします!!!");
            }
            selectcontinent = this.ContinentComboBox.Text;
            continentCountryList.Clear();
            this.CountryComboBox.Items.Clear();

            treeMemory.getTotalCountryList(selectcontinent, continentCountryList);//添加下拉选项
            foreach (string country in continentCountryList)
            {
                this.CountryComboBox.Items.Add(country);
            }
            //设置默认第一个值
            this.CountryComboBox.Text = continentCountryList[0];
        }

        private void CountryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> filterCountry = new List<string>();
            //get filter continent
            filterCountry = treeMemory.getTotalFilterCountryList(selectcontinent, this.ModelComboBox.Text);
            if (!filterCountry.Contains(this.CountryComboBox.Text))
            {
                string expMsg = string.Format("FTBによると,[{0}]の仕向け[{1}]が支持しないです!!!\nFTBを参照し,テスト仕向け再確認をお願いします!!!",
                    ModelComboBox.Text, CountryComboBox.Text);
                MessageBox.Show(expMsg, "エラー!!!テスト仕向け再確認をお願いします!!!");
            }
            selectcountry = this.CountryComboBox.Text;
            machinetypeList.Clear();
            this.MachineTypeComboBox.Items.Clear();

            machinetypeList = treeMemory.getTotalLanguageModelList();
            //添加下拉选项
            foreach (string machinetype in machinetypeList)
            {
                this.MachineTypeComboBox.Items.Add(machinetype);
            }
            this.MachineTypeComboBox.Enabled = true;
            this.LanguageComboBox.Enabled = false;
            this.LanguageComboBox.Text = "";
            this.MachineTypeComboBox.Text = "";

            this.TCSelextButton.Enabled = false;
            this.ConditionButton.Enabled = false;
            this.opinionSelectButton.Enabled = false;
            this.conditionCalibrateButton.Enabled = false;

            usaUKEnglishCorrespond = new USAUKEnglishCorrespond();
            usaUKEnglishCorrespond.StartPosition = FormStartPosition.CenterScreen;
            this.CorrespondButton.Enabled = true;
        }

        private void MachineTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectmachinetype = this.MachineTypeComboBox.Text;
            languageList.Clear();
            this.LanguageComboBox.Items.Clear();

            languageList = treeMemory.getTotalLanguageList(selectmachinetype, selectcountry);
            //添加下拉选项
            foreach (string language in languageList)
            {
                this.LanguageComboBox.Items.Add(language);
            }
            this.LanguageComboBox.Enabled = true;
            //设置默认第一个值
            this.LanguageComboBox.Text = languageList[0];
        }

        private void selectModeAndCountryCheck()
        {
            string intPutModelPathPath = StaticEnvironInfo.getIntPutModelPath();
            string[] filestr = Directory.GetFiles(intPutModelPathPath + @"\CountriesPosList");
            string filePath = StaticEnvironInfo.getIntPutModelPath() + @"\EnglishIsDefaultLanguage.txt";
            List<string> oneList = readTxtFileToList(filePath);
            List<string> AList = new List<string>(filestr);
            bool singleFlag = false;
            if (AList.Count > 0)
            {
                foreach (string str in AList)
                {
                    if (str.Contains(selectmode.Trim(new Char[] { ' ' })) && str.Contains(selectcountry.Trim(new Char[] { ' ' })))
                    {
                        return;
                    }
                    else
                    {
                        for (int j = 0; j < oneList.Count; j++)
                        {
                            if (selectcountry == oneList[j]) { return; }
                            else { singleFlag = true; }
                        }
                    }
                }

                if (singleFlag == true)
                {
                    startPrinterMonitor(selectmode, selectcountry);
                }
            }
            else
            {
                startPrinterMonitor(selectmode, selectcountry);
            }
        }
        private void startPrinterMonitor(string selectMode, string selectCountry)
        {
            MessageBoxButtons mesgButton = MessageBoxButtons.YesNo;
            string expMsg = string.Format("Can't Find [{0}] ButtonPos In Input CountriesPosList Files!!", selectMode + " " + selectCountry);
            DialogResult dr = MessageBox.Show(expMsg + "\nFirst Manual Change the MachineCountry you want to test.\nThen Click (Y) to start PrinterMonitor.exe and Start RecordClickPosButton,then save!!\nClick (N) to ignore this Message!!",
                expMsg, mesgButton, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (DialogResult.Yes == dr)
            {
                System.Diagnostics.Process.Start("PrinterMonitor.exe");
            }
        }

        private void LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectlanguage = this.LanguageComboBox.Text;
            this.TCSelextButton.Enabled = true;
            this.ConditionButton.Enabled = true;
            this.opinionSelectButton.Enabled = true;
            this.conditionCalibrateButton.Enabled = true;
            this.screenCalibrateButton.Enabled = true;
            treeMemory.setTotalModelAndCountry(selectmode, selectcountry);
            All_updata();
        }

        private void All_updata()
        {
            viewform = new TCSelectForm();
            viewform.StartPosition = FormStartPosition.CenterScreen;

            ConditionForm = new ConditionSelect();
            ConditionForm.StartPosition = FormStartPosition.CenterScreen;

            opinionSelectForm = new OpinionSelectForm(treeMemory);
            opinionSelectForm.StartPosition = FormStartPosition.CenterScreen;

            ConditionInput_Form = new ConditionInput(this.ConditionForm);
            screenTest = new ScreenTest(treeMemory);
            treeMemory.importScreenDict();

            treeMemory.setTotalContidionType();
            treeMemory.setTotalContidionIndex();
            treeMemory.setTotalContidionUnselect();
        }

        private void TCSelextButton_Click(object sender, EventArgs e)
        {
            viewform.set_tree(treeMemory);
            viewform.setNoConditionflag(ConditionForm.GetNoConditionCheckBoxState());

            viewform.ShowDialog();
        }



        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConditionButton_Click(object sender, EventArgs e)
        {
            ConditionForm.set_tree(treeMemory);
            ConditionForm.ShowDialog();
        }

        private void timeout(object o, ElapsedEventArgs e)
        {
            if (thread != null)
            {
                if ((thread.state & ThreadState.Stopped) != 0)
                {
                    //this.tabControl1.Enabled = true;
                    this.tabControl1.BeginInvoke(new EventHandler(tabControlShow), EventArgs.Empty);
                    timer.Stop();
                }
            }
        }
        protected void tabControlShow(object o, EventArgs e)
        {
            this.tabControl1.Enabled = true;
            this.StartButton.Enabled = false;
            this.creatReportButton.Enabled = false;
            string expMsg = string.Format("FTB Auto Test is Finished\nPlease Click OK to Close this Form!!!");
            DialogResult dR = MessageBox.Show(expMsg, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (DialogResult.OK == dR)
            {
                this.Close();
            }
        }

        private void showMessageBoxError()
        {
            string expMsg = string.Format("MachineIP Must Correct In EWSAndRSPOptionOperator.ini.\nMeanWhile RSPService(EWSService) Must be Opened.!!!\nClick (OK) To Close This Form and Checked Them!!!");
            DialogResult dr = MessageBox.Show(expMsg, "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (DialogResult.OK == dr)
            {
                this.Close();
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (this.arguments.Count() == 0)
            {
                SaveProfileButton_Click(sender, e);
                userName = null;
                language = LanguageComboBox.Text.ToString();
                machineType = MachineTypeComboBox.Text.ToString();
            }
            else
            {
                timer.Stop();
                clickForMainControlForm();
            }
            if (COMPortRadioButton.Checked == true)
            {
                StaticEnvironInfo.setTPFirmTestFlag(true);
            }
            else if (BvboardRadioButton.Checked == true)
            {
                StaticEnvironInfo.setTPBvboardTestFlag(true);
            }
            //checkIPAddressCorrect();
            okButton_Click_Event(sender, e);

            if (this.arguments.Count() > 0)
            {
                this.Close();
            }
        }

        private void okButton_Click_Event(object sender, EventArgs e)
        {
            IIterator tcIterator = null;
            tcIterator = treeMemory.createMccFilteredTcIterator();
            bool tcSelectesFlag = false;
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                if (treeMemory.isTcSelected(tcIterator.currentItem()))
                {
                    tcSelectesFlag = true;
                    break;
                }
            }
            if (TestRuntimeAggregate.getSelectedOpinion().Count > 0)
            {
                //FtbScreenTitleOrderChecker
                if ((!TestRuntimeAggregate.getSelectedOpinion().Contains("FtbScreenItemListChecker") && tcSelectesFlag)
                || TestRuntimeAggregate.getSelectedOpinion().Contains("FtbScreenItemListChecker"))
                {
                    this.profileNameList.AddRange(StaticEnvironInfo.getProfileName());
                    //to run moveto and direct
                    ButtonWordFix buttonWordFixer = new ButtonWordFix(this.profileNameList);
                    buttonWordFixer.execute();
                    try
                    {
                        test = new TotalManageRun(SerialPortComboBox.Text, this.arguments);
                        thread = new TcRunThread(test.run);
                        thread.start();
                        this.creatReportButton.Enabled = true;
                        this.StartButton.Enabled = false;

                        tabControl1.Enabled = false;

                        timer = new System.Timers.Timer(3000);
                        timer.Elapsed += new ElapsedEventHandler(timeout);
                        timer.AutoReset = true;
                        timer.Start();
                        //start watch TcRunThread and updateUIStatus
                        ThreadMethod();
                    }
                    catch (System.Reflection.TargetInvocationException)
                    {
                        showMessageBoxError();
                    }
                    catch (System.ServiceModel.EndpointNotFoundException)
                    {
                        showMessageBoxError();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Tc!");
                }
            }
            else if (TestRuntimeAggregate.getSelectedOpinion().Count <= 0)
            {
                MessageBox.Show("Please Select Opinion!");
            }
        }

        private void clickForMainControlForm()
        {
            List<string> list;
            if (this.arguments.Count() == 1)
            {
                string param = getTextJson(this.arguments[0]);
                Newtonsoft.Json.Linq.JArray jo = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(param);
                string MachineMessageJsonPath = jo[0]["MachineMessageJsonPath"].ToString();
                string TcRelevantJsonPath = jo[0]["TcRelevantJsonPath"].ToString();
                string OpinionInformationJsonPath = jo[0]["OpinionInformationJsonPath"].ToString();

                list = new List<string>();
                list.Add(MachineMessageJsonPath);
                list.Add(TcRelevantJsonPath);
                list.Add(OpinionInformationJsonPath);
            }
            else
            {
                list = new List<string>(this.arguments);
            }
            machineMesReadRet = new MachineMessage();
            machineMesReadRet.loadMachineMessage(list);

            opinionInfoReadRet = new OpinionInformation();
            opinionInfoReadRet.loadOpinionInformation(list);

            tcRelevantJsonReadRet = new TcRelevant();
            tcRelevantJsonReadRet.loadTcRelevant(list);

            FTBFileTextBox.Text = Path.GetDirectoryName(machineMesReadRet.filePath);
            FTBFileTextBox.Enabled = false;
            ModelComboBox.Text = machineMesReadRet.selectedModel;
            ModelComboBox.Enabled = false;
            ContinentComboBox.Text = machineMesReadRet.selectedContinent;
            ContinentComboBox.Enabled = false;
            CountryComboBox.Text = machineMesReadRet.selectedCountry;
            CountryComboBox.Enabled = false;
            MachineTypeComboBox.Text = machineMesReadRet.machineType;
            MachineTypeComboBox.Enabled = false;
            LanguageComboBox.Text = machineMesReadRet.whichLanguage;
            LanguageComboBox.Enabled = false;
            if (machineMesReadRet.testCom == "Bvboard" && machineMesReadRet.comSelected == null)
            {
                BvboardRadioButton.Checked = true;
            }
            else
            {
                COMPortRadioButton.Checked = true;
            }
            SerialPortComboBox.Text = machineMesReadRet.comSelected;
            SerialPortComboBox.Enabled = false;
            setDirectoryName(machineMesReadRet.filePath);
            JsonFileBrowseButton.Enabled = false;
            //
            TCSelextButton.Enabled = true;
            opinionSelectButton.Enabled = true;
            ConditionButton.Enabled = true;
            //
            string fileName = machineMesReadRet.filePath;
            TreeMemoryFTBCommonAggregate.setImporter(new TreeMemoryFTBCommonImportFormJson(fileName));
            treeMemory = new TreeMemoryFTBCommonAggregate();
            treeMemory.importTree();
            treeMemory.setTotalModelAndCountry(ModelComboBox.Text, CountryComboBox.Text);
            All_updata();
            if (tcRelevantJsonReadRet.conditionFlag == "NoCondition")
            {
                //set no condition selected
                treeMemory.setTotalNoConditionSelected();
            }
            IIterator someTc = treeMemory.createMccFilteredTcIterator();
            for (someTc.first(); !someTc.isDone(); someTc.next())
            {
                foreach (string onePath in tcRelevantJsonReadRet.hadSelectTcList)
                {
                    if (onePath == treeMemory.getTcDir(someTc.currentItem()))
                    {
                        treeMemory.setTcSelected(someTc.currentItem());
                    }
                }
            }

            TotalOpinionInfo totalOpinionInfo = new TotalOpinionInfo();
            if (String.Equals(opinionInfoReadRet.testType, "Temp", StringComparison.CurrentCultureIgnoreCase))
            {
                totalOpinionInfo.loadOpinionInfo(StaticEnvironInfo.getTempTcOpinionFullFileName());
            }
            else
            {
                totalOpinionInfo.loadOpinionInfo(StaticEnvironInfo.getMenuTcOpinionFullFileName());
            }
            
            Dictionary<string, List<string>>.KeyCollection keyColl = totalOpinionInfo.tcRunManagerShip.Keys;

            TestRuntimeAggregate.import();
            TestRuntimeAggregate.setTreeMemory(treeMemory);
            //set select opinion and tcmanager
            for (int i = 0; i < opinionInfoReadRet.selectedTcRunManagerList.Count; i++)
            {
                foreach (string tcMangerName in keyColl)
                {
                    if (opinionInfoReadRet.selectedTcRunManagerList[i] == tcMangerName)
                    {
                        for (int amy = 0; amy < opinionInfoReadRet.selectedOpinionNameList.Count; amy++)
                        {
                            //if (totalOpinionInfo.tcRunManagerShip[tcMangerName].Contains(totalOpinionInfo.opinionList[amy].opinionName))
                            if (totalOpinionInfo.tcRunManagerShip[tcMangerName].Contains(opinionInfoReadRet.selectedOpinionNameList[amy]))
                            {
                                for (int w = 0; w < totalOpinionInfo.opinionList.Count; w++)
                                {
                                    if (totalOpinionInfo.opinionList[w].opinionName == opinionInfoReadRet.selectedOpinionNameList[amy])
                                    {
                                        TestRuntimeAggregate.addSelectedOpinion(totalOpinionInfo.opinionList[w].opinionName,
                                                                                totalOpinionInfo.opinionList[w].opinionDetail,
                                                                                totalOpinionInfo.opinionList[w].opinionRange,
                                                                                totalOpinionInfo.opinionList[w].opinionType
                                                                                );
                                        TestRuntimeAggregate.setSelectedTcRunManager(opinionInfoReadRet.selectedTcRunManagerList[i],
                                            totalOpinionInfo.opinionList[w].opinionName);

                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (opinionInfoReadRet.ocrFlag == "OCR")
            {
                StaticEnvironInfo.setOcrUsedFlag(true);
                treeMemory.setStringBuff("OCR");
            }
            else
            {
                StaticEnvironInfo.setOcrUsedFlag(false);
                treeMemory.setStringBuff("Log");
            }
        }

        private void checkIPAddressCorrect()
        {
            AbstractComponentMachineIO machineIO;
            if (StaticEnvironInfo.isTPBvboardTested() == false)
            {
                //Initial Serial Config
                machineIO = new ComponentIOSerial(new SerialConfig(SerialPortComboBox.Text));
            }
            else
            {
                machineIO = new ComponentIOSocket("127.0.0.1", "1023");
            }
            AbstractComponentKeyBoardMFCTP machineKeyBoard = new ComponentKeyBoardMFCTPUseIO(machineIO);
            AbstractComponentTouchPanelMFCTP machineTouchPanel = new ComponentTouchPanelMFCTPUseIO(machineIO);
            string machineConfigFilename = StaticEnvironInfo.getMachineConfigFileName();
            AbstractMachineMFCTP machine = new MachineMFCTPUseIO(machineIO, machineKeyBoard, machineTouchPanel, machineConfigFilename);
            LogScreenChangeChecker.setIO(machineIO);
            machine.io.connect();
            machine.io.write(MFCTPLogCode.StartAutoTest);
            machine.io.write("\x14");
            Thread.Sleep(100);
            machine.io.write("net_info\r\n");
            Thread.Sleep(1200);
            string net_info = machine.io.read();
            for (int i = 0; i < 20; i++)
            {
                machine.io.write("\b");
            }
            machine.io.write("exit\r\n");
            System.Threading.Thread.Sleep(500);
            machine.io.disconnect();
            machine.io = null;
            machineIO = null;
            machine = null;
            if (net_info.Contains("IP Address"))
            {
                string select = splitSpecialNumStr(net_info);
                string ip = getIPAddress();
                if (!select.Contains(ip))
                {
                    IniFile aa = new IniFile(StaticEnvironInfo.getIntPutModelPath() + @"\" + "EWSAndRSPOptionOperator.ini");
                    Match m = Regex.Match(select, @"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}");
                    if (m.Success)
                    {
                        select = m.Value;
                    }
                    aa.IniWriteValue("AutoTestForRSP", "MachineIP", select);
                    aa.IniWriteValue("AutoTestForEWS", "MachineIP", select);
                }
            }
            Thread.Sleep(300);
        }
        private string splitSpecialNumStr(string modular)
        {
            string AddressName = null;
            string[] splitRet = modular.Split(new char[] { '\r', '\n' });
            List<string> newList = new List<string>(splitRet);
            newList.RemoveAll(it => it == "");
            foreach (string str in newList)
            {
                if (str.Contains("IP Address") && !str.Contains("IP Address ="))
                {
                    AddressName = str;
                    return AddressName;
                }
                if (str.Contains("Model Name"))
                {
                    AddressName = str.Replace("Model Name", "");
                    return AddressName;
                }
            }
            return AddressName;
        }
        private string getIPAddress()
        {
            string iniFileName = StaticEnvironInfo.getIntPutModelPath() + @"\" + "EWSAndRSPOptionOperator.ini";
            List<string> keylist = new List<string>();
            Dictionary<string, string> EWSIpInfo = new Dictionary<string, string>();
            Parser.INIParser.getKeyList("AutoTestForEWS", iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                string value = Parser.INIParser.getvalue("AutoTestForEWS", iniFileName, keylist[i]);
                if (value != "")
                {
                    EWSIpInfo.Add(keylist[i], value);
                }
            }
            keylist.Clear();
            return EWSIpInfo["MachineIP"];
        }

        void ThreadMethod()
        {
            while (true)
            {
                string mes = TestRuntimeAggregate.getTcMessage();
                Application.DoEvents();
                if (mes == null)
                    continue;
                //assgin run over Tc to progressBar1
                double len = Convert.ToDouble(mes.Replace("%", ""));
                progressBar1.Maximum = 10000;
                progressBar1.Value = (int)(len * 100);
                label13.Text = "Partial: " + TestRuntimeAggregate.getTcMessage();
                Application.DoEvents();

                string totalManagerRunMes = TestRuntimeAggregate.getTotalManagerRun();
                if (totalManagerRunMes == null)
                    continue;
                //assgin run over TcRunManager to progressBar2
                double totalLen = Convert.ToDouble(totalManagerRunMes.Replace("%", ""));
                progressBar2.Maximum = 10000;
                progressBar2.Value = (int)(totalLen * 100);
                label12.Text = "Total: " + TestRuntimeAggregate.getTotalManagerRun();
                Application.DoEvents();
                if (totalManagerRunMes == "100%")
                {
                    break;
                }
            }
        }

        private void FTBTestForm_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            List<string> aist = new List<string>(ports);
            string[] ss = MulGetHardwareInfo(HardwareEnum.Win32_PnPEntity, "Name");
            List<string> newList = new List<string>(ss);
            string port = null;
            foreach (string strr in newList)
            {
                if (strr.Contains("USB Serial Port"))
                {
                    foreach (string sssa in aist)
                    {
                        if (strr.Contains(sssa)) { port = sssa; }
                    }
                }
            }
            textBox2.Enabled = false;
            textBox2.Text = "1023";
            Array.Sort(ports);
            SerialPortComboBox.Items.AddRange(ports);
            SerialPortComboBox.Text = port;
            startBat();
            if (this.arguments.Count() > 0)
            {
                timer = new System.Timers.Timer(100);
                timer.Elapsed += new ElapsedEventHandler(startButtonHookUp);
                timer.AutoReset = true;
                timer.Start();
            }
        }
        private void startButtonHookUp(object o, ElapsedEventArgs e)
        {
            this.BeginInvoke(new EventHandler(StartButton_Click));
        }
        private void startBat()
        {
            string runCurrentPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string batPath = Path.Combine(runCurrentPath, "ModifyRegistry.bat");
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            try { LaunchBat(batPath); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        private void LaunchBat(string batName)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.FileName = batName;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            System.Diagnostics.Process.Start(startInfo);
        }
        private void SerialPortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void opinionSelectButton_Click(object sender, EventArgs e)
        {
            opinionSelectForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test.CreateTcLibTest test = new Test.CreateTcLibTest(treeMemory);
            test.create();
        }
        public static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        {
            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value != null)
                        {
                            if (hardInfo.Properties[propKey].Value.ToString().Contains("COM"))
                            {
                                strs.Add(hardInfo.Properties[propKey].Value.ToString());
                            }
                        }
                    }
                    searcher.Dispose();
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            { strs = null; }
        }
        public enum HardwareEnum
        {
            //Hardware
            Win32_Processor,
            Win32_PhysicalMemory,
            Win32_Keyboard,
            Win32_PointingDevice,
            Win32_FloppyDrive,
            Win32_DiskDrive,
            Win32_CDROMDrive,
            Win32_BaseBoard,
            Win32_BIOS,
            Win32_ParallelPort,
            Win32_SerialPort,
            Win32_SerialPortConfiguration,
            Win32_SoundDevice,
            Win32_SystemSlot,
            Win32_USBController,
            Win32_NetworkAdapter,
            Win32_NetworkAdapterConfiguration,
            Win32_Printer,
            Win32_PrinterConfiguration,
            Win32_PrintJob,
            Win32_TCPIPPrinterPort,
            Win32_POTSModem,
            Win32_POTSModemToSerialPort,
            Win32_DesktopMonitor,
            Win32_DisplayConfiguration,
            Win32_DisplayControllerConfiguration,
            Win32_VideoController,
            Win32_VideoSettings,
            //System
            Win32_TimeZone,
            Win32_SystemDriver,
            Win32_DiskPartition,
            Win32_LogicalDisk,
            Win32_LogicalDiskToPartition,
            Win32_LogicalMemoryConfiguration,
            Win32_PageFile,
            Win32_PageFileSetting,
            Win32_BootConfiguration,
            Win32_ComputerSystem,
            Win32_OperatingSystem,
            Win32_StartupCommand,
            Win32_Service,
            Win32_Group,
            Win32_GroupUser,
            Win32_UserAccount,
            Win32_Process,
            Win32_Thread,
            Win32_Share,
            Win32_NetworkClient,
            Win32_NetworkProtocol,
            Win32_PnPEntity,
        }
        private void conditionCalibrateButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "DocumentEngine.exe";
            myProcess.Exited += new EventHandler(exep_Exited);
            myProcess.Start();

            ConditionInput_Form.set_tree(treeMemory);
            ConditionInput_Form.ShowDialog();
        }

        void exep_Exited(object sender, EventArgs e)
        {
        }

        private void screenCalibrateButton_Click(object sender, EventArgs e)
        {
            screenTest.OpinionSelectFormFlag = false;
            screenTest.ShowDialog();
        }

        private void screenSelect_Click(object sender, EventArgs e)
        {
            screenTest.OpinionSelectFormFlag = true;
            screenTest.ShowDialog();
        }

        private void jsonToFtb_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("FTBExcelToJsonNopi.exe");
        }
        private void scanCalibrateButton_Click(object sender, EventArgs e)
        {
            ScanCalibration scanCalibrationForm = new ScanCalibration(profileNameList);
            scanCalibrationForm.ShowDialog();
        }

        private void CorrespondButton_Click(object sender, EventArgs e)
        {
            usaUKEnglishCorrespond.ShowDialog();
        }

        private void creatReportButton_Click(object sender, EventArgs e)
        {
            if (test.isReportCreating == true)
            {
                MessageBox.Show("FTB自動テストの結果を生成中、お待ち下さい.", "お待ち下さい");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("FTB自動テストを中止ですか？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            if (this.thread != null)
            {
                this.thread.stop();
                TotalTCWriteReportHandler.writeReport();
                thread = null;
            }

            this.StartButton.Enabled = true;
            this.creatReportButton.Enabled = false;
            tabControl1.Enabled = true;
            if (this.arguments.Count() == 0)
            {
                timer.Stop();
            }
        }


        public List<string> readTxtFileToList(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(fs);
            //use StreamReader to read Text.
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            //read to end.
            string tmp = sr.ReadLine();
            while (tmp != null)
            {
                list.Add(tmp);
                tmp = sr.ReadLine();
            }
            //after read close it.
            sr.Close();
            fs.Close();
            return list;
        }
        public List<string> splitStr(string str)
        {
            string[] splitRet = str.Split(new char[] { ',', '{', '}' });
            List<string> splitList = new List<string>();
            for (int i = 0; i < splitRet.Count(); i++)
            {
                if (splitRet[i] != "")
                {
                    splitList.Add(splitRet[i]);
                }
            }
            return splitList;
        }

        private void comPortChanged(object sender, EventArgs e)
        {
            if (this.COMPortRadioButton.Checked == true)
            {
                textBox2.Enabled = false;
                SerialPortComboBox.Enabled = true;
            }
            if (this.BvboardRadioButton.Checked == true)
            {
                this.SerialPortComboBox.Text = null;
                this.textBox2.Enabled = true;
                this.SerialPortComboBox.Enabled = false;
            }
        }

        private void SaveProfileButton_Click(object sender, EventArgs e)
        {
            saveData(sender, e);
            string modelPath = StaticEnvironInfo.getIntPutModelPath();
            if (Directory.Exists(modelPath + @"\Automatic") == false) { Directory.CreateDirectory(modelPath + @"\Automatic"); }
            string time = DateTime.Now.ToString("yyyyMMddHHmm");
            string oneSinlgePath = modelPath + @"\Automatic\"/* + time + @"\"*/;
            if (!Directory.Exists(oneSinlgePath)) { Directory.CreateDirectory(oneSinlgePath); }
            string jsonFile = oneSinlgePath + "JsonPath.ini";
            jsonFilePathData = new List<TestJsonPath>();
            for (int index = 0; index < testModelInfoList.Count; index++)
            {
                string nowModelStr = testModelInfoList[index].selectedModel.Trim(new Char[] { ' ' });
                string nowCountry = testModelInfoList[index].selectedCountry;
                string saveModelJsonPath = oneSinlgePath + nowModelStr + "-"
                    + nowCountry + @".txt";
                string saveOpinionInfoPath = oneSinlgePath + nowModelStr + "-"
                    + nowCountry + @"OpinionInfo.txt";
                string saveTcRelevantPath = oneSinlgePath + nowModelStr + "-"
                    + nowCountry + @"TcRelevant.txt";
                //string saveTestFTBScreenItemPath = oneSinlgePath + nowModelStr + "-"
                //    + nowCountry + @"SelectedStandardScreen.txt";
                creatJson(saveModelJsonPath, testModelInfoList[index]);
                creatJson(saveOpinionInfoPath, testOpinionInfo);
                creatJson(saveTcRelevantPath, testCaseInfo);
                //creatJson(saveTestFTBScreenItemPath, ScreenMemoryList[index]);
                TestJsonPath jsonData = new TestJsonPath();
                jsonData.MachineMessageJsonPath = saveModelJsonPath;
                jsonData.OpinionInformationJsonPath = saveOpinionInfoPath;
                jsonData.TcRelevantJsonPath = saveTcRelevantPath;
                //jsonData.FTBScreenItemCheckJsonPath = saveTestFTBScreenItemPath;
                jsonFilePathData.Add(jsonData);
            }
            creatJson(jsonFile, jsonFilePathData);
        }
        private void saveData(object sender, EventArgs e)
        {
            testModelInfoList = new List<TestModelInfo>();
            testModelInfo = new TestModelInfo();
            TestModelInfo oneInfo = new TestModelInfo();

            if (COMPortRadioButton.Checked == true)
            {
                oneInfo.testCom = "COMPort";
                oneInfo.comSelected = SerialPortComboBox.Text;
            }
            else if (BvboardRadioButton.Checked == true)
            {
                oneInfo.testCom = "Bvboard";
            }
            oneInfo.filePath = FTBFileTextBox.Text;
            oneInfo.machineType = "MFC";
            oneInfo.whichLanguage = "English";
            oneInfo.selectedModel = this.ModelComboBox.Text;
            oneInfo.selectedContinent = this.ContinentComboBox.Text;
            oneInfo.selectedCountry = CountryComboBox.Text;
            testModelInfoList.Add(oneInfo);

            SelectTcInfo nowTcSelectInfo = new SelectTcInfo();
            IFTBCommonAPI tree = TestRuntimeAggregate.getTreeMemory();
            IIterator TcLoop = tree.createMccFilteredTcIterator();
            for (TcLoop.first(); !TcLoop.isDone(); TcLoop.next())
            {
                if (tree.isTcSelected(TcLoop.currentItem()))
                {
                    string route = tree.getTcDir(TcLoop.currentItem());
                    nowTcSelectInfo.tcSelectInfoList.Add(route);
                }
            }
            testCaseInfo = new TestCaseInfo();
            testCaseInfo.hadSelectTcList = nowTcSelectInfo.tcSelectInfoList;
            testCaseInfo.conditionFlag = "NoCondition";
            opinionSelectInfo opinionSelect = opinionSelectForm.getNowOpinionSelectInfo();
            testOpinionInfo = new TestOpinionInfo();
            testOpinionInfo.testType = opinionSelect.TestType.ToString();
            testOpinionInfo.ocrFlag = opinionSelect.OCRFlag.ToString();
            testOpinionInfo.selectedTcRunManagerList = opinionSelect.selectedTcRunManagerList;
            testOpinionInfo.selectedOpinionNameList = opinionSelect.selectedOpinionNameList;
        }

        private void ExoprtButton_Click(object sender, EventArgs e)
        {
            string JsonPathFileName = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Please select Json File";
            if (!Directory.Exists(Application.StartupPath + @"\Input"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\Input");
            }
            dlg.InitialDirectory = Application.StartupPath + @"\Input";
            dlg.Filter = "Text File|*.txt;*.ini|ALL|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                JsonPathFileName = dlg.FileName;
            }
            string param = getTextJson(JsonPathFileName);
            Newtonsoft.Json.Linq.JArray joJo = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(param);
            string MachineMessageJsonPath = joJo[0]["MachineMessageJsonPath"].ToString();
            string TcRelevantJsonPath = joJo[0]["TcRelevantJsonPath"].ToString();
            string OpinionInformationJsonPath = joJo[0]["OpinionInformationJsonPath"].ToString();
            List<string> list = new List<string>();
            list.Add(MachineMessageJsonPath);
            list.Add(TcRelevantJsonPath);
            list.Add(OpinionInformationJsonPath);
            machineMesReadRet = new MachineMessage();
            machineMesReadRet.loadMachineMessage(list);

            opinionInfoReadRet = new OpinionInformation();
            opinionInfoReadRet.loadOpinionInformation(list);

            tcRelevantJsonReadRet = new TcRelevant();
            tcRelevantJsonReadRet.loadTcRelevant(list);

            FTBFileTextBox.Text = machineMesReadRet.filePath;
            FTBFileTextBox.Enabled = true;
            ModelComboBox.Text = machineMesReadRet.selectedModel;
            ModelComboBox.Enabled = true;
            ContinentComboBox.Text = machineMesReadRet.selectedContinent;
            ContinentComboBox.Enabled = true;
            CountryComboBox.Text = machineMesReadRet.selectedCountry; selectcountry = machineMesReadRet.selectedCountry;
            CountryComboBox.Enabled = true;
            MachineTypeComboBox.Text = machineMesReadRet.machineType; selectmachinetype = machineMesReadRet.machineType;
            MachineTypeComboBox.Enabled = true;
            LanguageComboBox.Text = machineMesReadRet.whichLanguage;
            LanguageComboBox.Enabled = true;
            SerialPortComboBox.Text = machineMesReadRet.comSelected;
            SerialPortComboBox.Enabled = true;
            setDirectoryName(machineMesReadRet.filePath);
            JsonFileBrowseButton.Enabled = true;
            //
            TCSelextButton.Enabled = true;
            opinionSelectButton.Enabled = true;
            ConditionButton.Enabled = true;
            //
            StaticEnvironInfo.setFilePath(FTBFileTextBox.Text);
            TreeMemoryFTBCommonAggregate.setImporter(new TreeMemoryFTBCommonImportFormJson(FTBFileTextBox.Text));
            treeMemory = new TreeMemoryFTBCommonAggregate();
            treeMemory.importTree();
            setDirectoryName(machineMesReadRet.filePath);
            TestRuntimeAggregate.import();
            TestRuntimeAggregate.setTreeMemory(treeMemory);
            modelList.Clear();
            this.ModelComboBox.Items.Clear();
            try
            {
                modelList = treeMemory.getTotalModelList();
                this.ModelComboBox.Items.AddRange(modelList.ToArray());
                this.ModelComboBox.Text = modelList[0];
            }
            catch
            {
                MessageBox.Show("Can't Find Some Model In FTB Sheet[Model x Country]");
            }
            this.JsonFileBrowseButton.Enabled = false;
            if (tcRelevantJsonReadRet.conditionFlag == "NoCondition")
            {
                treeMemory.setTotalNoConditionSelected();
                treeMemory.setStringBuff("NoCondition");
            }
        }
    }

    class TcRunThread
    {
        Thread thread = null;
        private bool isAlive = false;

        public TcRunThread(ThreadStart start)
        {
            thread = new Thread(start);
        }
        public void start()
        {
            isAlive = true;
            thread.Start();
        }
        public void stop()
        {
            if (isAlive == true)
            {
                thread.Abort();
                while ((thread.ThreadState & ThreadState.Aborted) == 0)
                {
                    Thread.Sleep(100);
                }
                isAlive = false;
                thread = null;
            }
        }
        public ThreadState state
        {
            get
            {
                if (thread != null)
                {
                    return thread.ThreadState;
                }
                return ThreadState.Unstarted;
            }
        }
    }


    class MachineMessage
    {
        public string testCom;
        public string selectedModel;
        public string selectedContinent;
        public string selectedCountry;
        public string comSelected;
        public string whichLanguage;
        public string machineType;
        public string filePath;
        public void loadMachineMessage(List<string> list)
        {
            MachineMessage machineMesReadRet;
            for (int r = 0; r < list.Count(); r++)
            {
                if (!list[r].Contains("OpinionInfo") && !list[r].Contains("TcRelevant")
                    && !list[r].Contains("SelectedStandardScreen"))
                {
                    string param = getTextJson(list[r]);
                    if (param == null)
                        throw new FTBAutoTestException("Json content is null");
                    try { machineMesReadRet = JsonConvert.DeserializeObject<MachineMessage>(param); }
                    catch (Exception) { throw new FTBAutoTestException("DeserializeObject failed"); }
                    if (machineMesReadRet == null)
                        throw new FTBAutoTestException("DeserializeObject failed");

                    this.selectedModel = machineMesReadRet.selectedModel;
                    this.selectedContinent = machineMesReadRet.selectedContinent;
                    this.selectedCountry = machineMesReadRet.selectedCountry;
                    this.testCom = machineMesReadRet.testCom;
                    if (machineMesReadRet.testCom == "COMPort")
                    {
                        this.comSelected = machineMesReadRet.comSelected;
                    }
                    else { this.comSelected = null; }
                    this.whichLanguage = machineMesReadRet.whichLanguage;
                    this.machineType = machineMesReadRet.machineType;
                    this.filePath = machineMesReadRet.filePath;
                }
            }
        }
        private string getTextJson(string path)
        {
            string buf = "";
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }
            // Open and read file contents
            using (StreamReader sr = File.OpenText(path))
            {
                buf = sr.ReadToEnd();
                buf = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(buf, " ");
                sr.Close();
            }
            return buf;
        }
    }
    class TcRelevant
    {
        public List<string> hadSelectTcList;
        public string conditionFlag;

        public void loadTcRelevant(List<string> list)
        {
            for (int r = 0; r < list.Count(); r++)
            {
                TcRelevant tcRelevantJsonReadRet;
                if (list[r].Contains("TcRelevant"))
                {
                    string tcReData = getTextJson(list[r]);
                    if (tcReData == null)
                        throw new FTBAutoTestException("Json content is null");
                    try { tcRelevantJsonReadRet = JsonConvert.DeserializeObject<TcRelevant>(tcReData); }
                    catch (Exception) { throw new FTBAutoTestException("DeserializeObject failed"); }
                    if (tcRelevantJsonReadRet == null)
                        throw new FTBAutoTestException("DeserializeObject failed");

                    this.conditionFlag = tcRelevantJsonReadRet.conditionFlag;
                    this.hadSelectTcList = tcRelevantJsonReadRet.hadSelectTcList;
                }
            }
        }
        private string getTextJson(string path)
        {
            string buf = "";
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }
            // Open and read file contents
            using (StreamReader sr = File.OpenText(path))
            {

                buf = sr.ReadToEnd();
                buf = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(buf, " ");
                sr.Close();
            }
            return buf;
        }
    }
    class OpinionInformation
    {
        public List<string> selectedTcRunManagerList;
        public List<string> selectedOpinionNameList;
        public string testType;
        public string ocrFlag;

        public void loadOpinionInformation(List<string> list)
        {
            for (int r = 0; r < list.Count(); r++)
            {
                if (list[r].Contains("OpinionInfo"))
                {
                    OpinionInformation opinionInfoReadRet;
                    string optionInfo = getTextJson(list[r]);
                    if (optionInfo == null)
                        throw new FTBAutoTestException("Json content is null");
                    try { opinionInfoReadRet = JsonConvert.DeserializeObject<OpinionInformation>(optionInfo); }
                    catch (Exception) { throw new FTBAutoTestException("DeserializeObject failed"); }
                    if (opinionInfoReadRet == null)
                        throw new FTBAutoTestException("DeserializeObject failed");

                    this.selectedTcRunManagerList = opinionInfoReadRet.selectedTcRunManagerList;
                    this.selectedOpinionNameList = opinionInfoReadRet.selectedOpinionNameList;
                    this.testType = opinionInfoReadRet.testType;
                    this.ocrFlag = opinionInfoReadRet.ocrFlag;
                }
            }
        }
        private string getTextJson(string path)
        {
            string buf = "";
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new FTBAutoTestException("File does not exist or path error");
            }
            // Open and read file contents
            using (StreamReader sr = File.OpenText(path))
            {
                buf = sr.ReadToEnd();
                buf = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(buf, " ");
                sr.Close();
            }
            return buf;
        }
    }



    class TestJsonPath
    {
        public string MachineMessageJsonPath;
        public string TcRelevantJsonPath;
        public string OpinionInformationJsonPath;
        //public string FTBScreenItemCheckJsonPath;
    }
    class TestModelInfo
    {
        public string testCom;
        public string selectedModel;
        public string selectedContinent;
        public string selectedCountry;
        public string comSelected;
        public string whichLanguage;
        public string machineType;
        public string filePath;
    }
    class TestCaseInfo
    {
        public List<string> hadSelectTcList;
        public string conditionFlag;
    }
    class TestOpinionInfo
    {
        public List<string> selectedTcRunManagerList;
        public List<string> selectedOpinionNameList;
        public string testType;
        public string ocrFlag;
    }

}
