using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordingLink
{
    public class DataGridItem : BaseModel
    {
        bool _cbox_checked = false;
        public bool CBox_Checked
        {
            get { return _cbox_checked; }
            set { _cbox_checked = value; NotifyPropertyChanged("CBox_Checked"); }
        }

        bool _lineenabled = false;
        public bool LineEnabled
        {
            get { return _lineenabled; }
            set { _lineenabled = value; NotifyPropertyChanged("LineEnabled"); }
        }

        string _modelname = string.Empty;
        public string ModelName
        {
            get { return _modelname; }
            set { _modelname = value; NotifyPropertyChanged("ModelName"); }
        }

        string _modeltype = string.Empty;
        public string ModelType
        {
            get { return _modeltype; }
            set { _modeltype = value; NotifyPropertyChanged("ModelType"); }
        }

        string _linkfilepath = string.Empty;
        public string LinkFilePath
        {
            get { return _linkfilepath; }
            set
            {
                _linkfilepath = value;
                NotifyPropertyChanged("LinkFilePath");
                if (File.Exists(Path.Combine(_linkfilepath,CONST.FILENAME_STRIDLINK)) &&
                    File.Exists(Path.Combine(_linkfilepath,CONST.FILENAME_HINAGATA)))
                {
                    this.Text_LinkFileColor = CONST.ITEM_COLOR_PARENT;
                }
                else
                {
                    this.Text_LinkFileColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        string _hmifilepath = string.Empty;
        public string HmiFilePath
        {
            get { return _hmifilepath; }
            set
            {
                _hmifilepath = value;
                NotifyPropertyChanged("HmiFilePath");
                if (File.Exists(Path.Combine(_hmifilepath,CONST.FILENAME_HMISTRID)))
                {
                    this.Text_HmiFileColor = CONST.ITEM_COLOR_PARENT;
                }
                else
                {
                    this.Text_HmiFileColor = CONST.ITEM_COLOR_PINK;
                }
            }
        }

        private string _text_linkfilecolor = CONST.ITEM_COLOR_PARENT;
        public string Text_LinkFileColor
        {
            get { return _text_linkfilecolor; }
            set
            {
                _text_linkfilecolor = value;
                base.NotifyPropertyChanged("Text_LinkFileColor");
            }
        }

        private string _text_hmifilecolor = CONST.ITEM_COLOR_PARENT;
        public string Text_HmiFileColor
        {
            get { return _text_hmifilecolor; }
            set
            {
                _text_hmifilecolor = value;
                base.NotifyPropertyChanged("Text_HmiFileColor");
            }
        }

        string _procedure = string.Empty;
        public string Procedure
        {
            get { return _procedure; }
            set { _procedure = value; NotifyPropertyChanged("Procedure"); }
        }
    }
}
