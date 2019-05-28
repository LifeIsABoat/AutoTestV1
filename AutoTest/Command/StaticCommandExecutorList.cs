using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Command
{
    static class CommandList//todo
    {
        public const string move = "move";
        public const string move_d_s = "move-d-s";
        public const string move_d_id = "move-d-id";
        public const string move_r = "move~";
        public const string move_b = "move../";

        public const string click = "click";
        public const string click_w = "click-w";
        public const string click_b = "click-b";
        public const string click_k = "click-k";
        public const string click_p = "click-pos";
        public const string click_s = "click_s";//click softkey

        public const string list_LS = "list_LS";//get ScreenParamByLog

        public const string list = "list";
        public const string list_r = "list-r";
        public const string list_f = "list-f";
    }
    static class StaticCommandExecutorList
    {
        static ConcurrentDictionary<string, AbstractCommandExecutor> commandExecutorList = new ConcurrentDictionary<string, AbstractCommandExecutor>();

        public static void add(string command, AbstractCommandExecutor executor)
        {
            if (null == command)
                throw new FTBAutoTestException("Command name can't be null.");
            if (null == executor)
                throw new FTBAutoTestException("Command Operator can't be null");
            commandExecutorList.TryAdd(command, executor);
        }


        //it must return a valid operator
        public static AbstractCommandExecutor get(string command)
        {
            if (null == command)
                throw new FTBAutoTestException("Command name can't be null.");

            if (!commandExecutorList.ContainsKey(command))
                throw new FTBAutoTestException("Command\"" + command + "\" is not existed in operator list");

            return commandExecutorList[command];
        }
    }
}
