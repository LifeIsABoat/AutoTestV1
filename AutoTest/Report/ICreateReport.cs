using Tool.DAL;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Report
{
    interface ICreateReport
    {
        void writeLevel(DAL.IFTBCommonAPI treeMemory);
        void writeOption(List<string> option);
        void writeImageLink(Dictionary<int, List<string>> tcPath, ISheet sheetName);
        void writeOptionList(string opinionName,string detail,string range, ISheet sheetName);
        void writeBaseInfo( string baseInfoDetail, ISheet sheetName);
        void save(string path);
        void create(IFTBCommonAPI treeMemory, IScreenCommonAPI screenMemory, string templetExclePath, string savePath);
        void writeConclusionAndOpinionResult(Dictionary<int, BLL.TestCheckResult> optionResult, int i, ISheet sheetName, Dictionary<int, List<string>> tcPath);
    }
}
