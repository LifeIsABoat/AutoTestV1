using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;

namespace Tool.BLL
{
    abstract class AbstractScreenAggregate
    {
        //Aggregate Property
        protected AbstractCommandExecutor tpClicker;
        protected AbstractCommandExecutor rawScreenLoader;
        protected AbstractCommandExecutor fixedScreenLoader;
        protected IIterator readIterator, showIterator;

        public static AbstractScreenAggregate import(Screen screen)
        {
            if (null == screen)
                throw new FTBAutoTestException("Import Screen Aggregate Error by invalid screen input.");
            AbstractControl scrollControl = screen.getScrollControl();
            if (null == scrollControl)
                throw new FTBAutoTestException("Import Screen Aggregate Error by invalid screen input.");
            return ((IAggregateCreateAPI)scrollControl).createAggregate();
        }

        //Aggregate Operation
        public abstract Screen getFirstScreen();
        public abstract Screen getLastScreen();
        public abstract int getCount();

        public abstract void deleteScreen(int index);
        public abstract Screen toFirstScreen(Screen screen);
        public abstract void appendScreen(Screen screen,int index = -1);
        public abstract void moveToNextScreen(Screen screen, int index = -1);
        public abstract bool isScreenContains(Screen screen);

        public virtual Screen readScreen(int index)
        {
            throw new FTBAutoTestException("Read screen from screen aggregate by index error.");
        }
        public virtual Screen readScreen(string tagStr)
        {
            throw new FTBAutoTestException("Read screen from screen aggregate by tagString error.");
        }
        public virtual void showScreen(int index)
        {
            throw new FTBAutoTestException("Show screen from screen aggregate by index error.");
        }
        public virtual void showScreen(string tagStr)
        {
            throw new FTBAutoTestException("Show screen from screen aggregate by tagString error.");
        }

        //Aggregate Iterator operation
        public abstract IIterator createReadIterator();
        public abstract IIterator createShowIterator();

        public AbstractScreenAggregate()
        {
            this.tpClicker = StaticCommandExecutorList.get(CommandList.click_p);
            this.rawScreenLoader = StaticCommandExecutorList.get(CommandList.list_r);
            this.fixedScreenLoader = StaticCommandExecutorList.get(CommandList.list_f);
            this.readIterator = null;
            this.showIterator = null;
        }
    }
}
