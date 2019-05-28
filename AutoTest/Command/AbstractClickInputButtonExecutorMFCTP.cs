using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    abstract class AbstractClickInputButtonExecutorMFCTP : AbstractCommandExecutor
    {
        protected AbstractCommandExecutor tpClicker;
        protected AbstractCommandExecutor fixedScreenLoader;

        protected AbstractClickInputButtonExecutorMFCTP()
        {
            this.fixedScreenLoader = StaticCommandExecutorList.get(CommandList.list_f);
            this.tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
        }

        public override void execute(object param)
        {
            if (param is string)
            {
                //example:inputButtonWord = "QWERASDF"
                string inputBtnWordStr = param as string;
                if (inputBtnWordStr != null)
                {
                    //start click softkeyButton
                    StaticLog4NetLogger.commandExecutorLogger.Info("click-s \"" + inputBtnWordStr + "\" start.");
                    try
                    {
                        for (int i = 0; i < inputBtnWordStr.Count(); i++)
                        {
                            string softkeyButton = inputBtnWordStr[i].ToString();
                            click(softkeyButton);
                            StaticLog4NetLogger.commandExecutorLogger.Info("click-s \"" + softkeyButton + "\" succeed.");
                        }
                    }
                    catch (FTBAutoTestException excp)
                    {
                        StaticLog4NetLogger.commandExecutorLogger.Warn("click-s \"" + inputBtnWordStr + "\" failed.\nReason:" + excp.Message);
                        throw excp;
                    }
                }
            }
            else if (param is ControlSoftkeyStatus)
            {
                ControlSoftkeyStatus oneSpecialBtn = (ControlSoftkeyStatus)Enum.Parse(typeof(ControlSoftkeyStatus), param.ToString());

                StaticLog4NetLogger.commandExecutorLogger.Info("click-s \"" + oneSpecialBtn + "\" start.");
                try
                {
                    specialBtnClick(oneSpecialBtn);
                    StaticLog4NetLogger.commandExecutorLogger.Info("click-s \"" + oneSpecialBtn + "\" succeed.");
                }
                catch (FTBAutoTestException excp)
                {
                    StaticLog4NetLogger.commandExecutorLogger.Warn("click-s \"" + oneSpecialBtn + "\" failed.\nReason:" + excp.Message);
                    throw excp;
                }
            }
            else
            {
                throw new FTBAutoTestException("Execute error,Maybe param is null or other.");
            }
        }//end public override void execute()

        protected abstract void click(string softkeyInputBtnWord);
        protected abstract void specialBtnClick(ControlSoftkeyStatus oneSpecialBtn);
    }//end abstract class AbstractClickInputButtonExecutorMFCTP
}
