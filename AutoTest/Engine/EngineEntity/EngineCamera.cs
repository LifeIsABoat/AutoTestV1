using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tool.Engine
{
    class EngineCamera
    {
        static private AbstractEngineCommunicator engineCommunicator;

        static public void setEngineCommunicator(AbstractEngineCommunicator engineCommunicator)
        {
            EngineCamera.engineCommunicator = engineCommunicator;
        }
        static public AbstractEngineCommunicator getEngineCommunicator()
        {
            return engineCommunicator;
        }

        //SetCameraConfig
        public static void start(string configFilePath, int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "SetCameraConfig";
            cmd.param = configFilePath;
            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Start camera engine failed.");
        }
        public void capture(string imagePath, int timeout = -1)
        {
            Thread.Sleep(500);//todo
            EngineCommand cmd = new EngineCommand();
            cmd.name = "CaptureScreen";
            cmd.param = imagePath;
            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Camera engine capture screen failed.");
        }
        public static void stop(int timeout = -1)
        {
            EngineCommand cmd = new EngineCommand();
            cmd.name = "HaltCameraApp";
            cmd.param = "";
            string result = engineCommunicator.execute(cmd, timeout) as string;
            if (null != result && "OK" == result)
                return;
            throw new FTBAutoTestException("Stop camera engine failed.");
        }
    }
}
