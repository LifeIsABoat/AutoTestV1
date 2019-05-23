using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.Script.Serialization;

namespace Tool.BLL
{
    class BlackAndWhiteListInfo
    {
        public ScreenIdentify screenIdentify = new ScreenIdentify();
        public List<ElementString> elementStringList = new List<ElementString>();
        public List<ElementImage> elementImageList = new List<ElementImage>();

        public List<AbstractElement> getElementList()
        {
            List<AbstractElement> elementList = new List<AbstractElement>();
            if (elementStringList != null)
            {
                elementList.AddRange(elementStringList);
            }
            if (elementImageList != null)
            {
                elementList.AddRange(elementImageList);
            }
            if (elementList.Count > 0)
            {
                return elementList;
            }
            else
            {
                return null;
            }
        }
    }
    class LogControlInfo
    {
        public string scrId;
        public List<LogControl> controlInfo = new List<LogControl>();

        public List<LogControl> getControlList()
        {
            if (controlInfo != null || controlInfo.Count > 0)
            {
                return controlInfo;
            }
            else
            {
                return null;
            }
        }
    }
    class ModelInfo
    {
        public List<BlackAndWhiteListInfo> blackList;
        public List<BlackAndWhiteListInfo> whiteList;
        public List<LogControlInfo> virtualList;
        public List<string> tagList;
        public Size screenSize;
        public ScreenIdentify homeScreenIdentify;
        public string buttonStatusPath;
        public string reStartPath;
        public string getCornerPath;
        public Dictionary<ControlButtonStatus, ElementImage> buttonStatus;
        private string path;
        public void loadModelInfo(string path)
        {
            this.path = path;
            loadModel();
        }
        public void saveModelInfo()
        {
            string buf = JsonConvert.SerializeObject(this);
            StreamWriter sw = new StreamWriter(path);
            sw.Write(buf);
            sw.Close();
        }
        private string getTextJson()
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

        private void loadModel()
        {   
            if (path == null)
            {
                throw new FTBAutoTestException("Path cannot be empty");
            }
            string buf = getTextJson();
            if (buf == null)
            {
                throw new FTBAutoTestException("File content is empty");
            }
            ModelInfo result;
            try { result = JsonConvert.DeserializeObject<ModelInfo>(buf); }
            catch (System.Threading.ThreadAbortException ex) { throw ex; }
            catch (Exception) { throw new FTBAutoTestException("DeserializeObject failed"); }
            if (result != null)
            {
                this.blackList = result.blackList;
                this.whiteList = result.whiteList;
                this.virtualList = result.virtualList;
                this.tagList = result.tagList;
                this.screenSize = result.screenSize;
                this.homeScreenIdentify = result.homeScreenIdentify;
                this.buttonStatusPath = result.buttonStatusPath;
                this.buttonStatus = result.buttonStatus;
                this.reStartPath = result.reStartPath;
                this.getCornerPath = result.getCornerPath;
            }
            else
            {
                throw new FTBAutoTestException("DeserializeObject failed");
            }

        }
    }
}
