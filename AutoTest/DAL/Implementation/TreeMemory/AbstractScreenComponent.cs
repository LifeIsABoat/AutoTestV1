/*
 * File Name: Tree.cs
 * Creat Date: 2017-3-22
 * Description: 
 * Modify Content:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.DAL
{
    /*
     * Description: Base class for combination mode
     */
    public abstract class AbstractScreenComponent
    {
        //Point father node
        public AbstractScreenComponent parents;
        //Objects of type FTBButtonInfo
        public ButtonInfo ftbButton;
        public List<string> stringHelpInfoList;

        public AbstractScreenComponent()
        {
            ftbButton = null;
            parents = null;
        }

   
        /* 
         *  Description:The constructor
         *  Param:ftbButton - Objects of type FTBButtonInfo assignment to ftbButton
         *  Return:
         *  Exception:
         *  Example:
         */
        public AbstractScreenComponent(ButtonInfo ftbButton)
            : this()
        {
            parents = null;
            this.ftbButton = ftbButton;
        }
        public abstract void addScreenComponent(AbstractScreenComponent screenComponent);
    }//class
}
