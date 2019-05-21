using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    abstract class AbstractControlArrow : AbstractControl, IAggregateCreateAPI
    {
        //the UpArrow index must be 0 and Down must be 1
        protected List<ControlButton> arrowList;

        protected static string homeScrId;
        public static void setHomeScrId(string scrId)
        {
            AbstractControlArrow.homeScrId = scrId;
        }
        public ControlButton getFirstArrow()
        {
            if (null != arrowList)
                return arrowList[0];
            else
                return null;
        }
        public ControlButton getLastArrow()
        {
            if (null != arrowList)
                return arrowList[1];
            else
                return null;
        }
        public override void hasFixed()
        {
            if (null != arrowList)
            {
                foreach (ControlButton arrow in arrowList)
                {
                    arrow.imageList[0].hasFixed();
                }
            }
        }
        public override bool Equals(AbstractControl targetControl)
        {
            AbstractControlArrow arrow = targetControl as AbstractControlArrow;
            if (null == arrow)
                return false;

            if (null != getFirstArrow() && !getFirstArrow().Equals((object)arrow.getFirstArrow()))
                return false;
            if (null == getFirstArrow() && null != arrow.getFirstArrow())
                return false;

            if (null != getLastArrow() && getLastArrow().Equals((object)arrow.getLastArrow()))
                return false;
            if (null == getLastArrow() && null != arrow.getLastArrow())
                return false;

            return base.Equals(targetControl);
        }
        public abstract AbstractScreenAggregate createAggregate();
        protected AbstractControlArrow(List<ControlButton> arrowList)
            :base()
        {
            if (null == arrowList)
                throw new FTBAutoTestException("Create ControlArrow failed by input empty arrowList");

            if (2 != arrowList.Count)
                throw new FTBAutoTestException("Create ControlArrow failed by input invalid arrowList.");

            this.arrowList = arrowList;
        }
    }
}
