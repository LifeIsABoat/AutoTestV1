using System.Windows.Input;
using System.Collections.Generic;

namespace ColorDivisionTool.ViewModel
{
    public class BindItem : NotifyChangedBind
    {
        private ICommand _openFileDialog_Excel = null;
        //load file button command
        public ICommand OpenFileDialog_Excel
        {
            get
            {
                return _openFileDialog_Excel;
            }
            set
            {
                _openFileDialog_Excel = value;
            }
        }

        //button start command
        private ICommand _btn_StartChgColor = null;
        public ICommand Btn_StartChgColor
        {
            get
            {
                return _btn_StartChgColor;
            }
            set
            {
                _btn_StartChgColor = value;
            }
        }

        // ModelInfoKey
        private List<string> _modelInfoKey = new List<string>();
        public List<string> ModelInfoKey
        {
            get { return _modelInfoKey; }
            set
            {
                _modelInfoKey = value;
                NotifyPropertyChanged("ModelInfoKey");
            }
        }

        // ModelInfoType
        private List<string> _modelInfoValue = new List<string>();
        public List<string> ModelInfoValue
        {
            get { return _modelInfoValue; }
            set
            {
                _modelInfoValue = value;
                NotifyPropertyChanged("ModelInfoValue");
            }
        }

        //log
        private string _mWindowShowLog = string.Empty;
        public string MWindowShowLog
        {
            get { return _mWindowShowLog; }
            set
            {
                _mWindowShowLog = value;
                NotifyPropertyChanged("MWindowShowLog");
            }
        }

        //GifCat
        private string _show_GifStatus1 = string.Empty;
        public string Show_GifStatus1
        {
            get { return _show_GifStatus1; }
            set
            {
                _show_GifStatus1 = value;
                NotifyPropertyChanged("Show_GifStatus1");
            }
        }
        
        //GifLoading
        private string _show_GifStatus2 = string.Empty;
        public string Show_GifStatus2
        {
            get { return _show_GifStatus2; }
            set
            {
                _show_GifStatus2 = value;
                NotifyPropertyChanged("Show_GifStatus2");
            }
        }

        //Evidence File
        private string _excel_EvidenceFile = string.Empty;
        public string Excel_EvidenceFile
        {
            get { return _excel_EvidenceFile; }
            set
            {
                _excel_EvidenceFile = value;
                NotifyPropertyChanged("Excel_EvidenceFile");
            }
        }

        //start Button
        private bool _btn_ColorStartCtl = true;
        public bool Btn_ColorStartCtl
        {
            get { return _btn_ColorStartCtl; }
            set
            {
                _btn_ColorStartCtl = value;
                NotifyPropertyChanged("Btn_ColorStartCtl");
            }
        }

        private string _selectModelInfo = string.Empty;
        public string SelectModelInfo
        {
            get
            {
                return _selectModelInfo;
            }
            set
            {
                _selectModelInfo = value;
                NotifyPropertyChanged("SelectModelInfo");
            }
        }

        private string _selectModelType = string.Empty;
        public string SelectModelType
        {
            get
            {
                return _selectModelType;
            }
            set
            {
                _selectModelType = value;
                NotifyPropertyChanged("SelectModelType");
            }
        }
    }


}
