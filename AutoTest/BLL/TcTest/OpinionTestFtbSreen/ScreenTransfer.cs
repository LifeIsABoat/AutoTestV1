using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tool.DAL;
using Tool.Command;
using System.Web.Script.Serialization;

namespace Tool.BLL
{

    class ScreenTransfer : AbstractCmnTestHandler
    {
        int screenCount = 0;
        AbstractCmnTestHandler handler;
        IIterator ScreenIterator;
        public ScreenTransfer(AbstractCmnTestHandler handler)
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
            this.screenMemory = TestRuntimeAggregate.getScreenMemory();
            this.ScreenIterator = screenMemory.createScreenIterator();
            this.handler = handler;
        }

        public override void execute()
        {
            treeMemory.importScreenDict();

            for (ScreenIterator.first(); !ScreenIterator.isDone(); ScreenIterator.next())
            {
                int screenIndex = ScreenIterator.currentItem();
                //goHome
                StaticCommandExecutorList.get(CommandList.move_r).execute();

                //set 
                TestRuntimeAggregate.setCurrentTcIndex(screenIndex);

                TestRuntimeAggregate.setMachineLogPath(screenIndex, StaticEnvironInfo.getMachineLogPath(screenIndex));
                TestRuntimeAggregate.setCommandLogPath(screenIndex, StaticEnvironInfo.getCommandLogPath(screenIndex));
                TestRuntimeAggregate.setOcrLogPath(screenIndex, StaticEnvironInfo.getOcrLogPath(screenIndex));
                TestRuntimeAggregate.setScreenImagePath(screenIndex, StaticEnvironInfo.getScreenImagePath(screenIndex));

                //handle
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
                catch (System.Threading.ThreadAbortException ex)
                {
                    break;
                }
                catch (Exception ex)
                {
                    StaticLog4NetLogger.reportLogger.Warn("Screen-" + screenIndex + "------>Expection\r\nReason:" + ex.Message + "\r\n");
                    StaticLog4NetLogger.reportLogger.Warn(ex.StackTrace);
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                }
                finally
                {
                    TestRuntimeAggregate.setTcMessage(Math.Round(100.0 * (screenCount++ + 1) / screenMemory.getScreenCount(), 2) + "%");
                }
            }
           
            base.execute();
        }
    }
}
