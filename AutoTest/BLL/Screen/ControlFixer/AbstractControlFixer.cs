using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    abstract class AbstractControlFixer
    {
        private ControlFixerCondition condition;

        public AbstractControlFixer(ControlFixerCondition condition = null)
        {
            if (null == condition)
                this.condition = new ControlFixerCondition();
            else
                this.condition = condition;
        }

        protected abstract void doFix(Screen rawScreen);
        public void fix(Screen rawScreen)
        {
            if (condition.isValid(rawScreen.getIdentify()))
                doFix(rawScreen);
        }
    }
}
