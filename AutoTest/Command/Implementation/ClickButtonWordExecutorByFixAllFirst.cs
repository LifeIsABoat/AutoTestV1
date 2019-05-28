using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Command
{
    class ClickButtonWordExecutorByFixAllFirst : AbstractClickButtonWordExecutorMFCTP
    {
        protected override void click(string targetButtonWords)
        {
            ControlButtonIdentify targetButtonIdentify = new ControlButtonIdentify(targetButtonWords);
            StaticCommandExecutorList.get(CommandList.click_b).execute(targetButtonIdentify);
        }
    }
}
