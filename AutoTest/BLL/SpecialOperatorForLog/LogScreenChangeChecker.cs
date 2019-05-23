using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Tool.Machine;
using Tool.Command;

namespace Tool.BLL
{
    static class LogScreenChangeChecker
    {
        private static AbstractComponentMachineIO io = null;
        private static Stopwatch stopwatch = new Stopwatch();
        //private static Screen standardScreen = null;
        //private static LogParserMFCTP logParser;
        //private static LogScreenTranfer logTransfer;

        //static LogScreenChangeChecker()
        //{
        //    logParser = new LogParserMFCTP();
        //    logTransfer = new LogScreenTranfer();
        //}

        public static void setIO(AbstractComponentMachineIO io)
        {
            LogScreenChangeChecker.io = io;
        }

        //command
        public static void load()
        {
            //standardScreen = new Screen();
            //StaticCommandExecutorList.get(CommandList.list_r).execute(standardScreen);
        }

        public static bool check(uint timeOutMs)
        {
            if (null == io)
                throw new FTBAutoTestException("Create LogScreenChangeChecker error by null io.");

            bool logReadOpenFlag = io.isReadON();
            //Open Log Reader
            if (!logReadOpenFlag && io.isReadOFF())
                io.readON();

            string buf = "";
            stopwatch.Restart();
            while (!LogParserMFCTP.isScrIdFinished(buf))
            {
                if (stopwatch.ElapsedMilliseconds > timeOutMs)
                {
                    break;
                }
                System.Threading.Thread.Sleep(5);
                buf += io.read();

                //Screen tmpScreen = new Screen();
                //StaticCommandExecutorList.get(CommandList.list_r).execute(tmpScreen);
                //if (!tmpScreen.EqualsByStringList(standardScreen))
                //{
                //    break;
                //}
            }
            stopwatch.Stop();

            if (buf != "")
            {
                StaticLog4NetLogger.mfcLogger.Debug("ReadLog.\r\n" + buf);
                StaticLog4NetLogger.mfcLogger.Info("ReadLog.");
            }

            //Close Log Reader
            if (!logReadOpenFlag && io.isReadON())
                io.readOFF();

            return true;
        }
    }
}
