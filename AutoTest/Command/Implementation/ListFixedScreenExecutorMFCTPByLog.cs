using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    //-=-=-=-=-=-=-=-=-=-=-=-=Parse Logic By Log-=-=-=-=-=-=-=-=-=-=-=-=
    //               Label/Title |  StrBtn/Tag  | Icon/Arrow
    //ElementImage :               05   06 07 08  09 10 11 12
    //ElementString: 01 02 03 04  05 05 06 07 08

    class ListFixedScreenExecutorMFCTPByLog : AbstractListFixedScreenExecutorMFCTP
    {
        //Use as CommonSourseData for fix Logic
        static List<AbstractControlFixer> fixerList = new List<AbstractControlFixer>();//todo

        public ListFixedScreenExecutorMFCTPByLog(LogScreen logScreen)
            :base()
        {
            rawScreenLoader = new ListRawScreenExecutorMFCTPByLog(logScreen);//todo
            //Initial fixer
            fixerList.Add(new ControlInputFixer(logScreen));
            fixerList.Add(new ControlOptionIconFixerByLog(logScreen));
            fixerList.Add(new ControlTagFixerByLog(logScreen));
            fixerList.Add(new ControlArrowFixerByLog(logScreen));
            fixerList.Add(new ControlButtonFixerByLog(logScreen));
            fixerList.Add(new ControlTitleFixer());
            fixerList.Add(new ControlLableFixer());
        }

        protected override void fixScreen(Screen screen)
        {
            foreach (AbstractControlFixer fixer in fixerList)
                fixer.fix(screen);
        }
    }
}
