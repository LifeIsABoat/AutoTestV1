using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.UI;

namespace Tool.DAL
{
    interface IScreenCommonAPI : IScreenCommonAggregate
    {
        int getScreenLines();
        List<string> getScreenCondition(int currentIndex);
        List<string> getScreenPath(int currentIndex);
        List<string> getScreenWords(int currentIndex);
        int getScreenCount();
        List<StandardScreen> getNowStandardScreen();
    }
}
