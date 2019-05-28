using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.RSPService;
using System.ServiceModel;
using Tool.Parser;

namespace Tool.Engine
{
    class OptioinOperatorByRSP: IOptionOperateAPI
    {
        private Dictionary<string, string> PrinterIpInfo;
        private Dictionary<string, string> RSPIpInfo;
        ServiceClient RSPClient;

        public OptioinOperatorByRSP()
        {
            PrinterIpInfo = new Dictionary<string, string>();
            RSPIpInfo = new Dictionary<string, string>();
            string iniFileName = StaticEnvironInfo.getIntPutModelPath() + @"\" + StaticEnvironInfo.getDocumentewsAndRspConfigfilename();
            InitMachine(iniFileName);
            //BasicHttpBinding bind = new BasicHttpBinding();
            //bind.Security.Mode = BasicHttpSecurityMode.None;
            //RSPClient = new ServiceClient(bind, edp);
            string serviceAddress = RSPIpInfo["RSPService"];
            RSPClient = new ServiceClient("BasicHttpBinding_IService", serviceAddress);
            setMode("RSP");
        }

        private void InitMachine(string iniFileName)
        {
            List<string> keylist = new List<string>();

            INIParser.getKeyList("AutoTestForRSP", iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                //get value
                string value = INIParser.getvalue("AutoTestForRSP", iniFileName, keylist[i]);
                if (value != "")
                {
                    PrinterIpInfo.Add(keylist[i], value);
                }
            }
            keylist.Clear();

            INIParser.getKeyList("RSP", iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                //get value
                string value = INIParser.getvalue("RSP", iniFileName, keylist[i]);
                if (value != "")
                {
                    RSPIpInfo.Add(keylist[i], value);
                }
            }
            keylist.Clear();

        }

        private void setMode(string mode)
        {
            try
            {
                string result = RSPClient.setMode(mode);
                if (result != "[OK]")
                {
                    throw new Exception(result);
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException ex) { throw ex; }
        }

        private string getMode()
        {
            return RSPClient.getMode();
        }

        public void setMachineIP(string ip)
        {
            if (getMode() != "[OK]RSP")
            {
                throw new Exception("The mode isn't RSP!");
            }

            string result = RSPClient.setPrinterIP(PrinterIpInfo[ip]);

            if (result != "[OK]")
            {
                throw new Exception(result);
            }
        }

        public string setOption(List<string> path, string optionWords)
        {
            if (getMode() != "[OK]RSP")
            {
                throw new Exception("The mode isn't RSP!");
            }

            return RSPClient.setOption(path.ToArray(), optionWords);
        }

        public string getOption(List<string> path)
        {
            if (getMode() != "[OK]RSP")
            {
                throw new Exception("The mode isn't RSP!");
            }

            return RSPClient.getOption(path.ToArray());
        }
    }
}
