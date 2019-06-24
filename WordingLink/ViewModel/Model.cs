using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WordingLink
{
    public class Model : BaseModel
    {
        #region  Data Link

        private DataTable _wordingdata = null;
        public DataTable WordingData
        {
            get { return _wordingdata; }
            set
            {
                _wordingdata = value;
                base.NotifyPropertyChanged("WordingData");
            }
        }

        private DataTable _finaldata = null;
        public DataTable finalData
        {
            get { return _finaldata; }
            set
            {
                _finaldata = value;
                base.NotifyPropertyChanged("finalData");
            }
        }

        private DataTable _msgnodata = null;
        public DataTable MsgNoData
        {
            get { return _msgnodata; }
            set
            {
                _msgnodata = value;
                base.NotifyPropertyChanged("MsgNoData");
            }
        }

        private string _excel_wordingfilepath = string.Empty;
        public string Excel_WordingFilePath
        {
            get { return _excel_wordingfilepath; }
            set
            {
                _excel_wordingfilepath = value;
                base.NotifyPropertyChanged("Excel_WordingFilePath");
                if (Check_WordingInput())
                {
                    this.Text_WordingColor = CONST.ITEM_COLOR_WHITE;
                }else
                {
                    this.Text_WordingColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        private string _excel_finalfilepath = string.Empty;
        public string Excel_FinalFilePath
        {
            get { return _excel_finalfilepath; }
            set
            {
                _excel_finalfilepath = value;
                base.NotifyPropertyChanged("Excel_FinalFilePath");
                if (Check_FinalInput())
                {
                    this.Text_FinalColor = CONST.ITEM_COLOR_WHITE;
                }
                else
                {
                    this.Text_FinalColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        private string _excel_msgnofilepath = string.Empty;
        public string Excel_MsgNoFilePath
        {
            get { return _excel_msgnofilepath; }
            set
            {
                _excel_msgnofilepath = value;
                base.NotifyPropertyChanged("Excel_MsgNoFilePath");
                if (Check_MsgNoInput())
                {
                    this.Text_MsgNoColor = CONST.ITEM_COLOR_WHITE;
                }
                else
                {
                    this.Text_MsgNoColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        private string _outputpath = string.Empty;
        public string OutPutPath
        {
            get { return _outputpath; }
            set
            {
                _outputpath = value;
                base.NotifyPropertyChanged("OutPutPath");
                if (Check_OutPutPath())
                {
                    this.Text_OutPutColor = CONST.ITEM_COLOR_WHITE;
                }
                else
                {
                    this.Text_OutPutColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        private string _stepPrompt = string.Empty;
        public string StepPrompt
        {
            get { return _stepPrompt; }
            set
            {
                _stepPrompt = value;
                base.NotifyPropertyChanged("StepPrompt");
            }
        }

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

        private string _load_visibility = CONST.ITEM_COLLASPSED;
        public string Load_Visibility
        {
            get { return _load_visibility; }
            set
            {
                _load_visibility = value;
                base.NotifyPropertyChanged("Load_Visibility");
            }
        }

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

        #endregion

        #region  Items Link

        private bool _btn_wording_enabled = true;
        public bool Btn_WordingEnabled
        {
            get { return _btn_wording_enabled; }
            set
            {
                _btn_wording_enabled = value;
                base.NotifyPropertyChanged("Btn_WordingEnabled");
            }
        }

        private bool _btn_final_enabled = true;
        public bool Btn_FinalEnabled
        {
            get { return _btn_final_enabled; }
            set
            {
                _btn_final_enabled = value;
                base.NotifyPropertyChanged("Btn_FinalEnabled");
            }
        }

        private bool _btn_msgno_enabled = true;
        public bool Btn_MsgNoEnabled
        {
            get { return _btn_msgno_enabled; }
            set
            {
                _btn_msgno_enabled = value;
                base.NotifyPropertyChanged("Btn_MsgNoEnabled");
            }
        }
        
        private bool _btn_output_enabled = true;
        public bool Btn_OutPutEnabled
        {
            get { return _btn_output_enabled; }
            set
            {
                _btn_output_enabled = value;
                base.NotifyPropertyChanged("Btn_OutPutEnabled");
            }
        }

        private bool _checkbox_checked = true;
        public bool CheckBox_Checked
        {
            get { return _checkbox_checked; }
            set
            {
                _checkbox_checked = value;
                base.NotifyPropertyChanged("CheckBox_IsChecked");
            }
        }

        #endregion

        #region  Event Link
        private ICommand _button_Click = null;
        public ICommand Button_Click
        {
            get { return _button_Click; }
            set 
            {
                _button_Click = value;
                base.NotifyPropertyChanged("HelloWorld");
            }
        }

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

        private ICommand _btn_minisize = null;
        public ICommand Btn_MiniSize
        {
            get { return _btn_minisize; }
            set
            {
                _btn_minisize = value;
                base.NotifyPropertyChanged("Btn_Close");
            }
        }

        private ICommand _openFileDialog_Excel = null;
        public ICommand OpenFileDialog_Excel
        {
            get { return _openFileDialog_Excel; }
            set { _openFileDialog_Excel = value; }
        }

        private ICommand _openFileDialog_datagrid = null;
        public ICommand OpenFileDialog_DataGrid
        {
            get { return _openFileDialog_datagrid; }
            set { _openFileDialog_datagrid = value; }
        }

        ObservableCollection<DataGridItem> datagriditems = null;
        public ObservableCollection<DataGridItem> DataGridItems
        {
            get { return datagriditems; }
            set
            {
                datagriditems = value;
                base.NotifyPropertyChanged("ModelTypeList");
            }
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

        private ICommand _checkbox_selectall = null;
        public ICommand Checkbox_SelectAll
        {
            get { return _checkbox_selectall; }
            set { _checkbox_selectall = value; }
        }

        #endregion

        #region  Local Event
        private bool Check_WordingInput()
        {
            bool checkOK = false;
            try
            {
                ExcelHelper excel = new ExcelHelper(Excel_WordingFilePath);
                if (excel.OpenExcel())
                {
                    if (excel.ReadExcel(CONST.EXCEL_WORDING_SHEET, CONST.EXCEL_WORDING_STARTLINE, ref _wordingdata))
                    {
                        if (_wordingdata != null)
                        {
                            checkOK = true;
                        }
                    }
                }
            }
            catch
            {

            }

            return checkOK;
        }

        private bool Check_FinalInput()
        {
            bool checkOK = false;
            try
            {
                ExcelHelper excel = new ExcelHelper(_excel_finalfilepath);
                if (excel.OpenExcel())
                {
                    if (excel.ReadExcel(CONST.EXCEL_FINAL_SHEET, CONST.EXCEL_FINAL_STARTLINE, ref _finaldata))
                    {
                        if (_finaldata != null)
                        {
                            checkOK = true;
                        }
                    }
                }
            }
            catch
            {

            }

            return checkOK;
        }

        private bool Check_MsgNoInput()
        {
            bool checkOK = false;
            try
            {
                ExcelHelper excel = new ExcelHelper(_excel_msgnofilepath);
                if (excel.OpenExcel())
                {
                    if (excel.ReadExcel(CONST.EXCEL_MSGNO_SHEET, CONST.EXCEL_MSGNO_STARTLINE, ref _msgnodata))
                    {
                        if (_msgnodata != null)
                        {
                            checkOK = true;
                        }
                    }
                }
            }
            catch
            {

            }

            return checkOK;
        }

        private bool Check_OutPutPath()
        {
            bool checkOK = false;
            try
            {
                if (!Directory.Exists(_outputpath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(_outputpath);
                    directoryInfo.Create();
                    directoryInfo.Delete();
                }
                checkOK = true;
            }
            catch
            {

            }

            return checkOK;
        }
        #endregion
    }

    public class ProgressBarDataModel
    {
        public double EclipseSize { get; set; }
        public double CanvasSize { get; set; }
        public double ViewBoxSize
        {
            get
            {
                double length = Convert.ToDouble(CanvasSize) - Convert.ToDouble(EclipseSize);
                return length;
            }
        }
        public double EclipseLeftLength
        {
            get
            {
                double length = Convert.ToDouble(CanvasSize) / 2;
                return length;
            }
        }
        public double R
        {
            get
            {
                double length = (Convert.ToDouble(CanvasSize) - Convert.ToDouble(EclipseSize)) / 2;
                return length;
            }
        }
    }
}
