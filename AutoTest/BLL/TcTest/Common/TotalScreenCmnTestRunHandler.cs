using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.DAL;

namespace Tool.BLL
{
    class TotalScreenCmnTestRunHandler : AbstractCmnTestHandler
    {
        AbstractCmnTestHandler handler;
        string tcMangerName = null;
        public TotalScreenCmnTestRunHandler(AbstractCmnTestHandler handler, string tcMangerName = null)
        {
            this.handler = handler;
            this.tcMangerName = tcMangerName;
        }
        public override void execute()
        {
            IScreenCommonAPI screenMemory = TestRuntimeAggregate.getScreenMemory();
            IIterator screenIterator = screenMemory.createScreenIterator();
            //RunAllTCs
            // OneTCRun oneTc = new OneTCRun();
            if (tcMangerName != null)
            {
                StaticLog4NetLogger.reportLogger.Info("Run" + tcMangerName);
            }

            //get json text of condition
            int screenIndex = TestRuntimeAggregate.getCurrentTcIndex();

            TestRuntimeAggregate.setMachineLogPath(screenIndex, StaticEnvironInfo.getMachineLogPath(screenIndex));
            TestRuntimeAggregate.setCommandLogPath(screenIndex, StaticEnvironInfo.getCommandLogPath(screenIndex));
            TestRuntimeAggregate.setOcrLogPath(screenIndex, StaticEnvironInfo.getOcrLogPath(screenIndex));
            TestRuntimeAggregate.setScreenImagePath(screenIndex, StaticEnvironInfo.getScreenImagePath(screenIndex));

            try
            {
                handler.execute();
                StaticLog4NetLogger.reportLogger.Info("Screen-" + screenIndex + "------>OK");
            }
            catch (FTBAutoTestException ex)
            {
                StaticLog4NetLogger.reportLogger.Warn("Screen-" + screenIndex + "------>NG\r\nReason:" + ex.Message + "\r\n");
                StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
            }
            catch (Exception ex)
            {
                StaticLog4NetLogger.reportLogger.Warn("Screen-" + screenIndex + "------>Expection\r\nReason:" + ex.Message + "\r\n");
                StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.StackTrace);
            }

            base.execute();
        }
    }
}
