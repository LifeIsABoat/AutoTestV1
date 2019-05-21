using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    abstract class AbstractControl
    {
        public Rectangle rect;

        protected AbstractControl(Rectangle rect = null)
        {
            this.rect = rect;
        }

        public abstract void hasFixed();

        public virtual bool Equals(AbstractControl targetControl)
        {
            if (null == targetControl)
                return false;
            if (null != rect && !rect.Equals(targetControl.rect))
                return false;
            if (null == rect && null != targetControl.rect)
                return false;
            return true;
        }
    }
}
