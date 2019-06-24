using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using ColorDivisionTool.ViewModel;
using ColorDivisionTool.Model;
using ColorDivisionTool.Command;
using ColorDivisionTool.Operation;

namespace ColorDivisionTool
{
    using XmlData = Dictionary<string, Dictionary<string, string>>;
    public class MainViewModel
    {
        // Class object
        public BindItem ViewBindModel { get; private set; }

        public MainHandle mainHandle = new MainHandle();
        XmlHandle xmlHandle = new XmlHandle();

        List<string> lists = new List<string>();
        public MainViewModel(BindItem e, List<string> ArgumentsLists)
        {
            ViewBindModel = e;

            #region Initialization
            //默认隐藏Gif动态图状态
            ViewBindModel.Show_GifStatus1 = "Hidden";
            ViewBindModel.Show_GifStatus2 = "Hidden";
            #endregion
            lists.AddRange(ArgumentsLists);
            if (ArgumentsLists.Count > 2)
            {
                XmlData jpnData = xmlHandle.LoadModelInfoFile();
                if (jpnData == null)
                    MessageBox.Show("Get ModelInfo.xml Failed.");
                foreach (string key in jpnData.Keys)
                {
                    ViewBindModel.ModelInfoKey.Add(key);
                    foreach (string str in ArgumentsLists)
                    {
                        if (String.Equals(str, key, StringComparison.CurrentCultureIgnoreCase))
                        {
                            ViewBindModel.SelectModelInfo = key;
                            foreach (string s_key in jpnData[ViewBindModel.SelectModelInfo].Keys)
                                foreach (string one_str in ArgumentsLists)
                                    if (String.Equals(one_str, s_key, StringComparison.CurrentCultureIgnoreCase))
                                        ViewBindModel.SelectModelType = s_key;
                        }
                        else if (str.IndexOf(".xlsx") > 0 || str.IndexOf(".xlsm") > 0)
                        {
                            ViewBindModel.Excel_EvidenceFile = str;
                        }
                    }
                }
            }
            else
            {
                //获取测试机种集合
                XmlData jpnData = xmlHandle.LoadModelInfoFile();
                if (jpnData == null)
                    MessageBox.Show("Get ModelInfo.xml Failed.");
                foreach (string key in jpnData.Keys)
                {
                    ViewBindModel.ModelInfoKey.Add(key);
                }
            }
            ViewBindModel.Btn_StartChgColor = new DelegateCommand<string>(Btn_StartChgColorViewModel);
            ViewBindModel.OpenFileDialog_Excel = new DelegateCommand<string>(OpenFileDialog);
        }

        #region Start Change Color Function

        /// <summary>
        /// 点击button运行
        /// </summary>
        /// <param name="useBtnStart"></param>
        public void Btn_StartChgColorViewModel(string useBtnStart)
        {
            if (string.IsNullOrEmpty(ViewBindModel.Excel_EvidenceFile))
            {
                if (!File.Exists(ViewBindModel.Excel_EvidenceFile))
                {
                    MessageBox.Show("Excel File Path is NULL, Please Check!");
                    ViewBindModel.MWindowShowLog = "Excel File cannot be found";
                    return;
                }
            }

            //获取日本语类型
            string jpnValue = xmlHandle.GetJpnValueFromXml(ViewBindModel.SelectModelInfo, ViewBindModel.SelectModelType);
            if (string.IsNullOrEmpty(jpnValue))
            {
                ViewBindModel.MWindowShowLog = "Get JPN Country's type failed.";
                return;
            }
            Console.WriteLine("Open Configure File : " + ConfigConst.CONFIGUREFILEPATH);
            IniOperation.WritePrivateProfileString(ConfigConst.CHANGECOLORCOUNTRY, ConfigConst.KEY_SJPN, jpnValue, ConfigConst.CONFIGUREFILEPATH);
            IniOperation.WritePrivateProfileString(ConfigConst.JPANCOUNTRY, ConfigConst.KEY_JJPN, jpnValue, ConfigConst.CONFIGUREFILEPATH);

            //start change color
            mainHandle.StartChangeFileColor(ViewBindModel, lists);
        }

        #endregion

        #region OpenFileDialog event
        private void OpenFileDialog(string bindingProperty)
        {
            try
            {
                if (string.IsNullOrEmpty(bindingProperty))
                {// warning
                    ViewBindModel.MWindowShowLog = "Evidence File : OpenFileDialog Error #001";
                    return;
                }
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel|*.xls;*.xlsx;*.xlsm|ALL|*.*";
                openFileDialog.ShowDialog();
                if (string.IsNullOrEmpty(openFileDialog.FileName))
                    return;
                PropertyInfo propertyInfo = ViewBindModel.GetType().GetProperty(bindingProperty);
                if (propertyInfo == null)
                {// warning
                    ViewBindModel.MWindowShowLog = "Evidence File : OpenFileDialog Error #002";
                    return;
                }
                propertyInfo.SetValue(ViewBindModel, openFileDialog.FileName);
            }
            catch (Exception e)
            {// warning
                ViewBindModel.MWindowShowLog = e.Message;
                return;
            }
        }
        #endregion
    }
}
