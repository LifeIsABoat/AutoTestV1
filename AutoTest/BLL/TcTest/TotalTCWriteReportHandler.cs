using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class TotalTCWriteReportHandler
    {

        public static void writeReport()
        {
            //writeReport
            StaticLog4NetLogger.reportLogger.Info("Write test result to excel start.\r\n");
            Report.ICreateReport createReport = new Report.CreateReportExcleNpoi();
            createReport.create(TestRuntimeAggregate.getTreeMemory(), TestRuntimeAggregate.getScreenMemory(),
                                StaticEnvironInfo.getReportTemplateFullFileName(),
                                StaticEnvironInfo.getReportSaveFullFileName());
            StaticLog4NetLogger.reportLogger.Info("Write test result to excel finished.\r\n");
        }
    }
}
