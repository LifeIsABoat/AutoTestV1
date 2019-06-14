using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    /*
     * Description: Positon calss for Click Operation,include x,y
     */
    class Position
    {
        public int x, y;

        /* 
         *  Description:special constructor
         *  Param:x - x coordinates
         *        y - y coordinates
         *  Return:
         *  Exception:
         *  Example:Position(x,y)
         */
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

