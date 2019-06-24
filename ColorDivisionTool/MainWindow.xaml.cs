using System.IO;
using System.Windows;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ColorDivisionTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using XmlData = Dictionary<string, Dictionary<string, string>>;
    public partial class MainWindow : Window
    {
        private XmlData jpnData = new XmlData();

        public MainWindow()
        {
            InitializeComponent();

            jpnData = LoadModelInfoFile();
        }

        /// <summary>
        /// get the type of machine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (string key in jpnData.Keys)
            {
                if (cmb_Model.SelectedItem.ToString() == key)
                {
                    cmb_type.ItemsSource = jpnData[key].Keys;
                }
            }
        }

        //加载xml文件，读取Xml文件内容
        private static XmlData LoadModelInfoFile()
        {
            try
            {
                string RUNCURRENTPATH = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                string ModelInfoPath = Path.Combine(RUNCURRENTPATH, "ModelInfo.xml"); //@".\ModelInfo.xml";

                XmlDocument modelDoc = new XmlDocument();
                modelDoc.Load(ModelInfoPath);
                XmlData jpnData = new XmlData();
                XmlNode rootxn = modelDoc.SelectSingleNode("ModelInfo");

                foreach (XmlElement ele in rootxn.ChildNodes)
                {
                    Dictionary<string, string> tempData = new Dictionary<string, string>();
                    foreach (XmlElement nextele in ele.ChildNodes)
                    {
                        string type = nextele.GetAttribute("Type");
                        string jpnName = nextele.GetAttribute("JpnName");
                        tempData.Add(type, jpnName);
                    }
                    jpnData.Add(ele.Name, tempData);
                }
                return jpnData;
            }
            catch
            {
                return null;
            }
        }
    }
}
