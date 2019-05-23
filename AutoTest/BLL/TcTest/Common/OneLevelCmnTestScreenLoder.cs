using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.Command;
using Tool.Machine;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tool.BLL
{
    class OneLevelCmnTestScreenLoder : AbstractCmnTestHandler
    {
        //用来fix迁移后显示的画面的链
        private bool ocrAnalaysEnable, buttonAnalaysEnable;
        public OneLevelCmnTestScreenLoder(bool ocrAnalaysEnabel = false, bool buttonAnalaysEnabel = false)
        {
            this.treeMemory = TestRuntimeAggregate.getTreeMemory();
            this.ocrAnalaysEnable = ocrAnalaysEnabel;
            this.buttonAnalaysEnable = buttonAnalaysEnabel;
        }
        public override void execute()
        {
            Screen currentRawScreen = new Screen();
            StaticCommandExecutorList.get(CommandList.list_r).execute(currentRawScreen);
            //get current raw screen
            object runtimeScreen = null;
            //if nowLevel is the last optionLevel,the runtimeScreen is null
            if (treeMemory.isLevelOption(TestRuntimeAggregate.getCurrentLevelIndex()) == false)
            {
                runtimeScreen = TestRuntimeAggregate.getRuntimeScreen(currentRawScreen);
            }
            if (null != runtimeScreen)
            {
                if (runtimeScreen is Screen)
                {
                    //check runtime screen
                    if (!currentRawScreen.EqualsByElementList((Screen)runtimeScreen))
                    {
                        runtimeScreen = null;
                    }
                    else
                    {
                        //set to curent screen
                        StaticCurrentScreen.set((Screen)runtimeScreen);
                        parseSingleScreen((Screen)runtimeScreen);
                    }
                }
                else if (runtimeScreen is AbstractScreenAggregate)
                {
                    //check runtime screen aggregate
                    IIterator readIterator = ((AbstractScreenAggregate)runtimeScreen).createReadIterator();
                    for (readIterator.first(); !readIterator.isDone(); readIterator.next())
                    {
                        //check current screen page
                        Screen screen = ((AbstractScreenAggregate)runtimeScreen).readScreen(readIterator.currentItem());
                        if (!screen.EqualsByElementList(currentRawScreen))
                        {
                            //back to top of list
                            if (TestRuntimeAggregate.getCurrentLevelIndex() > 1)
                            {
                            	//back to previous screen
                                ((AbstractScreenAggregate)runtimeScreen).toFirstScreen(screen);
                            }
                            //set to null for fix again
                            runtimeScreen = null;
                            System.IO.Directory.Delete(StaticEnvironInfo.getScreenImagePath(),true);
                            TestRuntimeAggregate.clearTransferImagePathDict();
                            TestRuntimeAggregate.clearCameraImage();
                            break;
                        }
                        //set to current screen
                        StaticCurrentScreen.set(screen);
                        parseSingleScreen(screen);
                        //move to list screen next page
                        if (readIterator.currentItem() + 1 < ((AbstractScreenAggregate)runtimeScreen).getCount())
                        {
                            ((AbstractScreenAggregate)runtimeScreen).showScreen(readIterator.currentItem() + 1);
                            //update current raw screen
                            currentRawScreen = new Screen();
                            StaticCommandExecutorList.get(CommandList.list_r).execute(currentRawScreen);
                        }
                    }
                }
            }

            if (null == runtimeScreen)
            {
                Screen currentScreen = new Screen();
                StaticCommandExecutorList.get(CommandList.list_f).execute(currentScreen);
                AbstractScreenAggregate screenAggregate = null;
                if (currentScreen.isScrollable())
                {
                    screenAggregate = AbstractScreenAggregate.import(currentScreen);
                    Screen addScreen = screenAggregate.toFirstScreen(currentScreen);
                    while (!screenAggregate.isScreenContains(addScreen))
                    {
                        screenAggregate.appendScreen(addScreen);
                        parseSingleScreen(addScreen);
                        screenAggregate.moveToNextScreen(addScreen);
                        addScreen = new Screen();
                        StaticCommandExecutorList.get(CommandList.list_f).execute(addScreen);
                    }
                }
                else
                {
                    parseSingleScreen(currentScreen);
                }
                if (screenAggregate == null)
                {
                    TestRuntimeAggregate.setLogScreen(currentScreen, TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());
                }
                else
                {
                    TestRuntimeAggregate.setLogScreen(screenAggregate, TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());
                }
            }
            else
            {
                TestRuntimeAggregate.setLogScreen(runtimeScreen, TestRuntimeAggregate.getCurrentTcIndex(), TestRuntimeAggregate.getCurrentLevelIndex());
            }

            base.execute();
        }

        private void parseSingleScreen(Screen screen,int tcIndex = -1,int levelIndex = -1)
        {
            if (false == StaticEnvironInfo.isOcrUsed())
                return;
            //camera
            Engine.EngineCamera camera = new Engine.EngineCamera();
            //ocr
            Engine.EngineOCR ocr = new Engine.EngineOCR();

            //save image
            ScreenSocket asss = new ScreenSocket("127.0.0.1", "10010");
            string imagePath = StaticEnvironInfo.getScreenImageFileName();
            asss.send("GetScreen");
            byte[] imgBuffer = asss.read();
            System.IO.Stream imgStream = new System.IO.MemoryStream(imgBuffer);
            Bitmap bm = new Bitmap(imgStream);
            Bitmap btnew22 = GetSmall(bm, 2);
            btnew22.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            TestRuntimeAggregate.addTransferImagePathDict(screen, imagePath, tcIndex, levelIndex);
            
            //ocr analays
            if (ocrAnalaysEnable)
            {
                //ocr Result storage path
                ocr.setWorkSpacePath(StaticEnvironInfo.getOcrLogPath());
                List<AbstractElement> elementList = screen.getElementList(typeof(ElementString));
                if (elementList == null)
                    return;
                List<ElementString> elementStringList = elementList.ConvertAll(e => (ElementString)e);

                List<string> stringList = ocr.analyzeWords(imagePath, elementStringList);
                Dictionary<ElementString, string> ocrResult = new Dictionary<ElementString, string>();
                for (int i = 0; i < elementStringList.Count; i++)
                {
                    ocrResult.Add(elementStringList[i], stringList[i]);
                }
                TestRuntimeAggregate.addTransferOcrResult(ocrResult);
            }
            //image button status analays
            if (buttonAnalaysEnable)
            {
                List<AbstractControl> abstractButton = screen.getControlList(typeof(ControlButton));
                if (null != abstractButton)
                {
                    List<ControlButton> buttonList = abstractButton.ConvertAll(e => (ControlButton)e);
                    List<ControlButtonStatus> buttonStatus = ocr.analaysButtonStatus(imagePath, buttonList);
                    Dictionary<ControlButton, ControlButtonStatus> buttonStatusResult = new Dictionary<ControlButton, ControlButtonStatus>();
                    for (int i = 0; i < buttonList.Count; i++)
                    {
                        buttonStatusResult.Add(buttonList[i], buttonStatus[i]);
                    }
                    TestRuntimeAggregate.addTransferButtonStatusResult(buttonStatusResult);
                }
            }
        }
        private Bitmap GetSmall(Bitmap bm, double times)
        {
            int nowWidth = (int)(bm.Width * times);
            int nowHeight = (int)(bm.Height * times);
            Bitmap newbm = new Bitmap(nowWidth, nowHeight);//新建一个放大后大小的图片

            if (times >= 1 && times <= 1.1)
            {
                newbm = bm;
            }
            else
            {
                Graphics g = Graphics.FromImage(newbm);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(bm, new System.Drawing.Rectangle(0, 0, nowWidth, nowHeight),
                    new System.Drawing.Rectangle(0, 0, bm.Width, bm.Height), GraphicsUnit.Pixel);
                g.Dispose();
            }
            return newbm;
        }
    }
}
