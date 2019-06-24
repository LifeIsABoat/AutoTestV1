using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ColorDivisionTool.Model
{
    using XmlData = Dictionary<string, Dictionary<string, string>>;
    public class XmlHandle
    {
        private XmlData jpnData = new XmlData();

        /// <summary>
        /// get jpn type from ModelInfo.xml
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetJpnValueFromXml(string model, string type)
        {
            try
            {
                string jpnValue = string.Empty;
                if (jpnData.Count == 0)
                {
                    return null;
                }
                //获取日本语类型
                if (jpnData.Keys.Contains(model) && jpnData[model].Keys.Contains(type))
                {
                    jpnValue = jpnData[model][type];
                }

                return jpnValue;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// load ModelInfo file and get Data from it
        /// </summary>
        /// <returns></returns>
        public XmlData LoadModelInfoFile()
        {
            try
            {
                string RUNCURRENTPATH = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                string ModelInfoPath = Path.Combine(RUNCURRENTPATH, "ModelInfo.xml"); //@".\ModelInfo.xml";

                XmlDocument modelDoc = new XmlDocument();
                modelDoc.Load(ModelInfoPath);
                //XmlData jpnData = new XmlData();
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
