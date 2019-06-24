using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Forms = System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;

namespace WordingLink
{
    class ViewModel
    {
        #region 变量初始化
        public MWindowModel model { get; private set; }
        private delegate bool ProcessStep(); //声明委托类型,单步执行程序
        ProcessStep processStep = null; //新建委托
        ProcessStep processWait = null; //新建委托
        private int processIndex = 0;   //当前执行到第几步

        private delegate void ProcessMerge(string LinkFileFolder,string OutputFolder); //声明委托类型,比较输出文件
        ProcessMerge processMerge = null; //新建委托

        private ModelXmlHelper modelXml = null; //ModelInfo.XML文件数据
        private DataTable wordingData = null;   //保存文言申請システム文件数据
        private DataTable finalData = null; //保存最終文言エクセル文件数据
        private DataTable msgnoData = null; //MsgNo使用状況文件数据
        private DataTable finalSave = null; //保存最終文言エクセル文件需要最加的数据
        private int msgNo = 0;              //保存当前新规可用的MsgNo
        private int wordingStartCol = -1;    //保存文言申请文件的开始可用列
        private int finalStartCol = -1;      //保存最终文言文件的开始可用列
        private bool excuteError = false; //程序执行错误flag
        private bool hinagataUpdated = false; //Hinagata文件是否有更新
        private bool stridLinkUpdated = false; //strid_link文件是否有更新
        WordingExcelHandler wordingSearchExcel = null;

        private string MergeChangeList = string.Empty; //记录需要保存的ChangeList
        private string MergeWorkSpace = string.Empty; //记录需要保存的ChangeList所在的workspace

        Dictionary<int[], String> finalDict = new Dictionary<int[], string>();
        Dictionary<int[], String> wordingDict = new Dictionary<int[], string>();

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll ", EntryPoint = "GetDlgItem")]
        public static extern IntPtr GetDlgItem(IntPtr hParent, int nIDParentItem);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        const uint BM_CLICK = 0xF5;
        const uint WM_GETTEXT = 0x0D;
        const int wordingErrorID = 0x0000FFFF;
        private List<string> nAStrList = new List<string>() { "【N/A】", "「N/A」", "｢N/A｣", "[N/A]", "N/A" };

        private List<string> newList = new List<string>();
        private delegate bool ProcessTextScroll(int err,string textMsg); //声明委托类型,单步执行程序
        ProcessTextScroll processText = null; //新建委托
        private Object textLock = new Object();

        #endregion

