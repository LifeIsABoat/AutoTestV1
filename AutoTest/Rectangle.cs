using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    /*
     * Description: Rectangle calss for String Info and Button Info
     */
    class Rectangle
    {
        public int x, y, w, h;

        public Rectangle()
        {

        }
        /* 
         *  Description:special constructor
         *  Param:x - x coordinates of left top point
         *        y - y coordinates of left top point
         *        w - weight of rectangle
         *        h - height fo rectangle
         *  Return:
         *  Exception:
         *  Example:Rectangle(x,y,w,h)
         */
        public Rectangle(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        /* 
         *  Description:Get Button Rectangle Center Positon
         *  Param:
         *  Return:Center Positon of Rectangle
         *  Exception:
         *  Example:Positon pos = currentButton.getCenter()
         */
        public Position getCenter()
        {
            Position center = new Position(x + w / 2, y + h / 2);
            return center;
        }

        /* 
         *  Description:Get Button Rectangle Left Top Positon
         *  Param:
         *  Return:LeftTop Positon of Rectangle
         *  Exception:
         *  Example:Positon pos = currentButton.getLeftTopPos()
         */
        public Position getLeftTopPos()
        {
            Position leftTop = new Position(x, y);
            return leftTop;
        }

        /* 
         *  Description:override Rectangle class minus operator
         *  Param:
         *  Return:diff rectangle
         *  Exception:
         *  Example:Rectangle rectDiff = rect1 - rect2;
         */
        public static Rectangle operator -(Rectangle rect1, Rectangle rect2)
        {
            return new Rectangle(rect1.x - rect2.x,
                                 rect1.y - rect2.y,
                                 rect1.w - rect2.w,
                                 rect1.h - rect1.h);
        }

        public override string ToString()
        {
            return string.Format("{0:d3},{1:d3},{2:d3},{3:d3}", x, y, w, h);
        }

        public bool Equals(Rectangle rect)
        {
            if (null == rect)
                return false;
            if (rect.x != x || rect.y != y || rect.h != h || rect.w != w)
                return false;
            return true;
        }
    }
}
