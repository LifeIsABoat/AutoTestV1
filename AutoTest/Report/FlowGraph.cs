using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.BLL;

namespace Tool.Report
{
    class FlowGraph
    {
        private StreamWriter streamWriter = null;

        public void createFlowGraph()
        {
            IIterator tcIterater;
            if (TestRuntimeAggregate.getcurrentTcManagerName() == "TcManagerScreenCheck")
            {
                tcIterater = TestRuntimeAggregate.getScreenMemory().createScreenIterator();
            }
            else
            {
                tcIterater = TestRuntimeAggregate.getTreeMemory().createMccFilteredTcIterator();
            }
            for (tcIterater.first(); !tcIterater.isDone(); tcIterater.next())
            {
                if (isTcValid(tcIterater.currentItem()))
                {
                    TestRuntimeAggregate.setCurrentTcIndex(tcIterater.currentItem());
                    createDotFile();
                    writeDot();
                    excuteCmd();
                }
            }
        }

        private bool isTcValid(int tcIndex)
        {
            if (TestRuntimeAggregate.getcurrentTcManagerName() == "TcManagerScreenCheck")
            {
                return TestRuntimeAggregate.getLevelInfoListCount(tcIndex) > 0;
            }
            else
            {
                int ftbLevelCount = TestRuntimeAggregate.getTreeMemory().getLevelCount(tcIndex);
                List<string> opinionList = TestRuntimeAggregate.getTcRunManagerOpinion(TestRuntimeAggregate.getcurrentTcManagerName());
                if (null != opinionList)
                {
                    foreach (string opinionName in opinionList)
                    {
                        if (null != TestRuntimeAggregate.getEwsOptionWords(tcIndex, opinionName, ftbLevelCount))
                            return true;
                        if (null != TestRuntimeAggregate.getRspOptionWords(tcIndex, opinionName, ftbLevelCount))
                            return true;
                    }
                }

                return TestRuntimeAggregate.getLevelInfoListCount(tcIndex) > 0;
            }
        }

