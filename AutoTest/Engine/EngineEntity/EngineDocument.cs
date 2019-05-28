using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Engine
{
    class EngineDocument
    {
        static protected AbstractEngineCommunicator engineCommunicator;

        static public void setEngineCommunicator(AbstractEngineCommunicator engineCommunicator)
        {
            EngineDocument.engineCommunicator = engineCommunicator;
        }
        static public AbstractEngineCommunicator getEngineCommunicator()
        {
            return engineCommunicator;
        }

        public EngineDocument()
        {
            if (null == engineCommunicator)
                throw new FTBAutoTestException("Create document engine error by null.");
        }


        public void SetTCLibFunc(string dirpath, int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetTCLib";
            cmd.param = dirpath;
            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null == result || "NG" == result)
            {
                throw new FTBAutoTestException("Set screen size to ocr engine failed.");
            }
            return ;
        }

        public List<int> SetMatchTCFunc(string strvector, int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetMatchTC";
            cmd.param = strvector.Replace('/', ' ');
            string result = engineCommunicator.execute(cmd, timeout) as string;

            List<int> IndexList = new List<int>();
            if (null != result)
            {
                string Match_str = System.Text.RegularExpressions.Regex.Replace(result, @"[^-?\d]+", " ").Trim();
                string[] Match_str_list = Match_str.Split(' ');
                foreach (string onestr in Match_str_list)
                {
                    IndexList.Add(Convert.ToInt32(onestr));
                }
                return IndexList;
            }
            else
            {
                throw new FTBAutoTestException("Set screen size to ocr engine failed.");
            }
        }

        public List<int> SetReTCMatchFunc(string strvector, int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetReTCMatch";
            cmd.param = strvector.Replace('/', ' ');
            string result = engineCommunicator.execute(cmd, timeout) as string;

            List<int> IndexList = new List<int>();
            if (null != result)
            {
                string Match_str = System.Text.RegularExpressions.Regex.Replace(result, @"[^-?\d]+", " ").Trim();
                string[] Match_str_list = Match_str.Split(' ');
                foreach (string onestr in Match_str_list)
                {
                    IndexList.Add(Convert.ToInt32(onestr));
                }
                return IndexList;
            }
            else
            {
                throw new FTBAutoTestException("Set screen size to ocr engine failed.");
            }
        }

        public int SetMatchTCDefultFunc(string strvector, int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetMatchTCDefult";
            cmd.param = strvector.Replace('/', ' ');
            string result = engineCommunicator.execute(cmd, timeout) as string;

            int ret = -1;
            if (null != result)
            {
                ret = int.Parse(result);
            }
            else
            {
                throw new FTBAutoTestException("Set screen size to ocr engine failed.");
            }
            return ret;
        }

        public void CloseAppHalt(int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "CloseAppHalt";
            cmd.param = "";
            string result = engineCommunicator.execute(cmd, timeout) as string;

            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Stop document engine failed.");
        }
    }
}
