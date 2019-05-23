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
    public class SingleOpinionInfo
    {
        public string opinionName;
        public string opinionDetail;
        public string opinionRange;
        public string opinionType;
        //List<List<string>> opinionBlackList;
        //List<List<string>> opinionWhiteList;
    }

    class TotalOpinionInfo
    {
        public List<SingleOpinionInfo> opinionList = new List<SingleOpinionInfo>();
        public Dictionary<string, List<string>> tcRunManagerShip = new Dictionary<string, List<string>>();

        public void loadOpinionInfo(string path)
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
            TotalOpinionInfo result;
            try { result = Newtonsoft.Json.JsonConvert.DeserializeObject<TotalOpinionInfo>(buf); }
            catch (System.Threading.ThreadAbortException ex) { throw ex; }
            catch (Exception) { throw new FTBAutoTestException("DeserializeObject failed"); }
            if (result != null)
            {
                opinionList = result.opinionList;
                tcRunManagerShip = result.tcRunManagerShip;
            }
            else
            {
                throw new FTBAutoTestException("DeserializeObject failed");
            }
        }
        public int getOpinionIndex(string opinionName)
        {
            int index = -1;
            for (int i = 0; i < opinionList.Count; i++)
            {
                if (opinionList[i].opinionName == opinionName)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        private string getTextJson(string path)
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
    }
}
