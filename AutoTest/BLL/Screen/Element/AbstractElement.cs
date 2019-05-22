using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    abstract class AbstractElement
    {
        public ushort? id;
        public Rectangle rect;
        protected bool fixedFlag;

        public void hasFixed()
        {
            this.fixedFlag = true;
        }

        public bool isFixed()
        {
            return this.fixedFlag;
        }

        public virtual bool EqualsByAllAttribute(AbstractElement targetElement)
        {
            if (null == targetElement)
                return false;
            if (null != rect && !rect.Equals(targetElement.rect))
                return false;
            if (null == rect && null != targetElement.rect)
                return false;
            return true;
        }
        public virtual bool EqualsBySingleAttribute(AbstractElement targetElement)
        {
            if (null == targetElement)
                return false;
            if (null != rect && rect.Equals(targetElement.rect))
                return true;
            
            return false;
        }

        protected AbstractElement(ushort? id = null,
                                  Rectangle rect = null)
        {
            this.id = id;
            this.rect = rect;
            this.fixedFlag = false;
        }
    }
}
