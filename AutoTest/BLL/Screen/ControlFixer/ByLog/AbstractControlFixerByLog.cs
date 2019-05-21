using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;

namespace Tool.BLL
{
    abstract class AbstractControlFixerByLog : AbstractControlFixer
    {
        protected LogScreen logScreen;
        protected AbstractCommandExecutor rawScreenLoader;
        protected AbstractCommandExecutor tpClicker;
        protected AbstractCommandExecutor previousChanger;

        public AbstractControlFixerByLog(LogScreen logScreen,
                                         ControlFixerCondition condition = null)
            : base(condition)
        {
            //todo add null protect
            this.logScreen = logScreen;
            this.rawScreenLoader = StaticCommandExecutorList.get(CommandList.list_r);
            this.tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
            this.previousChanger = StaticCommandExecutorList.get(CommandList.move_b);
        }
        
        protected ControlButtonStatus parseButtonStatus(LogControl logBtn)
        {
            if (null == logBtn)
                throw new FTBAutoTestException("Can't get button info from null logBtn.");

            if (LogControl._VALIDSTAT == logBtn.btnStatus)
                return ControlButtonStatus.Valid;
            else if (LogControl._SELECTEDSTAT == logBtn.btnStatus)
                return ControlButtonStatus.Selected;
            else if (LogControl._INVALIDSTAT1 == logBtn.btnStatus || LogControl._INVALIDSTAT2 == logBtn.btnStatus)
                return ControlButtonStatus.Invalid;
            else
                throw new FTBAutoTestException("Get unknow arrowType.");
        }

        protected bool isMoveToNewScreenByClick(Position pos,
                                                Screen preScreen,
                                                Screen toScreen)
        {
            //protected
            if (null == preScreen)
                throw new FTBAutoTestException("Previous screen can't be null.");

            //click
            tpClicker.execute(pos);
            System.Threading.Thread.Sleep(500);
            rawScreenLoader.execute(toScreen);

            //move to the same Screen
            if (true == toScreen.EqualsById(preScreen))
                return false;

            //move to a new Screen
            //back to previous Screen
            previousChanger.execute();

            Screen tmpScreen = new Screen();
            rawScreenLoader.execute(tmpScreen);
            if (false == tmpScreen.EqualsById(preScreen))
                throw new FTBAutoTestException("Back to previous screen failed.");
            //if (false == tmpScreen.EqualsByElementList(preScreen))
            //    throw new FTBAutoTestException("Back to previous screen failed.");

            return true;
        }
    }
}
