using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordingLink
{
    public class SettingsModel : BaseModel
    {
        //Merge工具的路径
        private string _mergeapppath = string.Empty;
        public string MergeAppPath
        {
            get { return _mergeapppath; }
            set
            {
                _mergeapppath = value;
                base.NotifyPropertyChanged("MergeAppPath");
            }
        }

        //Perforce用户名
        private string _usernameh = string.Empty;
        public string UserName
        {
            get { return _usernameh; }
            set
            {
                _usernameh = value;
                base.NotifyPropertyChanged("UserName");
            }
        }

        //Perforce服务器地址
        private string _serviceport = string.Empty;
        public string ServicePort
        {
            get { return _serviceport; }
            set
            {
                _serviceport = value;
                base.NotifyPropertyChanged("ServicePort");
            }
        }

        private bool _gbox_mergeenabled = false;
        public bool GBox_MergeEnabled
        {
            get { return _gbox_mergeenabled; }
            set
            {
                _gbox_mergeenabled = value;
                base.NotifyPropertyChanged("GBox_MergeEnabled");
            }
        }

        private bool _settingswindow_enabled = true;
        public bool SettingsWindow_Enabled
        {
            get { return _settingswindow_enabled; }
            set
            {
                _settingswindow_enabled = value;
                base.NotifyPropertyChanged("SettingsWindow_Enabled");
            }
        }

        private bool _gbox_perforceenabled = false;
        public bool GBox_PerforceEnabled
        {
            get { return _gbox_perforceenabled; }
            set
            {
                _gbox_perforceenabled = value;
                base.NotifyPropertyChanged("GBox_PerforceEnabled");
            }
        }

        private bool _output_selected = false;
        public bool Output_Selected
        {
            get { return _output_selected; }
            set
            {
                _output_selected = value;
                base.NotifyPropertyChanged("Output_Selected");
                if (value == true)
                {
                    Output_Visibility = CONST.ITEM_VISIBLE;
                }
                else
                {
                    Output_Visibility = CONST.ITEM_COLLAPSED;
                }
            }
        }

        private bool _version_selected = false;
        public bool Version_Selected
        {
            get { return _version_selected; }
            set
            {
                _version_selected = value;
                base.NotifyPropertyChanged("Version_Selected");
                if (value == true)
                {
                    Version_Visibility = CONST.ITEM_VISIBLE;
                }
                else
                {
                    Version_Visibility = CONST.ITEM_COLLAPSED;
                }
            }
        }

        private string _output_visibility = CONST.ITEM_COLLAPSED;
        public string Output_Visibility
        {
            get { return _output_visibility; }
            set
            {
                _output_visibility = value;
                base.NotifyPropertyChanged("Output_Visibility");
            }
        }

        private string _outputitem_visibility = CONST.ITEM_COLLAPSED;
        public string OutputItem_Visibility
        {
            get { return _outputitem_visibility; }
            set
            {
                _outputitem_visibility = value;
                base.NotifyPropertyChanged("OutputItem_Visibility");
            }
        }

        private string _version_visibility = CONST.ITEM_COLLAPSED;
        public string Version_Visibility
        {
            get { return _version_visibility; }
            set
            {
                _version_visibility = value;
                base.NotifyPropertyChanged("Version_Visibility");
            }
        }

        private string _output_kind = "";
        public string Output_Kind
        {
            get { return _output_kind; }
            set
            {
                _output_kind = value;
                base.NotifyPropertyChanged("Output_Kind");
            }
        }

        private string _version_kind = "";
        public string Version_Kind
        {
            get { return _version_kind; }
            set
            {
                _version_kind = value;
                base.NotifyPropertyChanged("Version_Kind");
            }
        }

        private bool _version_topspeedchecked = false;
        public bool Version_TopSpeedChecked
        {
            get
            {
                return (this.Version_Kind.Equals(CONST.KEY_MODE_SPEED));
            }
            set
            {
                _version_topspeedchecked = value;
                if (_version_topspeedchecked)
                {
                    this.Version_Kind = CONST.KEY_MODE_SPEED;
                }
                base.NotifyPropertyChanged("Version_TopSpeedChecked");
            }
        }

        private bool _version_stablechecked = false;
        public bool Version_StableChecked
        {
            get
            {
                return (this.Version_Kind.Equals(CONST.KEY_MODE_STABLE));
            }
            set
            {
                _version_stablechecked = value;
                if (_version_stablechecked)
                {
                    this.Version_Kind = CONST.KEY_MODE_STABLE;
                }
                base.NotifyPropertyChanged("Version_StableChecked");
            }
        }

        private bool _output_defaultchecked = false;
        public bool Output_DefaultChecked
        {
            get
            {
                return (this.Output_Kind.Equals(CONST.KEY_KIND_DEFAULT));
            }
            set
            {
                _output_defaultchecked = value;
                if (_output_defaultchecked)
                {
                    this.Output_Kind = CONST.KEY_KIND_DEFAULT;
                }
                base.NotifyPropertyChanged("Output_DefaultChecked");
            }
        }

        private bool _output_mergechecked = false;
        public bool Output_MergeChecked
        {
            get
            {
                return (this.Output_Kind.Equals(CONST.KEY_KIND_MERGE));
            }
            set
            {
                _output_mergechecked = value;
                if (_output_mergechecked)
                {
                    this.Output_Kind = CONST.KEY_KIND_MERGE;
                    this.GBox_MergeEnabled = true;
                }
                else
                {
                    this.GBox_MergeEnabled = false;
                }
                base.NotifyPropertyChanged("Output_MergeChecked");
            }
        }

        private bool _output_perforcechecked = false;
        public bool Output_PerforceChecked
        {
            get
            {
                return (this.Output_Kind.Equals(CONST.KEY_KIND_PERFORCE));
            }
            set
            {
                _output_perforcechecked = value;
                if (_output_perforcechecked)
                {
                    this.Output_Kind = CONST.KEY_KIND_PERFORCE;
                    this.GBox_PerforceEnabled = true;
                }
                else
                {
                    this.GBox_PerforceEnabled = false;
                }
                base.NotifyPropertyChanged("Output_PerforceChecked");
            }
        }

        private bool _checkbox_updatelatest = false;
        public bool CheckBox_UpdateLatest
        {
            get { return _checkbox_updatelatest; }
            set
            {
                _checkbox_updatelatest = value;
                base.NotifyPropertyChanged("CheckBox_UpdateLatest");
            }
        }

        private int _cbox_clientindex = 0;
        public int CBox_ClientIndex
        {
            get { return _cbox_clientindex; }
            set
            {
                _cbox_clientindex = value;
                base.NotifyPropertyChanged("CBox_ClientIndex");
            }
        }

        private List<string> _cbox_clientname = new List<string>();
        public List<string> CBox_ClientName
        {
            get { return _cbox_clientname; }
            set
            {
                _cbox_clientname = value;
                base.NotifyPropertyChanged("CBox_ClientName");
            }
        }

        private int _cbox_changelistindex = 0;
        public int CBox_ChangeListIndex
        {
            get { return _cbox_changelistindex; }
            set
           {
                _cbox_changelistindex = value;
                base.NotifyPropertyChanged("CBox_ChangeListIndex");
            }
        }

        private ObservableCollection<ChangeListItem> _cbox_changelistname = new ObservableCollection<ChangeListItem>();
        public ObservableCollection<ChangeListItem> CBox_ChangeListName
        {
            get { return _cbox_changelistname; }
            set
            {
                _cbox_changelistname = value;
                base.NotifyPropertyChanged("CBox_ChangeListName");
            }
        }

        private ICommand _openFileDialog_mergeapp = null;
        public ICommand OpenFileDialog_MergeApp
        {
            get { return _openFileDialog_mergeapp; }
            set { _openFileDialog_mergeapp = value; }
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

        private ICommand _btn_save = null;
        public ICommand Btn_Save
        {
            get { return _btn_save; }
            set
            {
                _btn_save = value;
                base.NotifyPropertyChanged("Btn_Save");
            }
        }

        private ICommand _tbox_userportchanged = null;
        public ICommand TBox_UserPortChanged
        {
            get { return _tbox_userportchanged; }
            set
            {
                _tbox_userportchanged = value;
                base.NotifyPropertyChanged("TBox_UserPortChanged");
            }
        }

        private ICommand _cbox_clientchanged = null;
        public ICommand CBox_ClientChanged
        {
            get { return _cbox_clientchanged; }
            set
            {
                _cbox_clientchanged = value;
                base.NotifyPropertyChanged("CBox_ClientChanged");
            }
        }

        private ICommand _settingswindow_closed = null;
        public ICommand SettingsWindow_Closed
        {
            get { return _settingswindow_closed; }
            set
            {
                _settingswindow_closed = value;
                base.NotifyPropertyChanged("SettingsWindow_Closed");
            }
        }
    }

    public class ChangeListItem
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
