using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Parser;
using System.ServiceModel;
using Tool.EWSService;

namespace Tool.Engine
{
    class OptioinOperatorByEWS: IOptionOperateAPI
    {
        private Dictionary<string, string> EWSServiceInfo;
        private Dictionary<string, string> EWSIpInfo;
        ServiceClient EWSClient;

        public OptioinOperatorByEWS()
        {
            EWSServiceInfo = new Dictionary<string, string>();
            EWSIpInfo = new Dictionary<string, string>();
            string iniFileName = StaticEnvironInfo.getIntPutModelPath() + @"\" + StaticEnvironInfo.getDocumentewsAndRspConfigfilename();
            InitMachine(iniFileName);
            string serviceAddress = EWSServiceInfo["EWSService"];
            EWSClient = new ServiceClient("BasicHttpBinding_IService1", serviceAddress);
            setMode("EWS");
        }

        private void InitMachine(string iniFileName)
        {
            List<string> keylist = new List<string>();

            INIParser.getKeyList("AutoTestForEWS", iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                //get value
                string value = INIParser.getvalue("AutoTestForEWS", iniFileName, keylist[i]);
                if (value != "")
                {
                    EWSIpInfo.Add(keylist[i], value);
                }
            }
            keylist.Clear();

            INIParser.getKeyList("EWS", iniFileName, keylist);
            for (int i = 0; i < keylist.Count; i++)
            {
                //get value
                string value = INIParser.getvalue("EWS", iniFileName, keylist[i]);
                if (value != "")
                {
                    EWSServiceInfo.Add(keylist[i], value);
                }
            }
            keylist.Clear();
        }

        private void setMode(string mode)
        {
            try
            {
                string result = EWSClient.setMode(mode);
                if (result != "[OK]")
                {
                    throw new Exception(result);
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException ex) { throw ex; }
        }

        private string getMode()
        {
            return EWSClient.getMode();
        }
        public string getMachineIP(string ip)
        {
            return EWSIpInfo[ip];
        }
        public void setMachineIP(string ip)
        {
            if (getMode() != "[OK]EWS")
            {
                throw new Exception("The mode isn't EWS!");
            }

            string result = EWSClient.setPrinterIP(EWSIpInfo[ip]);

            if (result != "[OK]")
            {
                throw new Exception(result);
            }
        }

        public string setOption(List<string> path, string optionWords)
        {
            if (getMode() != "[OK]EWS")
            {
                throw new Exception("The mode isn't EWS!");
            }

            return EWSClient.setOption(path.ToArray(), optionWords);
        }

        public string getOption(List<string> path)
        {
            if (getMode() != "[OK]EWS")
            {
                throw new Exception("The mode isn't EWS!");
            }

            return EWSClient.getOption(path.ToArray());
        }
    }
}