        private void createDotFile()
        {
            string savePath = StaticEnvironInfo.getFlowGraphFullPath();
            int tcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            FileStream fileStream = new FileStream(savePath + @"\Tc-" + tcIndex + ".dot", FileMode.Create, FileAccess.Write);
            fileStream.Close();
            streamWriter = new StreamWriter(savePath + @"\Tc-" + tcIndex + ".dot", true);
        }
        /* 
         *  Description:write Dot
         *  Param:tcIndex-Tc row index
         *  Param:flag-is option
         *  Return:
         *  Exception:
         *  Example:writeDot(1,true)
         */
        private void writeDot()
        {
            int tcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            int allLevelCount = 0;
            if (TestRuntimeAggregate.getcurrentTcManagerName() == "TcManagerScreenCheck")
            {
                allLevelCount = TestRuntimeAggregate.getScreenMemory().getScreenPath(tcIndex).Count;
            }
            else
            {
                allLevelCount = TestRuntimeAggregate.getTreeMemory().getLevelCount(tcIndex);
            }

            //string lable = "TC-" + tcIndex;
            int fontSize = 0;
            int imageIndex = 1;

            Dictionary<int, TestCheckResult> resultDic = new Dictionary<int, TestCheckResult>();

            streamWriter.Write("digraph structs {\r\n");
            streamWriter.Write("    rankdir = LR\r\n");

            //Write Opinion Image
            List<string> opinionList = TestRuntimeAggregate.getTcRunManagerOpinion(TestRuntimeAggregate.getcurrentTcManagerName());
            if (null != opinionList && opinionList.Count > 0)
            {
                fontSize = 200;
                foreach (string opinionName in opinionList)
                {
                    int validLevelCount = 0;
                    resultDic = getLevelCheckResult(opinionName, tcIndex);
                    for (int levelIndex = 1; levelIndex <= allLevelCount; levelIndex++)
                    {
                        object opinionScreen = TestRuntimeAggregate.getOpinionScreen(tcIndex, opinionName, levelIndex);
                        if (null != opinionScreen)
                        {
                            validLevelCount++;
                            if (validLevelCount == 1)
                            {
                                //write flow graph title
                                writeFlowGraphTitle(opinionName, fontSize);
                            }
                            streamWriter.Write("img" + imageIndex + "[penwidth = 5 margin=0 shape=box, style=filled, fillcolor=white, color=black, label=<<TABLE border=\"0\" cellborder=\"0\">\r\n");

                            //write opinion image path
                            writeOpinionImagePath(opinionScreen, opinionName, tcIndex, levelIndex);

                            //write opinion level title
                            writeOpinionLevelTitle(opinionName, levelIndex, resultDic, imageIndex, validLevelCount);

                            imageIndex++;
                        }
                    }
                    if (validLevelCount > 0)
                    {
                        streamWriter.Write("}\r\n");
                    }

                    //show EWS and RSP flow graph
                    showEWSAndRSPFlowGraph(opinionName, tcIndex, fontSize, allLevelCount, resultDic);
                }

                if (opinionList.Contains("RspDefaultCheckWithFTB") || opinionList.Contains("EwsDefaultCheckWithFTB"))
                {
                    streamWriter.Write("}\r\n");
                    streamWriter.Write("}\r\n");
                    //update to file
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                else
                {
                    //Write Transfer Images
                    //fontSize = 200;
                    streamWriter.Write("subgraph cluster_transfer {\r\n");
                    streamWriter.Write("    fontsize =" + fontSize + "\r\n");
                    streamWriter.Write("    rankdir = LR\r\n");
                    streamWriter.Write("    label = \"LevelTransfer\"\r\n");
                    for (int levelIndex = 1; levelIndex <= allLevelCount; levelIndex++)
                    {
                        object screen = TestRuntimeAggregate.getLogScreen(tcIndex, levelIndex);
                        streamWriter.Write("img" + imageIndex + "[penwidth = 5 margin=0 shape=box, style=filled, fillcolor=white, color=black, label=<<TABLE border=\"0\" cellborder=\"0\">\r\n");
                        if (screen != null)
                        {
                            writeImagePath(screen, tcIndex, levelIndex);                           
                        }
                        else
                        {
                            streamWriter.Write("<TR><TD border=\"1\"><IMG SRC=\" \" scale=\"true\"/></TD></TR>\r\n");
                        }
                        writeLevelTitle(tcIndex, levelIndex, resultDic, imageIndex, opinionList);
                        imageIndex++;
                    }

                    streamWriter.Write("}\r\n");
                    streamWriter.Write("}\r\n");
                    //update to file
                    streamWriter.Flush();
                    streamWriter.Close();

                }
            }
        }

        private void writeImagePath(object screen, int tcIndex, int levelIndex)
        {
            if (screen is Screen)
            {
                string levelImgPath = TestRuntimeAggregate.getTransferImagePath((Screen)screen, tcIndex, levelIndex);
                streamWriter.Write("<TR><TD border=\"1\"><IMG SRC=\"" + levelImgPath + "\" scale=\"true\"/></TD></TR>\r\n");
            }
            else if (screen is AbstractScreenAggregate)
            {
                AbstractScreenAggregate screenAggreagate = (AbstractScreenAggregate)screen;
                int imgCount = screenAggreagate.getCount();

                for (int imgIndex = 0; imgIndex < imgCount; imgIndex++)
                {
                    string levelImgPath = TestRuntimeAggregate.getTransferImagePath(screenAggreagate.readScreen(imgIndex), tcIndex, levelIndex);
                    streamWriter.Write("<TR><TD border=\"1\"><IMG SRC=\"" + levelImgPath + "\" scale=\"true\"/></TD></TR>\r\n");
                }
            }
        }

        private void writeOpinionImagePath(object opinionScreen, string opinionName, int tcIndex, int levelIndex)
        {

            if (opinionScreen is Screen)
            {
                string opinionImgPath = TestRuntimeAggregate.getOpinionImagePath(tcIndex, opinionName, levelIndex, (Screen)opinionScreen);
                streamWriter.Write("<TR><TD border=\"1\"><IMG SRC=\"" + opinionImgPath + "\" scale=\"true\"/></TD></TR>\r\n");
            }
            else
            {
                AbstractScreenAggregate screenAggreagate = (AbstractScreenAggregate)opinionScreen;
                int imgCount = screenAggreagate.getCount();
                for (int imgIndex = 0; imgIndex < imgCount; imgIndex++)
                {
                    string opinionImgPath = TestRuntimeAggregate.getOpinionImagePath(tcIndex, opinionName, levelIndex, screenAggreagate.readScreen(imgIndex));
                    streamWriter.Write("<TR><TD border=\"1\"><IMG SRC=\"" + opinionImgPath + "\" scale=\"true\"/></TD></TR>\r\n");
                }
            }
        }

        private void writeLevelTitle(int tcIndex, int levelIndex, Dictionary<int, TestCheckResult> resultDic, int imageIndex, List<string> opinionList)
        {
            string buttonWord = "";
            int opinionCount = opinionList.Count;
            if (TestRuntimeAggregate.getcurrentTcManagerName() == "TcManagerScreenCheck")
            {
                buttonWord = TestRuntimeAggregate.getScreenMemory().getScreenPath(tcIndex)[levelIndex-1];
            }
            else
            {
                ControlButton button = TestRuntimeAggregate.getLogButton(tcIndex, levelIndex - 1);
                if (button != null && button.stringList != null && button.stringList.Count > 0)
                    buttonWord = button.stringList[0].str;

                if (buttonWord == "")
                {
                    buttonWord = TestRuntimeAggregate.getTreeMemory().getLevelButtonWord(levelIndex - 1, -1, tcIndex);
                    opinionCount = 1;
                }
            }

            streamWriter.Write("</TABLE>>];\r\n");
            if (levelIndex > 1)
            {
                streamWriter.Write("img" + (imageIndex - 1) + "-> img" + imageIndex);
                streamWriter.Write("[label = \"" + buttonWord + "\" fontsize = 100 arrowsize = 8 penwidth = 5]\r\n");
            }
            streamWriter.Write("subgraph cluster_transfer_level_" + levelIndex + "{\r\n");

            //Focus on the LEVEL of NG
            if (resultDic.Keys.Contains(levelIndex))
            {
                if (resultDic[levelIndex] == TestCheckResult.NG && opinionCount == 1)
                {
                    streamWriter.Write("    style = filled\r\n    fillcolor = red\r\n    label = \"Level " + levelIndex + "\"\r\n");
                }
                else
                {
                    streamWriter.Write("    label = \"Level " + levelIndex + "\"\r\n");
                }
            }
            else
            {
                streamWriter.Write("    label = \"Level " + levelIndex + "\"\r\n");
            }

            streamWriter.Write("    fontsize = 150\r\n");
            streamWriter.Write("    img" + imageIndex);
            streamWriter.Write("}\r\n");
        }

        private void writeOpinionLevelTitle(string opinionName, int levelIndex, Dictionary<int, TestCheckResult> resultDic, int imageIndex, int validLevelCount)
        {
            streamWriter.Write("</TABLE>>];\r\n");
            if (validLevelCount > 1)
            {
                streamWriter.Write("img" + (imageIndex - 1) + "-> img" + imageIndex + "\r\n");
            }
            streamWriter.Write("subgraph cluster_opinion" + opinionName + "_level" + levelIndex + "{\r\n");

            //Focus on the LEVEL of NG
            if (resultDic.Keys.Contains(levelIndex))
            {
                if (resultDic[levelIndex] == TestCheckResult.NG)
                {
                    streamWriter.Write("    style = filled\r\n  fillcolor = red\r\n label = \"Level " + levelIndex + "\"\r\n");
                }
                else
                {
                    streamWriter.Write("    label = \"Level " + levelIndex + "\"\r\n");
                }
            }

            streamWriter.Write("    fontsize = 150\r\n");
            streamWriter.Write("    img" + imageIndex);
            streamWriter.Write("}\r\n");
        }

        private void writeFlowGraphTitle(string opinionName, int fontSize)
        {
            streamWriter.Write("subgraph cluster_opinion" + opinionName + "{\r\n");
            streamWriter.Write("    fontsize =" + fontSize + "\r\n");
            streamWriter.Write("    rankdir = LR\r\n");
            streamWriter.Write("    label = \"" + opinionName + "\"\r\n");
            //streamWriter.Write("Node" + opinionIndex + " [fontsize=150 label= " + opinionName + "]\r\n");
        }

        private Dictionary<int, TestCheckResult> getLevelCheckResult(string opinionName, int tcIndex)
        {
            int opinionIndex = -1;
            opinionIndex = TestRuntimeAggregate.getOpinionIndex(opinionName);
            if (opinionIndex == -1)
            {
                return null;
            }
           return TestRuntimeAggregate.getCheckResult(tcIndex, opinionIndex);
        }

        private void showEWSAndRSPFlowGraph(string opinionName, int tcIndex, int fontSize, int levelIndex, Dictionary<int, TestCheckResult> resultDic)
        {
            if (opinionName.Contains("Ews"))
            {
                int ftbLevelCount = TestRuntimeAggregate.getTreeMemory().getLevelCount(tcIndex);
                string words = TestRuntimeAggregate.getEwsOptionWords(tcIndex, opinionName, ftbLevelCount);
                if (null != words)
                {
                    words = words.Replace("\n", "").Replace("\r", "").Replace(" ", "");

                    streamWriter.Write("subgraph cluster_opinion" + opinionName + "{\r\n");
                    streamWriter.Write("    fontsize =" + fontSize + "\r\n");
                    streamWriter.Write("    rankdir = LR\r\n");

                    if (opinionName == "EwsToMachineOptionChecker")
                    {
                        if (resultDic[levelIndex] == TestCheckResult.NG)
                        {
                            streamWriter.Write("    style = filled\r\n  fillcolor = red\r\n label = \"EWS Set\"\r\n");
                        }
                        else
                        {
                            streamWriter.Write("    label = \"EWS Set\"\r\n");
                        }
                    }
                    else if (opinionName == "MachineToEwsOptionChecker")
                    {
                        if (resultDic[levelIndex] == TestCheckResult.NG)
                        {
                            streamWriter.Write("    style = filled\r\n  fillcolor = red\r\n label = \"EWS Get\"\r\n");
                        }
                        else
                        {
                            streamWriter.Write("    label = \"EWS Get\"\r\n");
                        }
                    }
                    else if (opinionName == "EwsDefaultCheckWithFTB")
                    {
                        streamWriter.Write("    label = \"EWS Default\"\r\n");
                    }

                    streamWriter.Write("Node" + opinionName + " [penwidth = 5 margin=0 shape=box, style=filled, fillcolor=white, color=black, fontsize = 150, fontname=\"FangSong\", size =\"20, 20\", label = \"" + words + "\"]\r\n");
                    streamWriter.Write("Node" + opinionName + "\r\n");
                    streamWriter.Write("}\r\n");
                }
            }
            else if (opinionName.Contains("Rsp"))
            {
                int ftbLevelCount = TestRuntimeAggregate.getTreeMemory().getLevelCount(tcIndex);
                string words = TestRuntimeAggregate.getRspOptionWords(tcIndex, opinionName, ftbLevelCount);
                if (null != words)
                {
                    words = words.Replace("\n", "").Replace("\r", "").Replace(" ", "");

                    streamWriter.Write("subgraph cluster_opinion" + opinionName + "{\r\n");
                    streamWriter.Write("    fontsize =" + fontSize + "\r\n");
                    streamWriter.Write("    rankdir = LR\r\n");

                    if (opinionName == "RspToMachineOptionChecker")
                    {
                        if (resultDic[levelIndex] == TestCheckResult.NG)
                        {
                            streamWriter.Write("    style = filled\r\n  fillcolor = red\r\n label = \"RSP Set\"\r\n");
                        }
                        else
                        {
                            streamWriter.Write("    label = \"RSP Set\"\r\n");
                        }
                    }
                    else if (opinionName == "MachineToRspOptionChecker")
                    {
                        if (resultDic[levelIndex] == TestCheckResult.NG)
                        {
                            streamWriter.Write("    style = filled\r\n  fillcolor = red\r\n label = \"RSP Get\"\r\n");
                        }
                        else
                        {
                            streamWriter.Write("    label = \"RSP Get\"\r\n");
                        }
                    }
                    else if (opinionName == "RspDefaultCheckWithFTB")
                    {
                        streamWriter.Write("    label = \"RSP Default\"\r\n");
                    }

                    streamWriter.Write("Node" + opinionName + " [penwidth = 5 margin=0 shape=box, style=filled, fillcolor=white, color=black, fontsize = 150, fontname=\"FangSong\", size =\"20, 20\", label = \"" + words + "\"]\r\n");
                    streamWriter.Write("Node" + opinionName + "\r\n");
                    streamWriter.Write("}\r\n");
                }
            }
        }

        /* 
        *  Description:excute cmd
        *  Param:tcIndex-Tc row index
        *  Return:
        *  Exception:
        *  Example:excuteCmd(1)
        */
        private void excuteCmd()
        {
            string savePath = StaticEnvironInfo.getFlowGraphFullPath();
            int tcIndex = TestRuntimeAggregate.getCurrentTcIndex();
            //cmd command
            string cmd = "dot -Tjpg " + savePath + @"\Tc-" + tcIndex + ".dot -o " + savePath + @"\Tc-" + tcIndex + ".jpg";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;    
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(cmd + "&exit");
            process.StandardInput.AutoFlush = true;
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
        }
    }
}
