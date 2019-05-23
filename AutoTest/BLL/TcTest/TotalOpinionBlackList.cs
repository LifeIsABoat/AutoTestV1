using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tool.BLL
{
    public class RunBlackList
    {
        public List<string> blackList;
        public List<string> regulations;
    }
    public class OpinionRunBlackListInfo
    {
        public string opinionName;
        public RunBlackList NTBlackList;
        public RunBlackList NABlackList;
    }
    class TotalOpinionBlackList
    {
        public List<OpinionRunBlackListInfo> opinionBlackList;
        string path = null;

        public void loadOpinionBlackList(string path)
        {
            this.path = path;
            lodeBlackList();
        }

        private string getTextJson()
        {
            string buf = "";
            //Determine whether a file exists
            if (!File.Exists(path))
            {
                throw new Exception("File does not exist or path error");
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
        private void lodeBlackList()
        {
            if (path == null)
            {
                throw new Exception("Path cannot be empty");
            }
            string buf = getTextJson();
            if (buf == null)
            {
                throw new Exception("File content is empty");
            }
            TotalOpinionBlackList result;
            try { result = Newtonsoft.Json.JsonConvert.DeserializeObject<TotalOpinionBlackList>(buf); }
            catch (System.Threading.ThreadAbortException ex) { throw ex; }
            catch (Exception) { throw new Exception("Execute command error by get invalid command."); }
            if (result != null)
            {
                this.opinionBlackList = result.opinionBlackList;
            }
        }
    }
}
