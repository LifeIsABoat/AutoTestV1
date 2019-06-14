using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;
using System.IO;
using System.Windows.Forms;

namespace Tool.UI
{
    class ConditionInfo
    {
        public string condition;
        public ConditionType type;
        public string tcPath;
        //public int tcIndex;
    }
    class ConditionCalibration
    {
        public List<ConditionInfo> conditionList;

        public void setConditionList(List<string> conditionStrList, List<string> tcPathList)
        {
            conditionList = new List<ConditionInfo>();
            for(int num = 0;num< conditionStrList.Count;num++)
            {
                ConditionInfo oneConditionInfo = new ConditionInfo();
                oneConditionInfo.condition = conditionStrList[num];
                oneConditionInfo.tcPath = tcPathList[num];
                //oneConditionInfo.tcIndex = tcIndexList[num];
                if (tcPathList[num].Length > 0)
                {
                    oneConditionInfo.type = ConditionType.OptionSetting;
                }
                else
                {
                    oneConditionInfo.type = ConditionType.HardwareDevice;
                }
                conditionList.Add(oneConditionInfo);
            }
        }

        public void creatJson(string jsonPath)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save the selected Condition Calibration";
            sfd.Filter = "文本文件|*.txt";
            sfd.FileName = jsonPath;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fname = sfd.FileName;
                SaveFile(fname);
            }

            //string jsontext = "";
            //FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write);
            //fs.Close();
            //StreamWriter sw = new StreamWriter(jsonPath, true);

            ////add mcc,condition and level in one object
            ////jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(conditionList);

            //jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            ////write json
            //sw.Write("\r\n" + jsontext + "\r\n");
            //sw.Flush();
            //sw.Close();
        }

        private void SaveFile(string jsonPath)
        {
            FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write);
            fs.Close();
            Stream stream = File.OpenWrite(jsonPath);
            string jsontext = "";
            jsontext = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write("\r\n" + jsontext + "\r\n");
                writer.Flush();
                writer.Close();
            }
        }

        public string getTextJson(string path)
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

        public List<ConditionInfo> loadConditionInfo(string path)
        {
            if (path == null)
            {
                throw new FTBAutoTestException("Path cannot be empty");
            }
            string buf = getTextJson(path);
            if (buf == null)
            {
                throw new FTBAutoTestException("File content is empty");
            }
            ConditionCalibration result;
            try { result = Newtonsoft.Json.JsonConvert.DeserializeObject<ConditionCalibration>(buf); }
            catch (Exception) { throw new FTBAutoTestException("DeserializeObject failed"); }
            if (result != null)
            {
                this.conditionList = result.conditionList;
            }
            else
            {
                throw new FTBAutoTestException("DeserializeObject failed");
            }
            return this.conditionList;
        }
    }
}
