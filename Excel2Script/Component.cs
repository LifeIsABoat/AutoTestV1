using System;
using System.Collections.Generic;

namespace FTBExcel2Script
{
    public abstract class FTBComponent
    {

        public abstract void Add(FTBComponent c);
        public abstract void Remove(FTBComponent c);
        public abstract void Display(int depth);
    }
    public class OptionNode : FTBComponent
    {
        public FTBOptionInfo ftboption
        {
            get;
            set;
        }
        public FTBButtionTitleInfo ftbbuttontitle
        {
            get;
            set;
        }
        public OptionNode(FTBOptionInfo ftbOption, FTBButtionTitleInfo ftbButtonTitle)
            : base()
        {
            this.ftboption = ftbOption;
            this.ftbbuttontitle = ftbButtonTitle;
        }
        public OptionNode()
            : base()
        {
            ftboption = new FTBOptionInfo();
            ftbbuttontitle = new FTBButtionTitleInfo();
        }
        public override void Add(FTBComponent c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }
        public override void Remove(FTBComponent c)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth));
        }
    }

    public class LevelNode : FTBComponent
    {
        public List<FTBComponent> levelnodes
        {
            get;
            set;
        }
        public FTBButtonInfo ftbbutton
        {
            get;
            set;
        }
        public FTBButtionTitleInfo ftbbuttontitle
        {
            get;
            set;
        }
        public LevelNode(FTBButtonInfo ftbButton, FTBButtionTitleInfo ftbButtonTitle) : base()
        {
            this.ftbbutton = ftbButton;
            this.ftbbuttontitle = ftbButtonTitle;
            levelnodes = new List<FTBComponent>();
        }
        public LevelNode() : base()
        {
            ftbbutton = new FTBButtonInfo();
            ftbbuttontitle = new FTBButtionTitleInfo();
            levelnodes = new List<FTBComponent>();
        }

        public override void Add(FTBComponent component)
        {
            levelnodes.Add(component);
        }
        public override void Remove(FTBComponent component)
        {
            levelnodes.Remove(component);
        }

        public void RemoveAt(int index)
        {
            levelnodes.RemoveAt(index);
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth));
            foreach (FTBComponent component in levelnodes)
            {
                component.Display(depth + 2);
            }
        }
    }
}
