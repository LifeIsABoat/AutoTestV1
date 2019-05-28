using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    class MoveToHomeScreenExecutorBC4 : AbstractMoveToHomeScreenExecutorMFCTP
    {
        public MoveToHomeScreenExecutorBC4(ScreenIdentify homeScreenIdentify)
            :base(homeScreenIdentify)
        {

        }

        protected override void waitForBackHome()
        {
            //wait for 600 s 
            for (int cnt = 0; cnt < 120; cnt++)
            {
                //Check Screen Change for 5s
                LogScreenChangeChecker.check(5000);

                //check home
                if (true == checkHome())
                    break;
            }
        }
    }
}
