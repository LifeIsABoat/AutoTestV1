/*
 * File Name: Node.cs
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
     * Description: Node type of tree
     */
    public class ButtonComposite : AbstractScreenComponent
    {
        //Objects of type FTBLableInfo
        public LableInfo ftbButtonTitle;
        //For its own type list
        public List<AbstractScreenComponent> screenComponentList = new List<AbstractScreenComponent>();
        public ButtonComposite()
            : base()
        { }

        /* 
         *  Description:The constructor
         *  Param:ftbButton - Objects of type FTBButtonInfo assignment to ftbButton
         *  Param:ftbButtonTitle - Objects of type FTBLableInfo assignment to ftbButtonTitle
         *  Return:
         *  Exception:
         *  Example:
         */
        public ButtonComposite(ButtonInfo ftbButton, LableInfo ftbButtonTitle = null)
            : base(ftbButton)
        {
            this.ftbButtonTitle = ftbButtonTitle;
        }

        /* 
         *  Description: Add a node to a linked list
         *  Param: screenComponent - Node to add to list
         *  Return:
         *  Exception:
         *  Example:addScreenComponent(screenComponent);
         */
        public override void addScreenComponent(AbstractScreenComponent screenComponent)
        {
            screenComponent.parents = this;
            screenComponentList.Add(screenComponent);
        }
    }//class
}
