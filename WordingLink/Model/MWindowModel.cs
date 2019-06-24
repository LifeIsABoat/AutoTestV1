using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace WordingLink
{
    public class MWindowModel : BaseModel
    {
        //程序执行的状态类型
        public enum Process_Status
        {
            PROCESS,
            READY,
            WAIT,
            STOPED
        }

        #region  Data Link

        //程序当前的状态
        private Process_Status _process_status = Process_Status.STOPED;
        public Process_Status ProcessStatus
        {
            get { return _process_status; }
            set
            {
                _process_status = value;
                base.NotifyPropertyChanged("ProcessStatus");

                switch (_process_status)
                {
                    case Process_Status.PROCESS:
                        this.Btn_StartEnabled = false;
                        this.Btn_ResetEnabled = false;
                        break;
                    case Process_Status.READY:
                        this.Btn_StartContent = "引き続き";
                        this.Btn_StartEnabled = true;
                        this.Btn_ResetEnabled = true;
                        break;
                    case Process_Status.WAIT:
                        this.Btn_StartEnabled = false;
                        this.Btn_ResetEnabled = true;
                        break;
                    case Process_Status.STOPED:
                        this.Btn_FinalEnabled = true;
                        this.Btn_WordingEnabled = true;
                        this.Btn_MsgNoEnabled = true;
                        this.Btn_OutPutEnabled = true;
                        this.DataGridItems.Clear();
                        this.Btn_StartEnabled = true;
                        this.Btn_ResetEnabled = true;
                        this.Btn_StartContent = "スタート";
                        this.TextBlock_Message1 = string.Empty;
                        this.TextBlock_Message2 = string.Empty;
                        this.TextBlock_Message3 = string.Empty;
                        break;
                    default:
                        break;
                }
            }
        }

        //文言申請システム的文件路径
        private string _excel_wordingfilepath = string.Empty;
        public string Excel_WordingFilePath
        {
            get { return _excel_wordingfilepath; }
            set
            {
                _excel_wordingfilepath = value;
                base.NotifyPropertyChanged("Excel_WordingFilePath");
                if (File.Exists(_excel_wordingfilepath))
                {
                    this.Text_WordingColor = CONST.ITEM_COLOR_WHITE;
                }
                else
                {
                    this.Text_WordingColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        //最終文言エクセル的文件路径
        private string _excel_finalfilepath = string.Empty;
        public string Excel_FinalFilePath
        {
            get { return _excel_finalfilepath; }
            set
            {
                _excel_finalfilepath = value;
                base.NotifyPropertyChanged("Excel_FinalFilePath");
                if (File.Exists(_excel_finalfilepath))
                {
                    this.Text_FinalColor = CONST.ITEM_COLOR_WHITE;
                }
                else
                {
                    this.Text_FinalColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        //MsgNo使用状況的文件路径
        private string _excel_msgnofilepath = string.Empty;
        public string Excel_MsgNoFilePath
        {
            get { return _excel_msgnofilepath; }
            set
            {
                _excel_msgnofilepath = value;
                base.NotifyPropertyChanged("Excel_MsgNoFilePath");
                if (File.Exists(_excel_msgnofilepath))
                {
                    this.Text_MsgNoColor = CONST.ITEM_COLOR_WHITE;
                }
                else
                {
                    this.Text_MsgNoColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        //输出文件的路径
        private string _outputpath = string.Empty;
        public string OutPutPath
        {
            get { return _outputpath; }
            set
            {
                _outputpath = value;
                base.NotifyPropertyChanged("OutPutPath");
                try
                {
                    if (!Directory.Exists(_outputpath))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(_outputpath);
                        directoryInfo.Create();
                        directoryInfo.Delete();
                    }
                    this.Text_OutPutColor = CONST.ITEM_COLOR_WHITE;
                }
                catch
                {
                    this.Text_OutPutColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        //输出文件的路径
        private string _projectpath = string.Empty;
        public string ProjectPath
        {
            get { return _projectpath; }
            set
            {
                _projectpath = value;
                base.NotifyPropertyChanged("ProjectPath");
                try
                {
                    if (Directory.Exists(_projectpath))
                    {
                        this.Text_ProjectColor = CONST.ITEM_COLOR_WHITE;
                    }
                    else
                    {
                        this.Text_ProjectColor = CONST.ITEM_COLOR_PINK;
                    }
                }
                catch
                {
                    this.Text_ProjectColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        //启动按钮的文字显示
        private string _btn_startcontent = "スタート";
        public string Btn_StartContent
        {
            get { return _btn_startcontent; }
            set
            {
                _btn_startcontent = value;
                base.NotifyPropertyChanged("Btn_StartContent");
            }
        }

        //文言申請システム输入框的颜色
        private string _text_wordingcolor = CONST.ITEM_COLOR_WHITE;
        public string Text_WordingColor
        {
            get { return _text_wordingcolor; }
            set
            {
                _text_wordingcolor = value;
                base.NotifyPropertyChanged("Text_WordingColor");
            }
        }

        //最終文言エクセル输入框的颜色
        private string _text_finalcolor = CONST.ITEM_COLOR_WHITE;
        public string Text_FinalColor
        {
            get { return _text_finalcolor; }
            set
            {
                _text_finalcolor = value;
                base.NotifyPropertyChanged("Text_FinalColor");
            }
        }

        //MsgNo使用状況输入框的颜色
        private string _text_msgnocolor = CONST.ITEM_COLOR_WHITE;
        public string Text_MsgNoColor
        {
            get { return _text_msgnocolor; }
            set
            {
                _text_msgnocolor = value;
                base.NotifyPropertyChanged("Text_MsgNoColor");
            }
        }

        //输出文件路径输入框的颜色
        private string _text_outpucolor = CONST.ITEM_COLOR_WHITE;
        public string Text_OutPutColor
        {
            get { return _text_outpucolor; }
            set
            {
                _text_outpucolor = value;
                base.NotifyPropertyChanged("Text_OutPutColor");
            }
        }

        //工程路径输入框的颜色
        private string _text_projectcolor = CONST.ITEM_COLOR_WHITE;
        public string Text_ProjectColor
        {
            get { return _text_projectcolor; }
            set
            {
                _text_projectcolor = value;
                base.NotifyPropertyChanged("Text_ProjectColor");
            }
        }

        //Log输出框的文字
        private string _textblock_message1 = string.Empty;
        public string TextBlock_Message1
        {
            get { return _textblock_message1; }
            set
            {
                _textblock_message1 = value;
                base.NotifyPropertyChanged("TextBlock_Message1");
            }
        }

        //Log输出框的文字
        private string _textblock_message2 = string.Empty;
        public string TextBlock_Message2
        {
            get { return _textblock_message2; }
            set
            {
                _textblock_message2 = value;
                base.NotifyPropertyChanged("TextBlock_Message2");
            }
        }

        //Log输出框的文字
        private string _textblock_message3 = string.Empty;
        public string TextBlock_Message3
        {
            get { return _textblock_message3; }
            set
            {
                _textblock_message3 = value;
                //LogFile.WriteLog(value);
                base.NotifyPropertyChanged("TextBlock_Message3");
            }
        }

        //Log输出框的文字颜色
        private string _textblock_color = CONST.ITEM_COLOR_BLACK;
        public string TextBlock_Color
        {
            get { return _textblock_color; }
            set
            {
                _textblock_color = value;
                base.NotifyPropertyChanged("TextBlock_Color");
            }
        }

        //执行中动画的显示
        private string _load_visibility = CONST.ITEM_COLLAPSED;
        public string Load_Visibility
        {
            get { return _load_visibility; }
            set
            {
                _load_visibility = value;
                base.NotifyPropertyChanged("Load_Visibility");
            }
        }

        //DataGrid数据的保存
        private DataGridItem _selecteditem = null;
        public DataGridItem SelectedItem
        {
            get { return _selecteditem; }
            set
            {
                _selecteditem = value;
                base.NotifyPropertyChanged("SelectedItem");
            }
        }

        private DataGridItem _currentitem = null;
        public DataGridItem CurrentItem
        {
            get { return _currentitem; }
            set
            {
                _currentitem = value;
                base.NotifyPropertyChanged("CurrentItem");
            }
        }

        ObservableCollection<DataGridItem> datagriditems = null;
        public ObservableCollection<DataGridItem> DataGridItems
        {
            get { return datagriditems; }
            set
            {
                datagriditems = value;
                base.NotifyPropertyChanged("DataGridItems");
            }
        }

        #endregion

        #region  Items Link
        private bool _btn_startenabled = true;
        public bool Btn_StartEnabled
        {
            get { return _btn_startenabled; }
            set
            {
                _btn_startenabled = value;
                base.NotifyPropertyChanged("Btn_StartEnabled");
            }
        }

        private bool _btn_resetenabled = true;
        public bool Btn_ResetEnabled
        {
            get { return _btn_resetenabled; }
            set
            {
                _btn_resetenabled = value;
                base.NotifyPropertyChanged("Btn_ResetEnabled");
            }
        }

        private bool _btn_wordingenabled = true;
        public bool Btn_WordingEnabled
        {
            get { return _btn_wordingenabled; }
            set
            {
                _btn_wordingenabled = value;
                base.NotifyPropertyChanged("Btn_WordingEnabled");
            }
        }

        private bool _btn_finalenabled = true;
        public bool Btn_FinalEnabled
        {
            get { return _btn_finalenabled; }
            set
            {
                _btn_finalenabled = value;
                base.NotifyPropertyChanged("Btn_FinalEnabled");
            }
        }

        private bool _btn_msgnoenabled = true;
        public bool Btn_MsgNoEnabled
        {
            get { return _btn_msgnoenabled; }
            set
            {
                _btn_msgnoenabled = value;
                base.NotifyPropertyChanged("Btn_MsgNoEnabled");
            }
        }

        private bool _btn_outputenabled = true;
        public bool Btn_OutPutEnabled
        {
            get { return _btn_outputenabled; }
            set
            {
                _btn_outputenabled = value;
                base.NotifyPropertyChanged("Btn_OutPutEnabled");
            }
        }

        private bool _btn_projectenabled = true;
        public bool Btn_ProjectEnabled
        {
            get { return _btn_projectenabled; }
            set
            {
                _btn_projectenabled = value;
                base.NotifyPropertyChanged("Btn_ProjectEnabled");
            }
        }

        #endregion

        #region  Event Link
        private ICommand _btn_close = null;
        public ICommand Btn_Close
        {
            get { return _btn_close; }
            set
            {
                _btn_close = value;
                base.NotifyPropertyChanged("Btn_Close");
            }
        }

        private ICommand _openFileDialog_Excel = null;
        public ICommand OpenFileDialog_Excel
        {
            get { return _openFileDialog_Excel; }
            set { _openFileDialog_Excel = value; }
        }

        private ICommand _openFolder_OutPutPath = null;
        public ICommand OpenFolder_OutPutPath
        {
            get { return _openFolder_OutPutPath; }
            set { _openFolder_OutPutPath = value; }
        }

        private ICommand _openfolder_projectpath = null;
        public ICommand OpenFolder_ProjectPath
        {
            get { return _openfolder_projectpath; }
            set { _openfolder_projectpath = value; }
        }

        private ICommand _btn_startprocess = null;
        public ICommand Btn_StartProcess
        {
            get { return _btn_startprocess; }
            set { _btn_startprocess = value; }
        }

        private ICommand _btn_resetprocess = null;
        public ICommand Btn_ResetProcess
        {
            get { return _btn_resetprocess; }
            set { _btn_resetprocess = value; }
        }

        private ICommand _settingswindow_start = null;
        public ICommand SettingsWindow_Start
        {
            get { return _settingswindow_start; }
            set { _settingswindow_start = value; }
        }

        private ICommand _tbox_projectpathchanged = null;
        public ICommand TBox_ProjectPathChanged
        {
            get { return _tbox_projectpathchanged; }
            set
            {
                _tbox_projectpathchanged = value;
                base.NotifyPropertyChanged("TBox_ProjectPathChanged");
            }
        }

        private ICommand _mw_activechanged = null;
        public ICommand MW_ActiveChanged
        {
            get { return _mw_activechanged; }
            set
            {
                _mw_activechanged = value;
                base.NotifyPropertyChanged("MW_ActiveChanged");
            }
        }
        #endregion
    }
}