        //构造函数
        public ViewModel(MWindowModel e, List<string> ArgumentsLists)
        {
            model = e;
            newList.AddRange(ArgumentsLists);
            /* Initialization */

            /* Event Register */
            model.Btn_Close = new DelegateCommand<string>(Event_Button_Close);
            model.Btn_StartProcess = new DelegateCommand<string>(Event_Button_StartProcess);
            model.Btn_ResetProcess = new DelegateCommand<string>(Event_Button_ResetProcess);
            model.OpenFileDialog_Excel = new DelegateCommand<string>(Event_OpenFileDialog_Excel);
            model.OpenFolder_OutPutPath = new DelegateCommand<string>(Event_OpenFolder_OutPutPath);
            model.OpenFolder_ProjectPath = new DelegateCommand<string>(Event_OpenFolder_OutPutPath);
            model.SettingsWindow_Start = new DelegateCommand< string > (Event_SettingsWindow_Start);
            model.TBox_ProjectPathChanged = new DelegateCommand<string>(Event_TextBox_ProjectPathChanged);

            try
            {
                model.ProjectPath = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_PROJECT);
                model.Excel_WordingFilePath = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_WORDING);
                model.Excel_MsgNoFilePath = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_MSGNO);
                model.Excel_FinalFilePath = Path.Combine(model.ProjectPath, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_FINAL));
                model.OutPutPath = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_OUTPUT);
                
                string Output_Kind = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND);

                if (CONST.KEY_KIND_PERFORCE.Equals(Output_Kind))
                {
                    model.Btn_ProjectEnabled = false;
                }
                else
                {
                    model.Btn_ProjectEnabled = true;
                }

                //设置Perforce Client的字符类型
                ProcessHelper.Excute("p4.exe", " set P4CHARSET=shiftjis", "");
            }
            catch { }
        }

        #region 按钮事件处理

        /* Close The Window */
        private void Event_Button_Close(string useless)
        {
            if (model.ProcessStatus == MWindowModel.Process_Status.PROCESS)
            {
                if (MessageBox.Show("程序正在执行中，是否强制退出?", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Process_Resource_Save();
                    Process_Resource_End();
                    Application.Current.Shutdown();
                }
            }
            else
            {
                Process_Resource_Save();
                Process_Resource_End();
                Application.Current.Shutdown();
            }
        }

        public void Event_Button_StartProcess(string useless)
        {
            model.Load_Visibility = CONST.ITEM_VISIBLE;
            model.ProcessStatus = MWindowModel.Process_Status.PROCESS;
            processStep = new ProcessStep(Process_Step_Start);
            processStep.BeginInvoke(Process_Step_End, null);
        }

        private void Event_Button_ResetProcess(string useless)
        {
            Process_Resource_End();
            processIndex = 0;
            model.ProcessStatus = MWindowModel.Process_Status.STOPED;
            GC.Collect();
        }

        private void Event_OpenFileDialog_Excel(string bindingProperty)
        {
            try
            {
                if (string.IsNullOrEmpty(bindingProperty))
                {
                    return;
                }
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = CONST.EXCELSUFFIX;
                openFileDialog.ShowDialog();
                if (string.IsNullOrEmpty(openFileDialog.FileName))
                    return;
                PropertyInfo propertyInfo = model.GetType().GetProperty(bindingProperty);
                if ((propertyInfo == null) ||
                    (bindingProperty.Equals("Excel_WordingFilePath") && (model.Btn_WordingEnabled == false)) ||
                    (bindingProperty.Equals("Excel_FinalFilePath") && (model.Btn_FinalEnabled == false)) ||
                    (bindingProperty.Equals("Excel_MsgNoFilePath") && (model.Btn_MsgNoEnabled == false)))
                {
                    return;
                }

                propertyInfo.SetValue(model, openFileDialog.FileName);
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }
        
        private void Event_OpenFolder_OutPutPath(string bindingProperty)
        {
            try
            {
                if (string.IsNullOrEmpty(bindingProperty))
                {
                    return;
                }
                Forms.FolderBrowserDialog openFolderDialog = new Forms.FolderBrowserDialog();
                if (openFolderDialog.ShowDialog() == Forms.DialogResult.OK)
                {
                    PropertyInfo propertyInfo = model.GetType().GetProperty(bindingProperty);
                    if ((propertyInfo == null) ||
                        (bindingProperty.Equals("OutPutPath") && (model.Btn_OutPutEnabled == false)) ||
                        (bindingProperty.Equals("ProjectPath") && (model.Btn_OutPutEnabled == false)))
                    {
                        return;
                    }

                    propertyInfo.SetValue(model, openFolderDialog.SelectedPath);
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
                return;
            }
        }

        private void Event_SettingsWindow_Start(string useless)
        {
            SettingsViewModel.GetInstance(this);
        }

        private void Event_TextBox_ProjectPathChanged(string useless)
        {
            string LinkfileFolder = string.Empty;
            string HmiStridFolder = string.Empty;
            foreach (var item in model.DataGridItems)
            {
                modelXml.GetFilePath(item.ModelName, item.ModelType, ref LinkfileFolder, ref HmiStridFolder);
                LinkfileFolder = Path.Combine(model.ProjectPath, LinkfileFolder);
                HmiStridFolder = Path.Combine(model.ProjectPath, HmiStridFolder);
                item.LinkFilePath = LinkfileFolder;
                item.HmiFilePath = HmiStridFolder;
            }
        }
        #endregion

        public bool Process_Step_Start()
        {
            bool processSucceed = false;
            GC.Collect();
            model.Load_Visibility = CONST.ITEM_VISIBLE;

            switch (processIndex)
            {
                case 0:
                    try
                    {
                        processSucceed = Process_Step_Init();
                    }
                    catch (Exception e) { LogFile.WriteLog(e.Message); }
                    break;
                case 1:
                    try
                    {
                        if (model.Text_WordingColor == CONST.ITEM_COLOR_PINK) { break; }
                        model.Btn_WordingEnabled = false;
                        Process_TextBlock_MsgChanged(0,"文言申請システム ファイルを読み取っています...");
                        Console.WriteLine("文言申請システム ファイルを読み取っています...");

                        if (CONST.KEY_MODE_SPEED.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_VERSION, CONST.KEY_VERSION_MODE)))
                        {
                            processSucceed = Process_ReadData_WordingExcel();
                        }
                        else
                        {
                            processSucceed = Process_ReadData_InteropWordingExcel();
                        }
                        if (processSucceed)
                        {
                            processSucceed = Process_FillData_DataGridItems();
                        }
                    }
                    catch (Exception e) { LogFile.WriteLog(e.Message); }

                    break;
                case 2:
                    try
                    {
                        if (model.Text_FinalColor == CONST.ITEM_COLOR_PINK) { break; }
                        model.Btn_FinalEnabled = false;
                        Process_TextBlock_MsgChanged(0,"最終文言エクセル ファイルを読み取っています...");
                        Console.WriteLine("最終文言エクセル ファイルを読み取っています...");

                        //将文件更新到最新
                        ProcessHelper.Excute("p4.exe",
                            " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                            " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                            " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                            " -C shiftjis sync " + model.Excel_FinalFilePath,
                            "");

                        if (CONST.KEY_MODE_SPEED.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_VERSION, CONST.KEY_VERSION_MODE)))
                        {
                            processSucceed = Process_ReadData_FinalExcel();
                        }
                        else
                        {
                            processSucceed = Process_ReadData_InteropFinalExcel();
                        }
                    }
                    catch (Exception e) { LogFile.WriteLog(e.Message); }

                    break;
                case 3:
                    try
                    {
                        if (model.Text_MsgNoColor == CONST.ITEM_COLOR_PINK) { break; }
                        model.Btn_MsgNoEnabled = false;
                        Process_TextBlock_MsgChanged(0,"MsgNo使用状況 ファイルを読み取っています...");
                        Console.WriteLine("MsgNo使用状況 ファイルを読み取っています...");
                        processSucceed = Process_ReadData_MsgNoExcel();
                    }
                    catch (Exception e) { LogFile.WriteLog(e.Message); }

                    break;
                case 4:
                    try
                    {
                        if (model.Text_OutPutColor == CONST.ITEM_COLOR_PINK) { break; }
                        if (excuteError || Process_DataGrid_ItemReady())
                        {
                            excuteError = false;
                            DirectoryInfo directoryInfo = new DirectoryInfo(model.OutPutPath);
                            directoryInfo.Create();
                            model.Btn_OutPutEnabled = false;
                            Process_TextBlock_MsgChanged(0, "プログラム 実施を待ち...");
                            Console.WriteLine("プログラム 実施を待ち...");
                            if (Process_CheckData_SearchExcel())
                            {
                                processSucceed = Process_CompareData_WordingAndFinal();
                            }
                            if (wordingSearchExcel != null)
                            {
                                wordingSearchExcel.SaveExcel();
                                wordingSearchExcel.CloseExcel();
                                wordingSearchExcel = null;

                                string wordingSearchPath = Path.Combine(model.OutPutPath, CONST.FILENAME_WORDINGBAK);

                                if (File.Exists(wordingSearchPath))
                                {   //如果存在文言申請システム_検索用文件，则删除
                                    File.Delete(wordingSearchPath);
                                }
                            }
                        }
                        else
                        {
                            Process_TextBlock_MsgChanged(1, "格納場所が不正,あるいは処理が必要のアイテムがありません!");
                            Console.WriteLine("格納場所が不正,あるいは処理が必要のアイテムがありません!");
                            excuteError = true;
                            processSucceed = true;
                        }
                    }
                    catch (Exception e) { LogFile.WriteLog(e.Message); }

                    break;
                default:
                    break;
            }

            return processSucceed;
        }

        private void Process_Step_End(IAsyncResult iar)
        {
            AsyncResult ar = (AsyncResult)iar;
            ProcessStep process = (ProcessStep)ar.AsyncDelegate;
            bool result = process.EndInvoke(iar);

            model.Load_Visibility = CONST.ITEM_COLLAPSED;

            if (result)
            {
                switch (processIndex)
                {
                    case 0: //初始化操作
                    case 1: //文言申請システムファイルを読取
                    case 2: //最終文言エクセルファイルを読取
                    case 3: //MsgNo使用状況ファイルを読取
                        break;
                    case 4:
                        model.ProcessStatus = MWindowModel.Process_Status.WAIT;
                        if (excuteError)
                        {
                            model.ProcessStatus = MWindowModel.Process_Status.READY;
                            excuteError = false;
                        }
                        else
                        {
                            Process_TextBlock_MsgChanged(0, "プログラム 実施に完了しました");
                            Console.WriteLine("プログラム 実施に完了しました");
                            if (newList.Count > 2)
                            {
                                Console.WriteLine("プログラム 実施に完了しました");
                                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                            }
                        }
                        break;
                    default:
                        break;
                }
                if (model.ProcessStatus == MWindowModel.Process_Status.PROCESS)
                {
                    processIndex++;
                    processStep = new ProcessStep(Process_Step_Start);
                    processStep.BeginInvoke(Process_Step_End, null);
                }
            }
            else
            {
                model.ProcessStatus = MWindowModel.Process_Status.READY;
                switch (processIndex)
                {
                    case 0:
                        Process_TextBlock_MsgChanged(1, "プログラム 初期化に失敗しました、ログを参照してください!");
                        Console.WriteLine("プログラム 初期化に失敗しました、ログを参照してください!");
                        break;
                    case 1:
                        model.Btn_WordingEnabled = true;
                        Process_TextBlock_MsgChanged(1,"文言申請システム ファイルを読み取りに失敗しました");
                        Console.WriteLine("文言申請システム ファイルを読み取りに失敗しました");
                        break;
                    case 2:
                        model.Btn_FinalEnabled = true;
                        Process_TextBlock_MsgChanged(1,"最終文言エクセル ファイルを読み取りに失敗しました");
                        Console.WriteLine("最終文言エクセル ファイルを読み取りに失敗しました");
                        break;
                    case 3:
                        model.Btn_MsgNoEnabled = true;
                        Process_TextBlock_MsgChanged(1,"MsgNo使用状況 ファイルを読み取りに失敗しました");
                        Console.WriteLine("MsgNo使用状況 ファイルを読み取りに失敗しました");
                        break;
                    case 4:
                        excuteError = true;
                        Process_TextBlock_MsgChanged(1,"プログラム 実施に失敗しました,ログを参照してください!");
                        Console.WriteLine("プログラム 実施に失敗しました,ログを参照してください!");
                        break;
                    default:
                        break;
                }
            }
        }

        private void Process_Resource_End()
        {
            try
            {
                if (wordingData != null) { wordingData = null; }
                if (finalData != null) { finalData = null; }
                if (msgnoData != null) { msgnoData = null; }
                if (wordingSearchExcel != null)
                {
                    wordingSearchExcel.SaveExcel();
                    wordingSearchExcel.CloseExcel();
                    wordingSearchExcel = null;

                    string wordingSearchPath = Path.Combine(model.OutPutPath, CONST.FILENAME_WORDINGBAK);

                    if (File.Exists(wordingSearchPath))
                    {   //如果存在文言申請システム_検索用文件，则删除
                        File.Delete(wordingSearchPath);
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }

        #region 读取数据

        private bool Process_Step_Init()
        {
            bool processSucceed = false;

            try
            {
                do
                {
                    if (File.Exists(CONST.FILEPATH_CONFIGURE))
                    {   //如果存在配置文件Configure.ini
                        modelXml = new ModelXmlHelper();
                    }
                    else
                    {
                        LogFile.WriteLog(CONST.FILEPATH_CONFIGURE + "ファイルがありません");
                        Console.WriteLine(CONST.FILEPATH_CONFIGURE + "ファイルがありません");
                        break;
                    } 

                    if (!File.Exists(CONST.FILEPATH_MODELINFO))
                    {   //如果不存在配置文件ModelInfo.XML
                        LogFile.WriteLog(CONST.FILEPATH_MODELINFO + "ファイルがありません");
                        Console.WriteLine(CONST.FILEPATH_MODELINFO + "ファイルがありません");
                        break;
                    }
                    processSucceed = true;
                } while (false);
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        private void Process_TextBlock_MsgChanged(int err,string textMsg)
        {
            try
            {
                processText = new ProcessTextScroll(Process_TextBlock_MsgScroll);  
                IAsyncResult iar = processText.BeginInvoke(err, textMsg, null, null); //开始异步调用  
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }

        private bool Process_TextBlock_MsgScroll(int err, string textMsg)
        {
            bool processSucceed = false;
            string textTemp = string.Empty;

            try
            {
                lock (textLock)
                {
                    if (model.TextBlock_Message3 != string.Empty)
                    {
                        model.TextBlock_Message3 = string.Empty;
                    }
                    if (model.TextBlock_Message2 != string.Empty)
                    {
                        model.TextBlock_Message3 = model.TextBlock_Message2;
                    }
                    if (model.TextBlock_Message1 != string.Empty)
                    {
                        model.TextBlock_Message2 = model.TextBlock_Message1;
                    }

                    if (err == 0)
                    {
                        textMsg = "☆" + textMsg;
                        if (model.TextBlock_Color != CONST.ITEM_COLOR_BLACK)
                        {
                            model.TextBlock_Color = CONST.ITEM_COLOR_BLACK;
                        }
                        
                    }
                    else
                    {
                        textMsg = "★" + textMsg;
                        if (model.TextBlock_Color != CONST.ITEM_COLOR_RED)
                        {
                            model.TextBlock_Color = CONST.ITEM_COLOR_RED;
                        }
                    }
                    for (int i = textMsg.Length - 1; i >= 0; i--)
                    {
                        textTemp = textMsg[i] + textTemp;
                        model.TextBlock_Message1 = textTemp;
                        Thread.Sleep(20);
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
       
            return processSucceed;
        }

        private bool Process_Resource_Save()
        {
            bool processSucceed = false;

            try
            {
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_WORDING, model.Excel_WordingFilePath);
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_MSGNO, model.Excel_MsgNoFilePath);
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_OUTPUT, model.OutPutPath);
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_PROJECT, model.ProjectPath);
                processSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        private bool Process_CheckData_SearchExcel()
        {
            bool processSucceed = false;
            bool loginSucceed = true;

            try
            {
                if (wordingSearchExcel == null)
                {
                    processWait = new ProcessStep(Process_ReadData_SearchExcel);
                    IAsyncResult iar = processWait.BeginInvoke(null, null); //开始异步调用  
                    while (!iar.IsCompleted)
                    {
                        IntPtr staHandle = FindWindow("ThunderDFrame", "文言申請システム");
                        if (staHandle != IntPtr.Zero)
                        {
                            ShowWindow(staHandle, 0);
                        }

                        IntPtr msgHandle = FindWindow(null, "文言申請システム");
                        if (msgHandle != IntPtr.Zero)
                        {
                            IntPtr errHandl = GetDlgItem(msgHandle, wordingErrorID);
                            if (errHandl != IntPtr.Zero)
                            {
                                const int buffer_size = 1024;
                                StringBuilder buffer = new StringBuilder(buffer_size);
                                SendMessage(errHandl, WM_GETTEXT, buffer_size, buffer);
                                if (buffer.ToString().Contains("入力されたユーザは、存在しません") ||
                                    buffer.ToString().Contains("ユーザIDが入力されていません"))
                                {
                                    LogFile.WriteLog("文言申請システムのユーザは存在しません、設定ファイルを修正してください!");
                                    Console.Write("文言申請システムのユーザは存在しません、設定ファイルを修正してください!");
                                    loginSucceed = false;
                                }
                            }
                            //查找Button
                            IntPtr sbtnHandle = FindWindowEx(msgHandle, 0, "Button", "OK");
                            if (sbtnHandle != IntPtr.Zero)
                            {
                                SendMessage(sbtnHandle, BM_CLICK, 0, null);
                            }
                            //查找Button
                            IntPtr mbtnHandle = FindWindowEx(msgHandle, 0, "Button", "はい(&Y)");

                            if (mbtnHandle != IntPtr.Zero)
                            {
                                SendMessage(mbtnHandle, BM_CLICK, 0, null);

                            }
                        }
                        Thread.Sleep(10);
                    }
                    if (loginSucceed && processWait.EndInvoke(iar))
                    {
                        processSucceed = true;
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        private bool Process_ReadData_SearchExcel()
        {
            bool processSucceed = false;

            try
            {
                string wordingSearchPath = Path.Combine(model.OutPutPath, CONST.FILENAME_WORDINGBAK);

                if (File.Exists(wordingSearchPath))
                {   //如果存在文言申請システム_検索用文件，则删除
                    File.Delete(wordingSearchPath);
                }
                string Excel_WordingFilePath_extension = Path.GetExtension(model.Excel_WordingFilePath);
                string wordingSearchPath_extension = Path.GetExtension(wordingSearchPath);
                if (!wordingSearchPath.Equals(Excel_WordingFilePath_extension))
                {
                    wordingSearchPath = wordingSearchPath.Replace(wordingSearchPath_extension, Excel_WordingFilePath_extension);
                }

                File.Copy(model.Excel_WordingFilePath, wordingSearchPath, true);

                wordingSearchExcel = new WordingExcelHandler(wordingSearchPath);
                if (wordingSearchExcel.OpenExcel(true))
                {
                    wordingSearchExcel.ClearProcessingForm();
                    if (wordingSearchExcel.Login() &&
                        wordingSearchExcel.SearchExcel("Test")) //随意查找
                    {
                        processSucceed = true;
                    }
                }

                if (!processSucceed)
                {
                    Process_TextBlock_MsgChanged(1,"文言申請システム_検索用 ファイルを操作に失敗しました");
                    Console.Write("文言申請システム_検索用 ファイルを操作に失敗しました");
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        private bool Process_ReadData_WordingExcel()
        {
            bool processSucceed = false;

            try
            {
                NPOIExcelHelper wordingExcel = new NPOIExcelHelper(model.Excel_WordingFilePath);
                if (wordingExcel.OpenExcel())
                {
                    if (wordingExcel.GetStartCol(CONST.EXCEL_WORDING_SHEET, CONST.EXCEL_WORDING_STARTLINE, ref wordingStartCol) && (wordingStartCol != -1))
                    {
                        if (wordingExcel.ReadExcel(CONST.EXCEL_WORDING_SHEET, CONST.EXCEL_WORDING_STARTLINE, ref wordingData))
                        {
                            if (wordingData != null)
                            {
                                wordingData.DefaultView.RowFilter = "[" + CONST.EXCEL_WORDING_SERIES + "] <> '' and [" + CONST.EXCEL_WORDING_SERIES + "] is not null";
                                wordingData = wordingData.DefaultView.ToTable();
                                processSucceed = true;
                            }
                        }
                    }
                }
                wordingExcel.CloseExcel();
                wordingExcel = null;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        private bool Process_ReadData_InteropWordingExcel()
        {
            bool processSucceed = false;

            try
            {
                WordingExcelHandler wordingExcel = new WordingExcelHandler(model.Excel_WordingFilePath);
                if (wordingExcel.OpenExcel(false))
                {
                    if (wordingExcel.GetStartCol(CONST.EXCEL_WORDING_SHEET, Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "WordingExcelData", "ResultStartLine")), ref wordingStartCol) && (wordingStartCol != -1))
                    {
                        wordingStartCol--;
                        if (wordingExcel.ReadExcel(ref wordingData))
                        {
                            if (wordingData != null)
                            {
                                wordingData.DefaultView.RowFilter = "[" + CONST.EXCEL_WORDING_SERIES + "] <> '' and [" + CONST.EXCEL_WORDING_SERIES + "] is not null";
                                wordingData = wordingData.DefaultView.ToTable();
                                processSucceed = true;
                            }
                        }
                    }
                }
                wordingExcel.CloseExcel();
                wordingExcel = null;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        private bool Process_FillData_DataGridItems()
        {
            bool processSucceed = false;
            try
            {
                Dictionary<String, ArrayList> dict = new Dictionary<String, ArrayList>();
                string seriesTemp = string.Empty;
                string modelTemp = string.Empty;
                string LinkfileFolder = string.Empty;
                string HmiStridFolder = string.Empty;

                for (int i=0; i<wordingData.Rows.Count; i++)
                {
                    seriesTemp = wordingData.Rows[i][CONST.EXCEL_WORDING_SERIES].ToString();
                    modelTemp = wordingData.Rows[i][CONST.EXCEL_WORDING_MODEL].ToString();
                    if (dict.ContainsKey(seriesTemp))
                    {
                        if (!dict[seriesTemp].Contains(modelTemp))
                        {
                            dict[seriesTemp].Add(modelTemp);
                        }
                    }
                    else
                    {
                        ArrayList array = new ArrayList();
                        array.Add(modelTemp);
                        dict.Add(seriesTemp, array);
                    }
                }

                foreach(var itemDict in dict)
                {
                    foreach (var value in itemDict.Value) {
                        modelXml.GetFilePath(itemDict.Key, value.ToString(), ref LinkfileFolder, ref HmiStridFolder);
                        LinkfileFolder = Path.Combine(model.ProjectPath, LinkfileFolder);
                        HmiStridFolder = Path.Combine(model.ProjectPath, HmiStridFolder);
                        wordingData.DefaultView.RowFilter = "[" + CONST.EXCEL_WORDING_SERIES + "] = '"+ itemDict.Key+"' and [" + CONST.EXCEL_WORDING_MODEL + "] = '" + value.ToString() + "'";

                        DataGridItem item = new DataGridItem()
                        {
                            ModelName = itemDict.Key,
                            ModelType = value.ToString(),
                            LinkFilePath = LinkfileFolder,
                            HmiFilePath = HmiStridFolder,
                            LineEnabled = true,
                            Procedure = "[ 0 / "+wordingData.DefaultView.ToTable().Rows.Count.ToString()+" ]"
                        };
                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ParameterizedThreadStart(Process_DataGrid_AddItem), item);
                    }
                }
                if (dict.Count > 0)
                {
                    processSucceed = true;
                }
                else
                {
                    LogFile.WriteLog("文言申請システム ファイルでリンクする必要があるの内容がありません");
                    Console.Write("文言申請システム ファイルでリンクする必要があるの内容がありません");
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        private bool Process_DataGrid_ItemReady()
        {
            bool itemReady = false;
            bool itemExsit = false;
            try
            {
                Process_DataGrid_ItemUpdate();

                foreach (var item in model.DataGridItems)
                {
                    if (item.LineEnabled == true)
                    {
                        itemExsit = true;
                        if (item.Text_LinkFileColor == CONST.ITEM_COLOR_PINK)
                        {
                            LogFile.WriteLog("[" + item.ModelName + "] [" + item.ModelType + "] のリンクファイル格納場所あるいはhmi_strid_def.h格納場所が不正!");
                            Console.Write("[" + item.ModelName + "] [" + item.ModelType + "] のリンクファイル格納場所あるいはhmi_strid_def.h格納場所が不正!");
                            continue;
                        }
                        if (item.Text_HmiFileColor == CONST.ITEM_COLOR_PINK)
                        {
                            LogFile.WriteLog("[" + item.ModelName + "] [" + item.ModelType + "] のhmi_strid_def.h格納場所が不正!");
                            Console.Write("[" + item.ModelName + "] [" + item.ModelType + "] のhmi_strid_def.h格納場所が不正!");
                            continue;
                        }
                        itemReady = true;
                    }
                }

                if (!itemExsit)
                {
                    itemReady = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return itemReady;
        }

        private bool Process_DataGrid_ItemUpdate()
        {
            bool updateSucceed = false;

            modelXml = null;
            modelXml = new ModelXmlHelper();

            string LinkfileFolder = string.Empty;
            string HmiStridFolder = string.Empty;

            try
            {
                foreach (var item in model.DataGridItems)
                {
                    if (item.LineEnabled == true)
                    {
                        if ( (item.Text_LinkFileColor == CONST.ITEM_COLOR_PINK) || (item.Text_HmiFileColor == CONST.ITEM_COLOR_PINK) )
                        {
                            modelXml.GetFilePath(item.ModelName, item.ModelType, ref LinkfileFolder, ref HmiStridFolder);
                            item.LinkFilePath = Path.Combine(model.ProjectPath, LinkfileFolder);
                            item.HmiFilePath = Path.Combine(model.ProjectPath, HmiStridFolder);
                        }
                    }
                }
                updateSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return updateSucceed;
        }

        private void Process_DataGrid_AddItem(object item)
        {
            model.DataGridItems.Add((DataGridItem)item);
        }

        private bool Process_ReadData_FinalExcel()
        {
            bool processSucceed = false;
            try
            {
                NPOIExcelHelper finalExcel = new NPOIExcelHelper(model.Excel_FinalFilePath);
                if (finalExcel.OpenExcel())
                {
                    if (finalExcel.GetStartCol(CONST.EXCEL_FINAL_SHEET, CONST.EXCEL_FINAL_STARTLINE, ref finalStartCol) && (finalStartCol != -1))
                    {
                        if (finalExcel.ReadExcel(CONST.EXCEL_FINAL_SHEET, CONST.EXCEL_FINAL_STARTLINE, ref finalData))
                        {
                            if (finalData != null)
                            {
                                processSucceed = true;
                            }
                        }
                    }
                }
                finalExcel.CloseExcel();
                finalExcel = null;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        private bool Process_ReadData_InteropFinalExcel()
        {
            bool processSucceed = false;
            try
            {
                FinalExcelHandler finalExcel = new FinalExcelHandler(model.Excel_FinalFilePath);
                if (finalExcel.OpenExcel(false))
                {
                    if (finalExcel.GetStartCol(CONST.EXCEL_FINAL_SHEET, Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, "FinalExcelData", "TitleLine")), ref finalStartCol) && (finalStartCol != -1))
                    {
                        finalStartCol--;
                        if (finalExcel.ReadExcel(ref finalData))
                        {
                            if (finalData != null)
                            {
                                processSucceed = true;
                            }
                        }
                    }
                }
                finalExcel.CloseExcel();
                finalExcel = null;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        private bool Process_ReadData_MsgNoExcel()
        {
            bool processSucceed = false;

            try
            {
                NPOIExcelHelper msgnoExcel = new NPOIExcelHelper(model.Excel_MsgNoFilePath);
                if (msgnoExcel.OpenExcel())
                {
                    if (msgnoExcel.ReadExcel(CONST.EXCEL_MSGNO_SHEET, CONST.EXCEL_MSGNO_STARTLINE, ref msgnoData))
                    {
                        if (msgnoData != null)
                        {
                            int i = 0;
                            while((msgnoData.Rows.Count - 1 - i) >= 0)
                            {
                                string msgnoString = msgnoData.Rows[msgnoData.Rows.Count - 1 - i][CONST.EXCEL_MSGNO_MSGNO].ToString();
                                Regex reg = new Regex(@"([\d]+)(?:[\w]*)$");
                                Match match = reg.Match(msgnoString);
                                if (match.Success)
                                {
                                    msgNo = Convert.ToInt32(match.Groups[1].Value);
                                    processSucceed = true;
                                    break;
                                }
                                i++;
                            }
                        }
                    }
                }
                msgnoExcel.CloseExcel();
                msgnoExcel = null;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }
        #endregion

        #region 保存数据
        //保存hinagata.txt等文件
        private void Process_SaveData_LinkCommonFile(string modelName, string modelType, string LinkfileFolder, string HmiFileFolder)
        {
            try
            {
                hinagataUpdated = false;
                stridLinkUpdated = false;

                string saveFolder = Path.Combine(model.OutPutPath, modelName + "(" + modelType+")");

                wordingData.DefaultView.RowFilter = "[" + CONST.EXCEL_WORDING_SERIES + "] = '" + modelName + "' and [" + CONST.EXCEL_WORDING_MODEL + "] = '" + modelType + "'";
                DataTable wordingAfter = wordingData.DefaultView.ToTable();

                //将文件更新到最新
                if (IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_CHANGELIST_UPDATE) == "1")
                {
                    ProcessHelper.Excute("p4.exe",
                        " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                        " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                        " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                        " -C shiftjis sync " + Path.Combine(LinkfileFolder, CONST.FILENAME_HINAGATA) +
                        " " + Path.Combine(LinkfileFolder, CONST.FILENAME_STRIDLINK),
                        "");
                }
                HinagataFile hinagata = new HinagataFile(Path.Combine(LinkfileFolder, CONST.FILENAME_HINAGATA));
                if (!hinagata.ReadFileData() || !hinagata.UpdateFileData(saveFolder, wordingAfter, ref hinagataUpdated))
                {
                    Process_TextBlock_MsgChanged(1,"[" + modelName + "] [" + modelType + "]の" + CONST.FILENAME_HINAGATA + "更新に失敗しました");
                }

                StridLinkFile linkFile = new StridLinkFile(Path.Combine(LinkfileFolder, CONST.FILENAME_STRIDLINK));
                if (!linkFile.ReadFileData() || !linkFile.UpdateFileData(saveFolder, wordingAfter, ref stridLinkUpdated))
                {
                    Process_TextBlock_MsgChanged(1,"[" + modelName + "] [" + modelType + "]の" + CONST.FILENAME_STRIDLINK + "更新に失敗しました");
                }

                HmiStridFile hmistrFile = new HmiStridFile(Path.Combine(HmiFileFolder, CONST.FILENAME_HMISTRID));
                if (!hmistrFile.ReadFileData() || !hmistrFile.CheckFileData(wordingAfter))
                {
                    Process_TextBlock_MsgChanged(1,"[" + modelName + "] [" + modelType + "]の" + CONST.FILENAME_HMISTRID + "確認に失敗しました");
                }

                Process_MergeData_LinkCommonFile(LinkfileFolder, saveFolder);
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }

        //保存hinagata.txt等文件
        private void Process_MergeData_LinkCommonFile(string LinkfileFolder, string OutputFolder)
        {
            try
            {
                if (Directory.Exists(LinkfileFolder) && Directory.Exists(OutputFolder))
                {
                    //输出文件后直接启动比较工具进行比较
                    if (CONST.KEY_KIND_MERGE.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND)))
                    {
                        processMerge = new ProcessMerge(Process_Merge_MergeAppStart);
                        processMerge.BeginInvoke(LinkfileFolder, OutputFolder, null, null);
                    }
                    //输出文件后直接Merge到Perforce上
                    else if (CONST.KEY_KIND_PERFORCE.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND)))
                    {
                        processMerge = new ProcessMerge(Process_Merge_MergeToPerforce);
                        IAsyncResult iar = processMerge.BeginInvoke(LinkfileFolder, OutputFolder, null, null);
                        processMerge.EndInvoke(iar);
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }

        private void Process_Merge_MergeAppStart(string LinkFileFolder, string OutputFolder)
        {
            try
            {
                ProcessHelper.Excute(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_MERGEAPP_PATH), " /r " + LinkFileFolder + " " + OutputFolder,"");
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }

        private void Process_Merge_MergeToPerforce(string LinkFileFolder, string OutputFolder)
        {
            string Output = string.Empty;
            string ChangeListArg = string.Empty;
            try
            {
                if (CONST.KEY_CHANGELIST_DEFAULT.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST)))   //添加到default changelist
                {
                    ChangeListArg = "";
                }
                else if (CONST.KEY_CHANGELIST_CREATE.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST)))    //添加到新规的 changelist
                {
                    //如果需要保存的WorkSpace已经变更则新规ChangeList
                    if ((!IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT).Equals(MergeWorkSpace)) ||
                        (MergeChangeList == string.Empty))
                    {
                        MergeWorkSpace = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT);
                        //新建ChangeList
                        Output = ProcessHelper.Excute("p4.exe",
                            " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                            " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                            " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                            " change -i", 
                            "Change: new\r\nStatus: new\r\nDescription:\t < 文言リンク作業 新規@" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss >")
                            );
                        if (Output.Contains("created"))
                        {
                            //获取新建的ChangeList并保存
                            Output = ProcessHelper.Excute("p4.exe",
                                " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                                " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                                " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                                " changes -m 1","");
                            Match match = Regex.Match(Output, "(?<=Change )(.*?)(?= )", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                            if (match.Success)
                            {
                                MergeChangeList = match.Value;
                            } else { return; }
                        }
                    }
                    ChangeListArg = " -c "+ MergeChangeList;
                }
                else    //添加到指定的 changelist
                {
                    ChangeListArg = " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST);
                }

                if (hinagataUpdated)
                {
                    //将文件保存到ChangeList
                    Output = ProcessHelper.Excute("p4.exe",
                        " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                        " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                        " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                        " edit " + ChangeListArg +
                        " " + Path.Combine(LinkFileFolder, CONST.FILENAME_HINAGATA),
                        "");
                    if (Output.Contains("reopen"))   //已经打开
                    {
                        Output = ProcessHelper.Excute("p4.exe",
                        " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                        " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                        " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                        " reopen " + ChangeListArg +
                        " " + Path.Combine(LinkFileFolder, CONST.FILENAME_HINAGATA),
                        "");
                    }
                    if (Output.Contains("opened for edit") || Output.Contains("reopened"))
                    {//打开之后，则替换文件
                        if (File.Exists(Path.Combine(OutputFolder, CONST.FILENAME_HINAGATA)))
                        {
                            File.Copy(Path.Combine(OutputFolder, CONST.FILENAME_HINAGATA), Path.Combine(LinkFileFolder, CONST.FILENAME_HINAGATA), true);
                        }
                    }
                }

                if (stridLinkUpdated)
                {
                    Output = ProcessHelper.Excute("p4.exe",
                        " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                        " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                        " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                        " edit " + ChangeListArg +
                        " " + Path.Combine(LinkFileFolder, CONST.FILENAME_STRIDLINK),
                        "");
                    if (Output.Contains("reopen"))   //已经打开
                    {
                        Output = ProcessHelper.Excute("p4.exe",
                        " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                        " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                        " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                        " reopen " + ChangeListArg +
                        " " + Path.Combine(LinkFileFolder, CONST.FILENAME_STRIDLINK),
                        "");
                    }
                    if (Output.Contains("opened for edit") || Output.Contains("reopened"))
                    {//打开之后，则替换文件
                        if (File.Exists(Path.Combine(OutputFolder, CONST.FILENAME_STRIDLINK)))
                        {
                            File.Copy(Path.Combine(OutputFolder, CONST.FILENAME_STRIDLINK), Path.Combine(LinkFileFolder, CONST.FILENAME_STRIDLINK), true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }

        //保存文言申请系统文件内容
        private bool Process_SaveData_WordingExcel()
        {
            bool processSucceed = false;

            try
            {
                string path = Path.Combine(model.OutPutPath, CONST.FILENAME_WORDINGSAVE);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                string Excel_WordingFilePath_extension = Path.GetExtension(model.Excel_WordingFilePath);
                string wordingSearchPath_extension = Path.GetExtension(path);
                if (!path.Equals(Excel_WordingFilePath_extension))
                {
                    path = path.Replace(wordingSearchPath_extension, Excel_WordingFilePath_extension);
                }
                File.Copy(model.Excel_WordingFilePath, path, true);

                WordingExcelHandler wordingSaveExcel = new WordingExcelHandler(Path.GetFullPath(Path.Combine(model.OutPutPath, CONST.FILENAME_WORDINGSAVE)));

                if (wordingSaveExcel.OpenExcel(false) &&
                    wordingSaveExcel.WriteExcel(CONST.EXCEL_WORDING_SHEET, Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE,CONST.SETCION_WORDINGEXCEL,CONST.KEY_WORDINGEXCEL_TITLELINE)), wordingDict))
                {
                    processSucceed = true;
                }
                if (processSucceed)
                {
                    wordingSaveExcel.SaveExcel();
                    wordingDict = null;
                    wordingDict = new Dictionary<int[], string>();
                }

                wordingSaveExcel.CloseExcel();
                wordingSaveExcel = null;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        //保存最终文言文件内容
        private bool Process_MergeData_FinalExcel()
        {
            string Output = string.Empty;
            string ChangeListArg = string.Empty;
            bool processSucceed = false;

            try
            {
                if (CONST.KEY_KIND_PERFORCE.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND)))
                {
                    if (CONST.KEY_CHANGELIST_DEFAULT.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST)))   //添加到default changelist
                    {
                        ChangeListArg = "";
                    }
                    else if (CONST.KEY_CHANGELIST_CREATE.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST)))    //添加到新规的 changelist
                    {
                        //如果需要保存的WorkSpace已经变更则新规ChangeList
                        if ((!IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT).Equals(MergeWorkSpace)) ||
                            (MergeChangeList == string.Empty))
                        {
                            MergeWorkSpace = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT);
                            //新建ChangeList
                            Output = ProcessHelper.Excute("p4.exe",
                                " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                                " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                                " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                                " change -i",
                                "Change: new\r\nStatus: new\r\nDescription:\t < 文言リンク作業 新規@" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss >")
                                );
                            if (Output.Contains("created"))
                            {
                                //获取新建的ChangeList并保存
                                Output = ProcessHelper.Excute("p4.exe",
                                    " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                                    " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                                    " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                                    " changes -m 1", "");
                                Match match = Regex.Match(Output, "(?<=Change )(.*?)(?= )", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                                if (match.Success)
                                {
                                    MergeChangeList = match.Value;
                                }
                                else { return false; }
                            }
                        }
                        ChangeListArg = " -c " + MergeChangeList;
                    }
                    else    //添加到指定的 changelist
                    {
                        ChangeListArg = " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST);
                    }

                    //将文件保存到ChangeList
                    Output = ProcessHelper.Excute("p4.exe",
                        " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                        " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                        " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                        " edit " + ChangeListArg +
                        " " + model.Excel_FinalFilePath,
                        "");
                    if (Output.Contains("reopen"))   //已经打开
                    {
                        Output = ProcessHelper.Excute("p4.exe",
                        " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) +
                        " -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER) +
                        " -c " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT) +
                        " reopen " + ChangeListArg +
                        " " + model.Excel_FinalFilePath,
                        "");
                    }
                    if (Output.Contains("opened for edit") || Output.Contains("reopened"))
                    {
                        processSucceed = true;
                    }
                }
                else
                {
                    processSucceed = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        private bool Process_SaveData_FinalExcel()
        {
            bool processSucceed = false;
            bool editSucceed = true;

            try
            {
                if (Process_MergeData_FinalExcel())
                {
                    string path = Path.Combine(model.OutPutPath, CONST.FILENAME_FINALSAVE);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    File.Copy(model.Excel_FinalFilePath, path, true);
                    // 判断文件的属性是否是ReadOnly
                    if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        // 如果是将文件的属性设置为Normal
                        File.SetAttributes(path, FileAttributes.Normal);
                    }
                    FinalExcelHandler finalSaveExcel = new FinalExcelHandler(Path.GetFullPath(path));

                    if (finalSaveExcel.OpenExcel(true))
                    {
                        if ((finalSave != null) && (finalSave.Rows.Count > 0))  //有追加
                        {
                            if (!finalSaveExcel.WriteExcel(CONST.EXCEL_FINAL_SHEET, Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_FINALEXCEL, CONST.KEY_FINALEXCEL_TITLELINE)), finalSave, true, finalSave.Columns.IndexOf(CONST.EXCEL_COMMON_MSGNO), finalSave.Columns.IndexOf(CONST.EXCEL_FINAL_CHNWORDCOUNT)))
                            {
                                editSucceed = false;
                            }
                        }

                        if (finalDict.Count > 0) //有更新
                        {
                            if (!finalSaveExcel.WriteExcel(CONST.EXCEL_FINAL_SHEET, Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_FINALEXCEL, CONST.KEY_FINALEXCEL_TITLELINE)), finalDict))
                            {
                                editSucceed = false;
                            }
                        }
                    }

                    if (editSucceed)
                    {
                        finalSaveExcel.SaveExcel();
                        finalDict = null;
                        finalSave = null;
                        finalDict = new Dictionary<int[], string>();

                        if (CONST.KEY_KIND_PERFORCE.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND)))
                        { 
                            if (File.Exists(model.Excel_FinalFilePath))
                            {
                                File.Delete(model.Excel_FinalFilePath);
                            }
                            File.Copy(path, model.Excel_FinalFilePath, true);
                        }
                        processSucceed = true;
                    }

                    finalSaveExcel.CloseExcel();
                    finalSaveExcel = null;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }
        #endregion

        #region 匹配数据

        //匹配文言申请系统文件和最终文言文件的基本国内容
        private bool Process_CompareData_WordingAndFinal()
        {
            bool processSucceed = false;
            DataGridItem currentitem = null;
            string firstJPN = string.Empty;
            string lastJPN = string.Empty;

            try
            {
                foreach (var item in model.DataGridItems)
                {
                    currentitem = item;
                    if (item.LineEnabled == true)
                    {
                        if (!File.Exists(Path.Combine(item.LinkFilePath, CONST.FILENAME_HINAGATA)) ||
                            !File.Exists(Path.Combine(item.LinkFilePath, CONST.FILENAME_STRIDLINK)) ||
                            !File.Exists(Path.Combine(item.HmiFilePath, CONST.FILENAME_HMISTRID)))
                        {
                            LogFile.WriteLog("[" + item.ModelName + "] [" + item.ModelType + "] のリンクファイル格納場所あるいはhmi_strid_def.h格納場所が不正!");
                            Process_TextBlock_MsgChanged(1,"[" + item.ModelName + "] [" + item.ModelType + "] は処理に失敗しました!");
                            continue;
                        }
                        Process_TextBlock_MsgChanged(0,"[" + item.ModelName + "] [" + item.ModelType + "] " + "作業しています...");

                        if (!modelXml.GetJPN(item.ModelName, item.ModelType, ref firstJPN, ref lastJPN))
                        {
                            Process_TextBlock_MsgChanged(1,"[" + item.ModelName + "] [" + item.ModelType + "] " + "のModelInfoがエラー、ModelInfo.xmlを修正してください");
                            LogFile.WriteLog("[" + item.ModelName + "] [" + item.ModelType + "] " + "のModelInfoがエラー、ModelInfo.xmlを修正してください");
                            continue;
                        }

                        wordingData.DefaultView.RowFilter = "[" + CONST.EXCEL_WORDING_SERIES + "] = '" + item.ModelName + "' and [" + CONST.EXCEL_WORDING_MODEL + "] = '" + item.ModelType + "'";
                        DataTable wordingAfter = wordingData.DefaultView.ToTable();

                        item.LineEnabled = false;

                        int dealwithItem = 0;
                        for (int i = 0; i < wordingAfter.Rows.Count; i++)
                        {
                            if (Compare_DealWith_ByLine(wordingAfter, i))
                            {
                                dealwithItem++;
                                item.Procedure = "[ " + dealwithItem + " / " + wordingAfter.Rows.Count + " ]";
                            }
                        }
                        if (dealwithItem < wordingAfter.Rows.Count)
                        {
                            item.LineEnabled = true;
                        }
                        Process_SaveData_LinkCommonFile(item.ModelName, item.ModelType, item.LinkFilePath, item.HmiFilePath);
                    }
                }

                if (Process_DataGrid_ItemReady())
                {
                    processSucceed = true;

                    //保存最终文言文件
                    if ((finalSave != null) || (finalDict.Count > 0))
                    {
                        Process_TextBlock_MsgChanged(0, "最終文言エクセル ファイルを保存しています...");
                        if (!Process_SaveData_FinalExcel())
                        {
                            processSucceed = false;
                            Process_TextBlock_MsgChanged(1, "最終文言エクセル ファイルは更新に失敗しました");
                            LogFile.WriteLog("最終文言エクセル ファイルは更新に失敗しました");
                        }
                    }

                    //保存文言申请流用文件
                    if (wordingDict.Count > 0)
                    {
                        Process_TextBlock_MsgChanged(0, "文言申請システム ファイルを保存しています...");

                        processWait = new ProcessStep(Process_SaveData_WordingExcel);
                        IAsyncResult iar = processWait.BeginInvoke(null, null); //开始异步调用  
                        while (!iar.IsCompleted)
                        {
                            IntPtr msgHandle = FindWindow(null, "文言申請システム");
                            if (msgHandle != IntPtr.Zero)
                            {
                                //查找Button
                                IntPtr sbtnHandle = FindWindowEx(msgHandle, 0, "Button", "OK");
                                if (sbtnHandle != IntPtr.Zero)
                                {
                                    SendMessage(sbtnHandle, BM_CLICK, 0, null);
                                }
                            }
                            Thread.Sleep(50);
                        }

                        if (!processWait.EndInvoke(iar)) //调用EndInvoke来获取结果 
                        {
                            processSucceed = false;
                            Process_TextBlock_MsgChanged(1, "文言申請システム ファイルは更新に失敗しました");
                            LogFile.WriteLog("文言申請システム ファイルは更新に失敗しました");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                processSucceed = false;
                LogFile.WriteLog(e.Message);
            }
            return processSucceed;
        }

        //处理文言申请系统文件的其中一条记录
        private bool Compare_DealWith_ByLine(DataTable wordingAfter, int currentRow)
        {
            bool processSucceed = false;
            try
            {
                bool wordingMatched = false;
                if (DealWith_ByLine_WordingMatched(wordingAfter, currentRow, ref wordingMatched))
                {
                    
                    if (wordingMatched)     //在文言系统里面已找到
                    {
                        processSucceed = true;
                    }
                    else  //如果在文言系统里面没有找到基本国一致且申请字数相符的则在最终文言里查找
                    {
                        //依頼種別はJpn Only
                        if (CONST.EXCEL_WORDING_JPNONLY.Equals(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_DESTINATION].ToString()))
                        {
                            int findRow = -1;
                            DataTable finalAfter = finalData.Copy();
                            if (DealWith_ByLine_JPNMatched(finalAfter, wordingAfter, currentRow, ref findRow))
                            {
                                int findIndex = SearchTable_ByColume_FindIndex(wordingData, CONST.EXCEL_WORDING_NO, wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_NO].ToString());
                                if ((findRow != -1) && (findIndex != -1)) //JPNのみ的已经匹配到
                                {
                                    wordingData.Rows[findIndex][CONST.EXCEL_COMMON_MSGNO] = finalData.Rows[findRow][CONST.EXCEL_COMMON_MSGNO];
                                    wordingData.Rows[findIndex][CONST.EXCEL_WORDING_STRINGID] = finalData.Rows[findRow][CONST.EXCEL_FINAL_STRINGID];
                                    wordingDict.Add(new int[] { findIndex + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_WORDINGEXCEL, CONST.KEY_WORDINGEXCEL_TITLELINE)) + 1, wordingAfter.Columns.IndexOf(CONST.EXCEL_COMMON_MSGNO) + wordingStartCol + 1 }, finalData.Rows[findRow][CONST.EXCEL_COMMON_MSGNO].ToString());
                                    wordingDict.Add(new int[] { findIndex + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_WORDINGEXCEL, CONST.KEY_WORDINGEXCEL_TITLELINE)) + 1, wordingAfter.Columns.IndexOf(CONST.EXCEL_WORDING_STRINGID) + wordingStartCol + 1 }, finalData.Rows[findRow][CONST.EXCEL_FINAL_STRINGID].ToString());
                                    processSucceed = true;
                                }
                                else
                                {
                                    DataTable finalNull = null;
                                    processSucceed = DealWith_ByLine_CreateRow(wordingAfter, currentRow, ref finalNull);
                                }
                            }
                            else
                            {
                                processSucceed = true;
                            }
                        }
                        else
                        {
                            processSucceed = DealWith_ByLine_UsUkMatched(wordingAfter, currentRow);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //生成需要在最终文言文件追加的内容
        private bool DealWith_ByLine_CreateRow(DataTable wordingAfter, int currentRow, ref DataTable finalAfter)
        {
            bool processSucceed = false;
            int langStart = 0;
            int langEnd = 0;
            try
            {
                string firstJPN = string.Empty;
                string lastJPN = string.Empty;
                if (modelXml.GetJPN(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_SERIES].ToString(), wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_MODEL].ToString(), ref firstJPN, ref lastJPN))
                {
                    if (finalSave == null) finalSave = finalData.Clone();
                    DataRow dr = finalSave.NewRow();
                    //填充MsgNo
                    dr[CONST.EXCEL_COMMON_MSGNO] = CONST.EXCEL_COMMON_MSGNO + (msgNo + 1).ToString() + CONST.EXCEL_COMMON_LCD;
                    msgNo += 1; //MsgNo加一，以备下一次使用
                    //填充StringID
                    int index = 2;
                    List<string> splitStr = wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_HMISTRINGID].ToString().Replace("HS_", "").Split('_').ToList();
                    if (splitStr.Count > 3)
                    {
                        dr[CONST.EXCEL_FINAL_STRINGID] = splitStr[0] + "_" + splitStr[1] + "_" + splitStr[2] + CONST.EXCEL_WORDING_STRINGIDEND;
                    }
                    else if (splitStr.Count > 2)
                    {
                        dr[CONST.EXCEL_FINAL_STRINGID] = splitStr[0] + "_" + splitStr[1] + CONST.EXCEL_WORDING_STRINGIDEND;
                    }
                    else
                    {
                        //HMI String IDが「N/A」登録の文言処理,Linkファイル処理を不要とする(FIRM_ALL_MODEL-29699)
                        if (splitStr.Count == 1
                            && nAStrList.Find(o => o == splitStr[0]) != null
                            && wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_STRINGID].ToString() != string.Empty
                            && wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_STRINGID].ToString() != ""
                            && wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_STRINGID].ToString().Contains(CONST.EXCEL_WORDING_STRINGIDEND))
                        {
                            dr[CONST.EXCEL_FINAL_STRINGID] = wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_STRINGID].ToString();
                        }
                        else
                        {
                        dr[CONST.EXCEL_FINAL_STRINGID] = splitStr[0] + CONST.EXCEL_WORDING_STRINGIDEND;
                        }
                    }
                    while (true)
                    {
                        finalData.DefaultView.RowFilter = "[" + CONST.EXCEL_FINAL_STRINGID + "]='" + dr[CONST.EXCEL_FINAL_STRINGID].ToString() + "'";
                        DataTable stridAfter = finalData.DefaultView.ToTable();
                        if (stridAfter.Rows.Count > 0)
                        {
                            if (dr[CONST.EXCEL_FINAL_STRINGID].ToString().Contains("_P" + (index - 1) + "_"))
                            {
                                dr[CONST.EXCEL_FINAL_STRINGID] = dr[CONST.EXCEL_FINAL_STRINGID].ToString().Replace("_P" + (index - 1) + "_", "_P" + index + "_");
                            }
                            else
                            {
                                char[] separator = { '_' }; string indexPlusStr = "P" + (index - 1).ToString();
                                string[] sArray = dr[CONST.EXCEL_FINAL_STRINGID].ToString().Split(separator);// 根据字符"_"去分割字符
                                string regexResult = Regex.Replace(sArray[sArray.Length - 2], @"[^0-9]+", "");// 判断最后第二位是不是含有数字
                                // 判断最后第二位是不是含有字符"P"并含有数字
                                if ((sArray[sArray.Length - 2].Contains("P")) && (regexResult != null && regexResult != ""))
                                {
                                    sArray[sArray.Length - 2] = indexPlusStr;
                                    dr[CONST.EXCEL_FINAL_STRINGID] = string.Join("_", sArray);
                                }
                                else
                                {
                                    dr[CONST.EXCEL_FINAL_STRINGID] = dr[CONST.EXCEL_FINAL_STRINGID].ToString().Replace(CONST.EXCEL_WORDING_STRINGIDEND, "_P" + (index - 1) + CONST.EXCEL_WORDING_STRINGIDEND);
                                }
                            }
                            index++;
                        }
                        else { break; }
                    }
                    //填充申请字数和申请行数
                    if (CONST.EXCEL_WORDING_FONT_1.Equals(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_FONT].ToString()))
                    {
                        dr[CONST.EXCEL_FINAL_WORDCOUNT] = ((Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_WORDCOUNT])) * (Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_LINENUM]))).ToString();
                        dr[CONST.EXCEL_FINAL_LINENUM] = wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_LINENUM];
                    }
                    else
                    {
                        int tempNum = 0;
                        int applyWidth = Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_APPLYWIDTH]);
                        dr[CONST.EXCEL_FINAL_LINENUM] = Math.Round(((double)(Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_APPLYHEIGHT]) / (Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_FONTHEIGHT])))), MidpointRounding.AwayFromZero).ToString();

                        if (applyWidth % 6 == 0)
                        {
                            tempNum = (applyWidth / 6) % 2 == 0 ? applyWidth / 6 : applyWidth / 6 + 1;
                        }
                        else
                        {
                            tempNum = (applyWidth / 6) % 2 == 0 ? applyWidth / 6 + 2 : applyWidth / 6 + 1;
                        }
                        dr[CONST.EXCEL_FINAL_WORDCOUNT] = (Convert.ToInt32(dr[CONST.EXCEL_FINAL_LINENUM]) * tempNum).ToString();
                    }

                    //填充翻译文字
                    langStart = finalSave.Columns.IndexOf(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_FINALEXCEL, CONST.KEY_FINALEXCEL_LANGSTART));
                    langEnd = finalSave.Columns.IndexOf(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_FINALEXCEL, CONST.KEY_FINALEXCEL_LANGEND)) + 1;
                    for (int i = langStart; i < langEnd; i++)
                    {
                        dr[i] = CONST.EXCEL_COMMON_HONYAKU;
                    }
                    if (CONST.EXCEL_WORDING_JPNONLY.Equals(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_DESTINATION].ToString()))
                    {
                        //JPNのみの場合
                        string JPE = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_JPE].ToString());
                        //基本国(UK,USA,JPN)の登録が「N/A」の場合,最終文言エクセルには「__(T_T)__HONYAKU」を出力する(FIRM_ALL_MODEL-29418)
                        if (CONST.EXCEL_COMMON_NA.Equals(JPE) || (JPE == string.Empty) || nAStrList.Find(o => o == JPE) != null)
                        {
                            dr[CONST.EXCEL_FINAL_JPE] = CONST.EXCEL_COMMON_HONYAKU;
                        }
                        else
                        {
                            dr[CONST.EXCEL_FINAL_JPE] = JPE;
                        }
                    }
                    else
                    {
                        List<string> listJPN = new List<string>();
                        if (!modelXml.GetAllJPN(ref listJPN) || (listJPN.Count < 1))
                        {
                            LogFile.WriteLog(CONST.FILEPATH_MODELINFO + "でエラーがあり、解決後でプログラムを再実行してください");
                            return false;
                        }
                        listJPN.Remove(firstJPN);
                        if (finalAfter != null)
                        {
                            DataTable finalTemp = finalAfter.Copy();
                            for (int i = langStart; i < langEnd; i++)
                            {
                                //忽略JPN相关列的复制
                                if (listJPN.Contains(finalTemp.Columns[i].ToString()))
                                {
                                    continue;
                                }
                                dr[i] = finalTemp.Rows[0][i];
                            }
                        }

                        string UK = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_UK].ToString());
                        string USA = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_US].ToString());
                        dr[CONST.EXCEL_FINAL_UK] = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_UK].ToString());
                        dr[CONST.EXCEL_FINAL_US] = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_US].ToString());
                        string JPE = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_JPE].ToString());
                        //基本国(UK,USA,JPN)の登録が「N/A」の場合,最終文言エクセルには「__(T_T)__HONYAKU」を出力する(FIRM_ALL_MODEL-29418)
                        if (CONST.EXCEL_COMMON_NA.Equals(JPE) || (JPE == string.Empty) || nAStrList.Find(o => o == JPE) != null)
                        {
                            dr[CONST.EXCEL_FINAL_JPE] = CONST.EXCEL_COMMON_HONYAKU;
                        }
                        else
                        {
                            dr[CONST.EXCEL_FINAL_JPE] = JPE;
                        }
                        if (CONST.EXCEL_COMMON_NA.Equals(USA) || (USA == string.Empty) || nAStrList.Find(o => o == USA) != null)
                        {
                            dr[CONST.EXCEL_FINAL_US] = CONST.EXCEL_COMMON_HONYAKU;
                        }
                        if (CONST.EXCEL_COMMON_NA.Equals(UK) || (UK == string.Empty) || nAStrList.Find(o => o == UK) != null)
                        {
                            dr[CONST.EXCEL_FINAL_UK] = CONST.EXCEL_COMMON_HONYAKU;
                        }
                    }
                    //填充JPN首选列,Write dr[firstJPN]
                    string jpnString = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_JPN].ToString());
                    if (CONST.EXCEL_COMMON_NA.Equals(jpnString) || (jpnString == string.Empty) || nAStrList.Find(o => o == jpnString) != null)
                    {
                        dr[firstJPN] = CONST.EXCEL_COMMON_HONYAKU;
                    }
                    else
                    {
                    dr[firstJPN] = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_JPN].ToString());
                    }

                    finalSave.Rows.Add(dr);
                    DataRow fdSaveRow = finalData.NewRow();
                    fdSaveRow.ItemArray = dr.ItemArray;
                    finalData.Rows.Add(fdSaveRow);

                    int findIndex = SearchTable_ByColume_FindIndex(wordingData, CONST.EXCEL_WORDING_NO, wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_NO].ToString());
                    if (-1 != findIndex)
                    {
                        wordingData.Rows[findIndex][CONST.EXCEL_COMMON_MSGNO] = dr[CONST.EXCEL_COMMON_MSGNO];
                        wordingData.Rows[findIndex][CONST.EXCEL_WORDING_STRINGID] = dr[CONST.EXCEL_FINAL_STRINGID];
                        wordingDict.Add(new int[] { findIndex + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_WORDINGEXCEL, CONST.KEY_WORDINGEXCEL_TITLELINE)) + 1, wordingAfter.Columns.IndexOf(CONST.EXCEL_COMMON_MSGNO) + wordingStartCol + 1 }, dr[CONST.EXCEL_COMMON_MSGNO].ToString());
                        wordingDict.Add(new int[] { findIndex + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_WORDINGEXCEL, CONST.KEY_WORDINGEXCEL_TITLELINE)) + 1, wordingAfter.Columns.IndexOf(CONST.EXCEL_WORDING_STRINGID) + wordingStartCol + 1 }, dr[CONST.EXCEL_FINAL_STRINGID].ToString());
                        processSucceed = true;
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }
        /// <summary>
        /// 处理DataRow筛选条件的特殊字符
        /// </summary>
        /// <param name="rowFilter">行筛选条件表达式</param>
        /// <returns></returns>
        public static string DvRowFilter(string rowFilter)
        {
            //在DataView的RowFilter里面的特殊字符要用"[]"括起来，单引号要换成"''"
            return rowFilter.Replace("[", "[[ ")
                .Replace("]", " ]]")
                .Replace("*", "[*]")
                .Replace("%", "[%]")
                .Replace("[[ ", "[[]")
                .Replace(" ]]", "[]]")
                .Replace("\'", "''");
        }
        //在最终文言表中匹配US，UK相同的文言
        private bool DealWith_ByLine_UsUkMatched(DataTable wordingAfter, int currentRow)
        {
            string tmpFilter = string.Empty;
            bool processSucceed = false;
            try
            {
                string beforeReplaceUSA = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_US].ToString());
                string beforeReplaceUK = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_UK].ToString());
                string USA = DvRowFilter(beforeReplaceUSA);
                string UK = DvRowFilter(beforeReplaceUK);

                if ((USA != string.Empty) && !CONST.EXCEL_COMMON_NA.Equals(USA))
                {
                    tmpFilter += "["+CONST.EXCEL_FINAL_US + "]='" + USA + "'";
                }

                if ((UK != string.Empty) && !CONST.EXCEL_COMMON_NA.Equals(UK))
                {
                    if (tmpFilter != string.Empty)
                    {
                        tmpFilter += " and ";
                    }
                    tmpFilter += "["+CONST.EXCEL_FINAL_UK + "]='" + UK + "'";
                }

                if (tmpFilter == string.Empty) return false;

                finalData.DefaultView.RowFilter = tmpFilter;
                DataTable finalAfter = finalData.DefaultView.ToTable();

                if (finalAfter.Rows.Count >= 1)     //US,UK相同的已经匹配到
                {
                    int findRow = -1;
                    if (DealWith_ByLine_JPNMatched(finalAfter, wordingAfter, currentRow, ref findRow))
                    {
                        int findIndex = SearchTable_ByColume_FindIndex(wordingData, CONST.EXCEL_WORDING_NO, wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_NO].ToString());
                        if ((findRow != -1) && (-1 != findIndex)) //JPN也匹配到
                        {
                            wordingData.Rows[findIndex][CONST.EXCEL_COMMON_MSGNO] = finalData.Rows[findRow][CONST.EXCEL_COMMON_MSGNO];
                            wordingData.Rows[findIndex][CONST.EXCEL_WORDING_STRINGID] = finalData.Rows[findRow][CONST.EXCEL_FINAL_STRINGID];
                            wordingDict.Add(new int[] { findIndex + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_WORDINGEXCEL, CONST.KEY_WORDINGEXCEL_TITLELINE)) + 1, wordingAfter.Columns.IndexOf(CONST.EXCEL_COMMON_MSGNO) + wordingStartCol + 1 }, finalData.Rows[findRow][CONST.EXCEL_COMMON_MSGNO].ToString());
                            wordingDict.Add(new int[] { findIndex + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_WORDINGEXCEL, CONST.KEY_WORDINGEXCEL_TITLELINE)) + 1, wordingAfter.Columns.IndexOf(CONST.EXCEL_WORDING_STRINGID) + wordingStartCol + 1 }, finalData.Rows[findRow][CONST.EXCEL_FINAL_STRINGID].ToString());
                            processSucceed = true;
                        }
                        else    //US,UK匹配到，JPN未匹配到
                        {
                            DataTable finalNull = null;
                            processSucceed = DealWith_ByLine_CreateRow(wordingAfter, currentRow, ref finalNull);
                        }
                    }
                    else
                    {
                        processSucceed = true;
                    }
                }
                else    //US,UK未匹配到
                {
                    DataTable finalNull = null;
                    processSucceed = DealWith_ByLine_CreateRow(wordingAfter, currentRow, ref finalNull);
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //在最终文言表中匹配申请行数，字数相符的文言
        private bool DealWith_ByLine_LineNumMatched(ref DataTable finalAfter, DataTable wordingAfter, int currentRow)
        {
            bool processSucceed = false;

            try
            {
                string lineNum = string.Empty;
                string wordCount = string.Empty;

                //计算出申请字数和申请行数
                if (CONST.EXCEL_WORDING_FONT_1.Equals(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_FONT].ToString()))
                {
                    wordCount = ((Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_WORDCOUNT])) * (Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_LINENUM]))).ToString();
                    lineNum = wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_LINENUM].ToString();
                }
                else
                {
                    int tempNum = 0;
                    int applyWidth = Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_APPLYWIDTH]);
                    lineNum = Math.Round(((double)(Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_APPLYHEIGHT]) / (Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_FONTHEIGHT])))), MidpointRounding.AwayFromZero).ToString();

                    if (applyWidth % 6 == 0)
                    {
                        tempNum = (applyWidth / 6) % 2 == 0 ? applyWidth / 6 : applyWidth / 6 + 1;
                    }
                    else
                    {
                        tempNum = (applyWidth / 6) % 2 == 0 ? applyWidth / 6 + 2 : applyWidth / 6 + 1;
                    }
                    wordCount = (Convert.ToInt32(lineNum) * tempNum).ToString();
                }

                for (int i = finalAfter.Rows.Count - 1; i >= 0; i--)
                {
                    //如果申请行数不相等或者最终文言中申请字数大于文言申请中的申请字数，则不匹配
                    if ((Convert.ToInt32(lineNum) < Convert.ToInt32(finalAfter.Rows[i][CONST.EXCEL_FINAL_LINENUM])) ||
                        (Convert.ToInt32(wordCount) < Convert.ToInt32(finalAfter.Rows[i][CONST.EXCEL_FINAL_WORDCOUNT])) )
                    {
                        finalAfter.Rows.RemoveAt(i);
                        continue;
                    }
                }
                if (finalAfter.Rows.Count > 0)
                {
                    processSucceed = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //在最终文言表中匹配JPE相同的文言
        private bool DealWith_ByLine_JPEMatched(ref DataTable finalAfter,string JPE)
        {
            bool processSucceed = false;

            try
            {
                if ((JPE != string.Empty) && !JPE.Equals(CONST.EXCEL_COMMON_NA))
                {
                    for (int i = finalAfter.Rows.Count - 1; i >= 0; i--)
                    {
                        if (CONST.EXCEL_COMMON_HONYAKU.Equals(finalAfter.Rows[i][CONST.EXCEL_FINAL_JPE].ToString()))
                        {   //如果JPE为__(T_T)__HONYAKU并且和USA不相等，则弃用
                            if (!JPE.Equals(finalAfter.Rows[i][CONST.EXCEL_FINAL_US].ToString()))
                            {
                                finalAfter.Rows.RemoveAt(i);
                                continue;
                            }
                        }
                        else
                        {   //如果JPE不相等并且不为__(T_T)__HONYAKU，则弃用
                            if (!JPE.Equals(finalAfter.Rows[i][CONST.EXCEL_FINAL_JPE].ToString()))
                            {
                                finalAfter.Rows.RemoveAt(i);
                                continue;
                            }
                        }
                    }
                }
                if (finalAfter.Rows.Count > 0)
                {
                    processSucceed = true;
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //在最终文言表中匹配JPN相同的文言
        private bool DealWith_ByLine_JPNMatched(DataTable finalAfter, DataTable wordingAfter, int currentRow, ref int findRow)
        {
            bool processSucceed = false;

            try
            {
                DataTable finalTemp = finalAfter.Copy();
                string JPE = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_JPE].ToString());
                if (DealWith_ByLine_JPEMatched(ref finalTemp, JPE) &&
                    DealWith_ByLine_LineNumMatched(ref finalTemp, wordingAfter, currentRow))
                {
                    //依頼種別はJpn Only
                    if (CONST.EXCEL_WORDING_JPNONLY.Equals(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_DESTINATION].ToString()))
                    {
                        processSucceed = DealWith_ByLine_JPNOnlyMatchedSub(finalTemp, wordingAfter, currentRow, ref findRow);
                    }
                    else
                    {
                        processSucceed = DealWith_ByLine_JPNMatchedSub(finalTemp, wordingAfter, currentRow, ref findRow);
                    }
                    
                    if (!processSucceed)
                    {
                        DealWith_ByLine_CreateRow(wordingAfter, currentRow, ref finalTemp);
                    }
                }
                else
                {
                    DataTable finalNull = null;
                    DealWith_ByLine_CreateRow(wordingAfter, currentRow, ref finalNull);
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //在最终文言表中匹配JPN相同的文言(非JPN Only)
        private bool DealWith_ByLine_JPNMatchedSub(DataTable finalAfter, DataTable wordingAfter, int currentRow, ref int findRow)
        {
            bool processSucceed = false;

            try
            {
                string firstJPN = string.Empty;
                string lastJPN = string.Empty;
                if (modelXml.GetJPN(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_SERIES].ToString(), wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_MODEL].ToString(), ref firstJPN, ref lastJPN))
                {
                    string JPN = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_JPN].ToString());
                    int findIndex = -1;
                    //如果JPN为N/A或者为空,则任选其一
                    if (CONST.EXCEL_COMMON_NA.Equals(JPN) || (JPN == string.Empty))
                    {
                        findRow = SearchTable_ByColume_FindIndex(finalData, CONST.EXCEL_COMMON_MSGNO, finalAfter.Rows[0][CONST.EXCEL_COMMON_MSGNO].ToString());
                        findIndex = 0;
                        processSucceed = true;
                    }
                    else
                    {
                        for (int i = 0; i < finalAfter.Rows.Count; i++)
                        {
                            if (firstJPN != string.Empty)
                            {
                                if (JPN.Equals(finalAfter.Rows[i][firstJPN].ToString()))
                                {
                                    findRow = SearchTable_ByColume_FindIndex(finalData, CONST.EXCEL_COMMON_MSGNO, finalAfter.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString());
                                    findIndex = i;
                                    processSucceed = true;
                                    break;
                                }
                                //JPN首选列不为“_(T_T)_HONYAKU”，则未匹配到
                                if (!CONST.EXCEL_COMMON_HONYAKU.Equals(finalAfter.Rows[i][firstJPN].ToString()))
                                {
                                    continue;
                                }
                            }
                            
                            if (JPN.Equals(finalAfter.Rows[i][lastJPN].ToString()))
                            {
                                findRow = SearchTable_ByColume_FindIndex(finalData, CONST.EXCEL_COMMON_MSGNO, finalAfter.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString());
                                findIndex = i;
                                processSucceed = true;
                                break;
                            }
                            else
                            {
                                //JPN备选列不为“_(T_T)_HONYAKU”
                                if (!CONST.EXCEL_COMMON_HONYAKU.Equals(finalAfter.Rows[i][lastJPN].ToString()))
                                {
                                    if (DealWith_ByLine_WordingUsed(finalAfter, lastJPN, i))
                                    { //文言在使用
                                        continue;
                                    }
                                }

                                findRow = SearchTable_ByColume_FindIndex(finalData, CONST.EXCEL_COMMON_MSGNO, finalAfter.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString());
                                if (-1 != findRow)
                                {
                                    finalDict.Add(new int[] { findRow + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_FINALEXCEL, CONST.KEY_FINALEXCEL_TITLELINE)) + 1, finalAfter.Columns.IndexOf(firstJPN) + finalStartCol + 1 }, JPN);
                                }
                                findIndex = i;
                                processSucceed = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //在最终文言表中匹配JPN相同的文言(JPN Only)
        private bool DealWith_ByLine_JPNOnlyMatchedSub(DataTable finalAfter, DataTable wordingAfter, int currentRow, ref int findRow)
        {
            bool processSucceed = false;

            try
            {
                string firstJPN = string.Empty;
                string lastJPN = string.Empty;
                if (modelXml.GetJPN(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_SERIES].ToString(), wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_MODEL].ToString(), ref firstJPN, ref lastJPN))
                {
                    string JPN = Transform_WordingCode(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_JPN].ToString());
                    int findIndex = -1;
                    //JPN不为N/A并且不为空
                    if (!CONST.EXCEL_COMMON_NA.Equals(JPN) && (JPN != string.Empty))
                    {
                        for (int i = 0; i < finalAfter.Rows.Count; i++)
                        {
                            if (firstJPN != string.Empty)
                            {
                                if (JPN.Equals(finalAfter.Rows[i][firstJPN].ToString()))
                                {
                                    findRow = SearchTable_ByColume_FindIndex(finalData, CONST.EXCEL_COMMON_MSGNO, finalAfter.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString());
                                    findIndex = i;
                                    processSucceed = true;
                                    break;
                                }
                                //JPN首选列不为“_(T_T)_HONYAKU”，则未匹配到
                                if (!CONST.EXCEL_COMMON_HONYAKU.Equals(finalAfter.Rows[i][firstJPN].ToString()))
                                {
                                    continue;
                                }
                            }

                            if (JPN.Equals(finalAfter.Rows[i][lastJPN].ToString()))
                            {
                                findRow = SearchTable_ByColume_FindIndex(finalData, CONST.EXCEL_COMMON_MSGNO, finalAfter.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString());
                                findIndex = i;
                                processSucceed = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //在文言申请系统文件匹配已经存在基本国相同的文言
        private bool DealWith_ByLine_WordingMatched(DataTable wordingAfter, int currentRow,ref bool wordingMatched)
        {
            bool processSucceed = false;
            bool tpModel = true;

            string modelType = wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_MODEL].ToString();

            if ((modelType == "1_LINE") || (modelType == "2_LINE"))
            {
                tpModel = false;
            }

            try
            {
                DataTable searchData = null;
                wordingSearchExcel.SearchExcel(wordingAfter.Rows[currentRow], tpModel);
                Thread.Sleep(500);
                wordingSearchExcel.ReadExcel(ref searchData);
                Thread.Sleep(500);
                if (searchData != null && searchData.Rows.Count > 0)
                {
                    for (int i = 0; i < searchData.Rows.Count; i++)
                    {
                        if ((searchData.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString() != string.Empty) // MsgNo != null
                            && (searchData.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString() != string.Empty) // String ID != null
                            && (!CONST.EXCEL_WORDING_REQUESTCANCEL.Equals(searchData.Rows[i][CONST.EXCEL_WORDING_REQUESTTYPE].ToString()))) // 依頼種別：取消は含まない
                        {
                            if (!tpModel && (Convert.ToInt32(searchData.Rows[i][CONST.EXCEL_WORDING_WORDCOUNT].ToString()) > Convert.ToInt32(wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_WORDCOUNT].ToString())))
                            {
                                continue;
                            }

                            int findIndex = SearchTable_ByColume_FindIndex(wordingData, CONST.EXCEL_WORDING_NO, wordingAfter.Rows[currentRow][CONST.EXCEL_WORDING_NO].ToString());
                            if (-1 != findIndex)
                            {
                                wordingData.Rows[findIndex][CONST.EXCEL_COMMON_MSGNO] = searchData.Rows[i][CONST.EXCEL_COMMON_MSGNO];
                                wordingData.Rows[findIndex][CONST.EXCEL_WORDING_STRINGID] = searchData.Rows[i][CONST.EXCEL_FINAL_STRINGID];
                                wordingDict.Add(new int[] { findIndex + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_WORDINGEXCEL, CONST.KEY_WORDINGEXCEL_TITLELINE)) + 1, wordingAfter.Columns.IndexOf(CONST.EXCEL_COMMON_MSGNO) + wordingStartCol + 1 }, searchData.Rows[i][CONST.EXCEL_COMMON_MSGNO].ToString());
                                wordingDict.Add(new int[] { findIndex + Convert.ToInt32(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_WORDINGEXCEL, CONST.KEY_WORDINGEXCEL_TITLELINE)) + 1, wordingAfter.Columns.IndexOf(CONST.EXCEL_WORDING_STRINGID) + wordingStartCol + 1 }, searchData.Rows[i][CONST.EXCEL_WORDING_STRINGID].ToString());
                                wordingMatched = true;
                                break;
                            }
                        }
                    }
                }
                processSucceed = true;
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //在文言申请系统文件匹配某条记录是否已经在使用
        private bool DealWith_ByLine_WordingUsed(DataTable finalAfter, string jpnName, int currentRow)
        {
            bool processSucceed = false;

            try
            {
                DataTable searchData = null;
                Dictionary<String, ArrayList> dict = new Dictionary<String, ArrayList>();
                modelXml.GetByFirstJPN(jpnName, ref dict);

                if (wordingSearchExcel.SearchExcel(finalAfter.Rows[currentRow][CONST.EXCEL_COMMON_MSGNO].ToString()) &&
                    wordingSearchExcel.ReadExcel(ref searchData))
                {
                    for (int i = 0; i < searchData.Rows.Count; i++)
                    {

                        if (CONST.EXCEL_WORDING_REQUESTCANCEL.Equals(searchData.Rows[i][CONST.EXCEL_WORDING_REQUESTTYPE].ToString()))
                        {
                            continue;
                        }
                        foreach (var d in dict)
                        {
                            if (d.Key.Equals(searchData.Rows[i][CONST.EXCEL_WORDING_SERIES].ToString()))
                            {
                                foreach(string v in d.Value)
                                {
                                    if (v.Equals(searchData.Rows[i][CONST.EXCEL_WORDING_MODEL].ToString()))
                                    {
                                        processSucceed = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }

            return processSucceed;
        }

        //在数据表中匹配某条内容的位置
        private int SearchTable_ByColume_FindIndex(DataTable data, string columnStr, string Value)
        {
            int  findIndex = -1;

            try
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (Value.Equals(data.Rows[i][columnStr]))
                    {
                        findIndex = i;
                        break;
                    }
                }
            }
            catch { }

            return findIndex;
        }

        //文字替换
        private string Transform_WordingCode(string value)
        {
            string returnval = value;
            foreach (string dicKey in CONST.g_WordingChangeData.Keys)
            {
                if (value.Contains(dicKey))
                {
                    returnval = value.Replace(dicKey, CONST.g_WordingChangeData[dicKey]);
                }
            }
            return returnval;
        }

        #endregion
    }

    //设置框
    class SettingsViewModel
    {

        #region 变量初始化

        public SettingsModel model { get; private set; }
        private static SettingsViewModel settingsVMInstance = null;
        private static SettingsWindow settingWindow = null;
        public static ViewModel mainVM = null;

        #endregion

        public delegate void ProcessExcute(); //声明委托类型
        ProcessExcute processExcute = null; //新建委托

        //构造函数
        public SettingsViewModel(SettingsModel e)
        {
            model = e;

            model.Btn_Close = new DelegateCommand<string>(Event_Button_Close);
            model.Btn_Save = new DelegateCommand<string>(Event_Button_Save);
            model.SettingsWindow_Closed = new DelegateCommand<string>(Event_SettingsWindow_Closed);
            

            model.TBox_UserPortChanged = new DelegateCommand<string>(Event_TextBox_UserPortChanged);
            model.CBox_ClientChanged = new DelegateCommand<string>(Event_ComboBox_ClientChanged);
            model.OpenFileDialog_MergeApp = new DelegateCommand< string > (Event_OpenFileDialog_MergeApp);

            try
            {
                model.Output_Kind = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND);
                if (CONST.KEY_KIND_MERGE.Equals(model.Output_Kind))
                {
                    model.GBox_MergeEnabled = true;
                }
                else if (CONST.KEY_KIND_PERFORCE.Equals(model.Output_Kind))
                {
                    model.GBox_PerforceEnabled = true;
                }
                else
                {
                    //Do Nothing
                }

                model.Version_Kind = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_VERSION, CONST.KEY_VERSION_MODE);

                string output = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SETTINGS, CONST.KEY_SETTINGS_MODE);
                if (output == "1")
                {
                    model.OutputItem_Visibility = CONST.ITEM_VISIBLE;
                }

                output = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_CHANGELIST_UPDATE);
                if (output == "1")
                {
                    model.CheckBox_UpdateLatest = true;
                }

                model.MergeAppPath = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_MERGEAPP_PATH);
                model.UserName = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER);
                model.ServicePort = IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT);

                processExcute = new ProcessExcute(Process_Excute_GetClient);
                processExcute.BeginInvoke(null, null);
            }
            catch { }
        }

        public static SettingsViewModel GetInstance(ViewModel viewModel)
        {
            GC.Collect();
            mainVM = viewModel;
            if (settingsVMInstance == null)
            {
                settingWindow = new SettingsWindow();
                SettingsModel settingModel = new SettingsModel();
                settingsVMInstance = new SettingsViewModel(settingModel);
                settingWindow.DataContext = settingModel;
                settingWindow.Show();
                settingWindow.Activate();
            }
            else
            {
                if (settingWindow != null) { settingWindow.Activate(); }
            }

            return settingsVMInstance;
        }

        /* Close The Window */
        private void Event_Button_Close(string useless)
        {
            if (model.Output_Kind.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND)) &&
                model.Version_Kind.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_VERSION, CONST.KEY_VERSION_MODE)))
            {
                model.SettingsWindow_Enabled = false;
            }
            else
            {
                if (MessageBox.Show("数据已变更，确定退出?", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    model.SettingsWindow_Enabled = false;
                }
            }
        }
        
        private void Event_TextBox_UserPortChanged(string useless)
        {
            processExcute = new ProcessExcute(Process_Excute_GetClient);
            processExcute.BeginInvoke(null, null);
        }

        private void Event_SettingsWindow_Closed(string useless)
        {
            settingsVMInstance = null;
            settingWindow = null;
            Process_FillData_ClientPath();
            GC.Collect();
        }

        private void Event_Button_Save(string useless)
        {
            if (model.Output_Kind.Equals(CONST.KEY_KIND_MERGE))
            {
                if (model.MergeAppPath == string.Empty)
                {
                    MessageBox.Show("请输入差分程序的路径！", "错误");
                    return;
                }
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_MERGEAPP_PATH, model.MergeAppPath);
            }
            else if (model.Output_Kind.Equals(CONST.KEY_KIND_PERFORCE))
            {
                if ((model.UserName == string.Empty) || (model.ServicePort == string.Empty) || (model.CBox_ChangeListIndex == -1) || (model.CBox_ClientIndex == -1))
                {
                    MessageBox.Show("输入信息有误！", "错误");
                    return;
                }
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER, model.UserName);
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT, model.ServicePort);
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT, model.CBox_ClientName[model.CBox_ClientIndex]);
                IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST, model.CBox_ChangeListName[model.CBox_ChangeListIndex].ID);
                if (model.CheckBox_UpdateLatest)
                {
                    IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_CHANGELIST_UPDATE, "1");
                }
                else
                {
                    IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_CHANGELIST_UPDATE, "0");
                }
            }
            else
            {
                //DO Nothing
            }
            IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND, model.Output_Kind);
            IniHelper.SetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_VERSION, CONST.KEY_VERSION_MODE, model.Version_Kind);

            model.SettingsWindow_Enabled = false;
        }

        private void Event_OpenFileDialog_MergeApp(string bindingProperty)
        {
            try
            {
                if (string.IsNullOrEmpty(bindingProperty))
                {
                    return;
                }
                OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Filter = ;
                openFileDialog.ShowDialog();
                if (string.IsNullOrEmpty(openFileDialog.FileName))
                    return;
                PropertyInfo propertyInfo = model.GetType().GetProperty(bindingProperty);
                if (propertyInfo == null)
                {
                    return;
                }
                propertyInfo.SetValue(model, openFileDialog.FileName);
            }
            catch (Exception e)
            {
                LogFile.WriteLog(e.Message);
            }
        }

        private void Process_Excute_GetClient()
        {
            try
           {
                model.CBox_ClientName = new List<string>();
                model.CBox_ClientIndex = -1;
                string Output = ProcessHelper.Excute("p4.exe", " -p "+model.ServicePort+" clients -u "+model.UserName, "");
                if (Output != string.Empty)
                {
                    string Pattern = "(?<=Client ).*?(?= )";
                    MatchCollection Matches = Regex.Matches(Output, Pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    List<string> listItem = new List<string>();
                    foreach (Match match in Matches)
                    {
                        listItem.Add(match.Value);
                    }
                    model.CBox_ClientName = listItem;
                        

                    if (model.CBox_ClientName.Count > 0)
                    {
                        int index = model.CBox_ClientName.IndexOf(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT));
                        if (index >= 0)
                        {
                            model.CBox_ClientIndex = index;
                        }
                        else
                        {
                            model.CBox_ClientIndex = 0;
                        }
                    }
                }
            }
            catch { }
        }

        private void Event_ComboBox_ClientChanged(string useless)
        {
            model.CBox_ChangeListName = new ObservableCollection<ChangeListItem>();
            model.CBox_ChangeListIndex = -1;
            processExcute = new ProcessExcute(Process_Excute_GetChangeList);
            processExcute.BeginInvoke(null, null);
        }

        private void Process_Excute_GetChangeList()
        {
            try
            {
                if ((model.CBox_ClientName.Count <= 0) || (model.CBox_ClientIndex < 0))
                {
                    return;
                }
                string Output = ProcessHelper.Excute("p4.exe", " -p " + model.ServicePort + " changelists -c " + model.CBox_ClientName[model.CBox_ClientIndex] + " -s pending","");

                ChangeListItem item = new ChangeListItem()
                {
                    ID = CONST.KEY_CHANGELIST_CREATE,
                    Name = "<新規>"
                };
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ParameterizedThreadStart(Process_ComboBox_AddItem), item);
                item = new ChangeListItem()
                {
                    ID = CONST.KEY_CHANGELIST_DEFAULT,
                    Name = "<default>"
                };
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ParameterizedThreadStart(Process_ComboBox_AddItem), item);
                if (Output != string.Empty)
                {
                    string PatternID = "(?<=Change )(.*?)(?= )";
                    MatchCollection MatchesID = Regex.Matches(Output, PatternID, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    string PatternName = "(?<= ')(.*?)(?='\r\n)";
                    MatchCollection MatchesName = Regex.Matches(Output, PatternName, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                        
                    if (MatchesID.Count == MatchesName.Count)
                    {
                        for (int i = 0; i < MatchesID.Count; i++)
                        {
                            item = new ChangeListItem()
                            {
                                ID = MatchesID[i].Value,
                                Name = "<" + MatchesID[i].Value + "> " + MatchesName[i].Value
                            };
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ParameterizedThreadStart(Process_ComboBox_AddItem), item);
                        }
                    }
                }
                if (model.CBox_ChangeListIndex == -1)
                {
                    model.CBox_ChangeListIndex = 0;
                }
            }
            catch { }
        }

        private void Process_ComboBox_AddItem(object item)
        {
            model.CBox_ChangeListName.Add((ChangeListItem)item);
            ChangeListItem itemTmp = (ChangeListItem)item;
            if (itemTmp.ID.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST)))
            {
                for (int i = 0; i < model.CBox_ChangeListName.Count; i++)
                {
                    if ((model.CBox_ClientName[model.CBox_ClientIndex] == IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT)) && 
                        (model.CBox_ChangeListName[i].ID.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CHANGELIST))) )
                    {
                        model.CBox_ChangeListIndex = i;
                        break;
                    }
                }
            }
        }

        private void Process_FillData_ClientPath()
        {
            bool processed = false;
            try
            {
                if (CONST.KEY_KIND_PERFORCE.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_KIND)))
                {
                    string Output = ProcessHelper.Excute("p4.exe", " -p " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_PORT) + " clients -u " + IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_USER), "");
                    if (Output != string.Empty)
                    {
                        string PatternName = "(?<=Client )(.*?)(?= )";
                        MatchCollection MatchesName = Regex.Matches(Output, PatternName, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                        string PatternPath = "(?<= root )(.*?)(?= ')";
                        MatchCollection MatchesPath = Regex.Matches(Output, PatternPath, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                        if (MatchesName.Count == MatchesPath.Count)
                        {
                            for (int i = 0; i < MatchesName.Count; i++)
                            {
                                if (MatchesName[i].Value.Equals(IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_OUTPUT, CONST.KEY_OUTPUT_CLIENT)))
                                {
                                    mainVM.model.ProjectPath = MatchesPath[i].Value;
                                    mainVM.model.Excel_FinalFilePath = Path.Combine(MatchesPath[i].Value, IniHelper.GetKeyValue(CONST.FILEPATH_CONFIGURE, CONST.SETCION_SAVEPATH, CONST.KEY_SAVEPATH_FINAL));
                                    processed = true;
                                }
                            }
                        }
                    }
                }

                if (processed)
                {
                    mainVM.model.Btn_ProjectEnabled = false;
                }
                else
                {
                    mainVM.model.Btn_ProjectEnabled = true;
                }
            }
            catch { }
        }
    }
}
