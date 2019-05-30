using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tool.DAL
{
    class DirectHelper
    {
        private string tarGetStr;//define tarGetStr
        public void execute(AbstractScreenComponent currentNode, AbstractScreenComponent tempNode)
        {
            //if currentNode.ftbButton.usWords is contains "[Direct:]"
            //Match string to delete the "[Direct:]"
            string input = currentNode.ftbButton.usWords;
            string pattern = @"(?<=\[?Direct:)(.*)[^\]?]";
            Match Str = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            //Assign Str.ToString() to tarGetStr
            tarGetStr = Str.ToString();
            //do find method
            AbstractScreenComponent tarGetNode = find(tempNode, tarGetStr);
            if (tarGetNode is ButtonComposite)
            {
                currentNode.stringHelpInfoList = new List<string>();
                //Add node's usWords to currentNode.stringaggregate
                foreach (AbstractScreenComponent node in ((ButtonComposite)tarGetNode).screenComponentList)
                {
                    currentNode.stringHelpInfoList.Add(node.ftbButton.usWords);
                }
            }
        }//end excute

        private AbstractScreenComponent find(AbstractScreenComponent notLeafNode, string tarGetStr)
        {
            if (notLeafNode is ButtonComposite)
            {
                foreach (AbstractScreenComponent node in ((ButtonComposite)notLeafNode).screenComponentList)
                {
                    if (node is ButtonComposite)
                    {
                        //if node's usWords == tarGetStr
                        if (((ButtonComposite)node).ftbButtonTitle.usWords == tarGetStr)
                        {
                            //return now node
                            return node;
                        }
                        else
                        {
                            //do node recursion
                            AbstractScreenComponent findResult = find(node, tarGetStr);
                            if (findResult != null)
                            {
                                //do recursion
                                return findResult;
                            }
                        }
                    }//end if node is ButtonComposite
                }//end foreach
            }//end if notleafnode is ButtonComposite
            else
            {
                //do Nothing
            }
            return null;
        }//end find(tempNode, targetStr)
    }//end class DirectHelper

    class MoveToHelper
    {
        public List<string> strList = new List<string>();
        public void execute(AbstractScreenComponent currentNode, AbstractScreenComponent root)
        {
            //if root.ftbButton.usWords is contains "move to"
            //delete the "Move to"
            string ftbMovetoUswords = currentNode.ftbButton.usWords;
            string path = @"[^move to ""](.+)[^""]";
            //Match string
            Match mt = Regex.Match(ftbMovetoUswords, path, RegexOptions.IgnoreCase);
            //after Match then do Split('|')
            string[] moveto = mt.ToString().Split('|');
            //Assign the moveto to the address
            List<string> address = new List<string>(moveto);
            //from root to find node address
            AbstractScreenComponent tarGetNode = find(root, address);
            if (tarGetNode is ButtonComposite)
            {
                currentNode.stringHelpInfoList = new List<string>();
                //Add targetnode's usWords to currentNode.stringAggregate
                currentNode.stringHelpInfoList.Add(((ButtonComposite)tarGetNode).ftbButtonTitle.usWords);
            }//end if targetnode is ButtonComposite
        }//end excute

        private AbstractScreenComponent find(AbstractScreenComponent notLeafNode, List<string> address)
        {
            if (notLeafNode is ButtonComposite)
            {
                foreach (AbstractScreenComponent node in ((ButtonComposite)notLeafNode).screenComponentList)
                {
                    strList = getPath(node);
                    //convert  "address" and "strList" to string
                    string addressJudge = string.Join("", address.ToArray());
                    string strListJudge = string.Join("", strList.ToArray());
                    //if "string[strListjudge]" is Contains "string[strListjudge]"
                    if (strListJudge.Contains(addressJudge) && strList[strList.Count - 1] == address[address.Count - 1])
                    {
                        //return now node
                        return node;
                    }
                    else
                    {
                        //if strList is contains the "[]"
                        //delete the "[]"
                        string pattern = strList[0];
                        string regular = @"[^\[]([^\]])*[^\]]";
                        //Match string
                        Match strList1 = Regex.Match(pattern, regular, RegexOptions.IgnoreCase);
                        //Assign strList1.ToString() to strList[0]
                        strList[0] = strList1.ToString();
                        //convert  "address" and "strList" to string
                        addressJudge = string.Join("", address.ToArray());
                        strListJudge = string.Join("", strList.ToArray());
                        //if "string[strListjudge]" is Contains "string[strListjudge]"
                        if (strListJudge.Contains(addressJudge) && strList[strList.Count - 1] == address[address.Count - 1])
                        {
                            //return now node
                            return node;
                        }
                        //do recursion
                        AbstractScreenComponent findResult = find(node, address);
                        if (findResult != null)
                        {
                            //do recursion
                            return findResult;
                        }
                    }//end else
                }//end foreach
            }//end if notleafnode is ButtonComposite
            else
            {
                //do Nothing
            }
            return null;
        }//end find(root, address)
        private List<string> getPath(AbstractScreenComponent node)
        {
            AbstractScreenComponent Node = node;
            List<string> strList = new List<string>();
            while (true)
            {
                if (Node != null)
                {
                    //if Node's upper strata level is null
                    if (Node.parents == null)
                    {
                        //do Nothing
                        break;
                    }
                    else
                    {
                        //get now Node.parents.uswords
                        strList.Add(Node.ftbButton.usWords);
                        //do node recursion
                        Node = Node.parents;
                    }
                }//end if
                else
                {
                    //do Nothing
                    break;
                }
            }//end while
            //do strList Reverse then return the strList
            strList.Reverse();
            return strList;
        }//end private getpath
    }//end class MoveToHelper

    class ButtonWordFix
    {
        AbstractScreenComponent root;
        DirectHelper directFix = new DirectHelper();
        MoveToHelper moveToFix = new MoveToHelper();
        List<string> profileNameList;
        public ButtonWordFix(List<string> profileNameList)
        {
            this.profileNameList = profileNameList;
            //get root from FTBtree
            this.root = TreeMemoryFTBCommonAggregate.getRoot();
        }
        public void execute()
        {
            if (this.root is ButtonComposite)
            {
                foreach (AbstractScreenComponent node in ((ButtonComposite)this.root).screenComponentList)
                {
                    //do TempNodeChecked recursion
                    TempNodeChecked(node);
                }
            }//end
        }//end public excute

        private void TempNodeChecked(AbstractScreenComponent mideSideNode)
        {
            string butttonWord = mideSideNode.ftbButton.usWords;
            //if butttonWord is contains "Direct:"
            if (Regex.IsMatch(butttonWord, @"^\[?Direct:", RegexOptions.IgnoreCase))
            {
                AbstractScreenComponent tempNode = mideSideNode;
                while (true)
                {
                    if(tempNode.parents != null || tempNode.parents.parents != null)
                    {
                        //tempNode from which level
                        if (tempNode.parents.parents.parents == null)
                        {
                            //do Nothing
                            break;
                        }
                        else
                        {
                            //do node recursion
                            tempNode = tempNode.parents;
                        }
                    }//end if
                    else
                    {
                        //do Nothing
                        break;
                    }
                }//end while
                //do direct
                directFix.execute(mideSideNode, tempNode);
            }//end if match
            //if butttonWord is contains "Move to"
            else if (Regex.IsMatch(butttonWord, "^Move to ", RegexOptions.IgnoreCase))
            {
                //do move to
                moveToFix.execute(mideSideNode, this.root);
            }
            //if butttonWord is contains "Select:"
            else if (Regex.IsMatch(butttonWord, @"^\[?Select:", RegexOptions.IgnoreCase))
            {
                mideSideNode.stringHelpInfoList = this.profileNameList;
            }//end
            if (mideSideNode is ButtonComposite)
            {
                foreach (AbstractScreenComponent node in ((ButtonComposite)mideSideNode).screenComponentList)
                {
                    //do TempNodeChecked recursion
                    TempNodeChecked(node);
                }
            }
            else if (mideSideNode is OptionLeaf)
            {
                //do Nothing
            }
        }//end TempNodeChecked recursion
    }//end class ButtonWordFix
}
