using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Tool.DAL;
using Tool.BLL;

namespace Tool.UI
{
    public partial class TCSelectForm : Form
    {

        private IFTBCommonAPI tree;
        bool isCreated = false;
        private bool noConditionflag = false;

        public TCSelectForm()
        {
            InitializeComponent();
            this.OKButton.Enabled = false;
            isCreated = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TCSelectForm_Load(object sender, EventArgs e)
        {
            IIterator tcIterator = tree.createMccFilteredTcIterator();
            IIterator levelIterator = tree.createLevelIterator();
            TreeNode nowNode = null;

            //temp fax 2
            int preTcIndex = 0;
            for (tcIterator.first(); !tcIterator.isDone(); tcIterator.next())
            {
                int tcIndex = tcIterator.currentItem();
                bool tempNoConditionFlag = false;
                for (levelIterator.first(); !levelIterator.isDone(); levelIterator.next())
                {
                    int i = levelIterator.currentItem();
                    string str = tree.getLevelButtonWord();
                    if (str == "")
                    {
                        str = @"""";
                    }
                    if (noConditionflag == true)
                    {
                        if (0 != tree.getLevelCondition())
                        {
                            tempNoConditionFlag = true;
                        }
                    }
                    TreeNodeCollection nodecollection;
                    if (i == 0)
                    {
                        nodecollection = treeView1.Nodes;                       
                    }
                    else
                    {
                        nodecollection = nowNode.Nodes;                                               
                    }
                    if (nodecollection != null && (nodecollection.Find(str, false).Count() > 0 || nodecollection.Find((str + tcIndex.ToString()), false).Count() > 0))
                    {
                        foreach (TreeNode t in nodecollection)
                        {
                            if (t.Name == str || t.Name == (str + tcIndex.ToString()))
                            {
                                nowNode = t;
                                if (tempNoConditionFlag == true)
                                {
                                    nowNode.ForeColor = Color.Gray;
                                    //CheckParent(nowNode);
                                }
                                else
                                {
                                    if (nowNode.ForeColor == Color.Gray)
                                    {
                                        if (nowNode.StateImageKey == "select")
                                        {
                                            nowNode.ForeColor = Color.Blue;
                                            this.CheckSelect(nowNode);
                                        }
                                        else
                                        {
                                            nowNode.ForeColor = Color.Black;
                                            if (nowNode.Checked == true)
                                            {
                                                this.CheckSelect(nowNode);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        continue;
                    }
                    TreeNode n = new TreeNode();
                    n.Text = str;
                    n.Name = str;
                    n.Checked = true;
                    n.StateImageKey = "nomal";
                    nodecollection.Add(n);
                    nowNode = n;        
                    if (tempNoConditionFlag == true)
                    {
                        nowNode.ForeColor = Color.Gray;
                    }
                }//for(levelIterator)
                //nowNode.Name += ("(_" + tcIndex.ToString() + "_)");
                if (isCreated == false)
                {
                    nowNode.Name += tcIndex.ToString();
                    nowNode.Tag = tcIndex;
                    preTcIndex = tcIndex;
                }
                
            }//for(tcIterator)
            isCreated = true;
            this.OKButton.Enabled = true;
            this.ApplyButton.Enabled = false;
        }

        public void set_tree(object t)
        {
            this.tree = (IFTBCommonAPI)t;
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //节点关系   
            this.ApplyButton.Enabled = true;
            if (e.Action == TreeViewAction.ByMouse)
            {
                //e.Node.ForeColor = System.Drawing.SystemColors.ControlText;
                e.Node.ForeColor = Color.Black;
                this.CheckParent(e.Node);
                this.CheckChild(e.Node);
                this.CheckSelect(e.Node);
                this.CheckSelectDark(e.Node);
            } 
        }

        //节点之间关联
        private void CheckParent(TreeNode node)
        {
            if (node.Parent != null)
            {
                if (node.Checked)
                {
                    foreach (TreeNode brother in node.Parent.Nodes)
                    {
                        if (brother.Checked == false)
                        {
                            node.Parent.Checked = false;
                            return;
                        }
                    }
                    node.Parent.Checked = node.Checked;
                    CheckParent(node.Parent);
                }
                else
                {
                    node.Parent.Checked = false;
                    CheckParent(node.Parent);
                }
                if (node.Parent.Checked == true)
                {
                    node.Parent.StateImageKey = "nomal";
                }              
            }
        }

        //选择全部子节点
        private void CheckChild(TreeNode node)
        {
            TreeNodeCollection nodes = node.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeNode child in nodes)
                {
                    if (child.ForeColor != Color.Gray)
                    {
                        child.Checked = node.Checked;
                        child.ForeColor = Color.Black;
                        if (child.Checked == true)
                        {
                            child.StateImageKey = "nomal";
                        }
                    }
                    CheckChild(child);
                }
            }
        }

        //选择一个子节点父节点变色
        private void CheckSelect(TreeNode node)
        {
            if (node.Parent != null)
            {
                foreach (TreeNode brother in node.Parent.Nodes)
                {
                    if (brother.Checked == false)
                    {
                        node.Parent.ForeColor = Color.Blue;
                        node.Parent.StateImageKey = "select";
                        CheckSelect(node.Parent);
                        break;
                    }
                    else
                    {
                        CheckSelect(node.Parent);
                    }
                }
            }
        }

        //全选或不选子节点父节点变回黑色
        private void CheckSelectDark(TreeNode node)
        {
            int a = 0, b = 0;
            if (node.Parent != null)
            {
                foreach (TreeNode brother in node.Parent.Nodes)
                {
                    if (brother.Checked == true)
                    {
                        a++;
                        if (a == node.Parent.Nodes.Count)
                        {
                            //node.Parent.ForeColor = System.Drawing.SystemColors.ControlText;
                            node.Parent.ForeColor = Color.Black;
                            CheckSelectDark(node.Parent);
                            a = 0;
                        }

                    }
                    else if (brother.Checked == false)
                    {
                        node.Parent.Checked = false;                     
                        //if (brother.ForeColor != System.Drawing.SystemColors.HotTrack)
                        if (brother.ForeColor != Color.Blue)
                        {
                            b++;
                        }
                        if (b == node.Parent.Nodes.Count)
                        {                                                     
                            //node.Parent.ForeColor = System.Drawing.SystemColors.ControlText;
                            node.Parent.ForeColor = Color.Black;
                            node.Parent.StateImageKey = "nomal";
                            CheckSelectDark(node.Parent);
                            b = 0;
                        }
                        CheckSelectDark(node.Parent);
                    }
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ApplyButton_Click(sender, e);
            this.Close();
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            tree.setTotalUnselected();
            if (treeView1 != null && treeView1.Nodes.Count > 0)
            {
                foreach (TreeNode node in treeView1.Nodes)
                {
                    SetTreeNodeChecked(node);
                }
            }
            this.ApplyButton.Enabled = false;
        }


        private void SetTreeNodeChecked(TreeNode tn)
        {
            if (tn == null)
            {
                return; // 若为空，则返回
            }           
            //else if (tn.Checked == false && tn.ForeColor == Color.Black)
            //{
            //    return;
            //}
            else if (tn.Nodes.Count > 0)
            {
                foreach (TreeNode item in tn.Nodes)
                {
                    SetTreeNodeChecked(item);
                }
            }
            else
            {
                if (tn.Checked == true)
                {
                    int index = (int)tn.Tag;
                    tree.setTcSelected(index);
                }
            }
        }

        /*
        public void clearTreeView()
        {
            if (treeView1 != null)
            {
                if (treeView1.Nodes.Count > 0)
                {
                    treeView1.Nodes.Clear();
                }
            }
        }
        */

        public void setNoConditionflag(bool val)
        {
            this.noConditionflag = val;
        }
        public bool getNoConditionFlag()
        {
            return this.noConditionflag;
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == Color.Gray)
            {
                e.Cancel = true;
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            treeView1.Refresh();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }

    //重写treeview避免双击
    //this.treeView1 = new System.Windows.Forms.TreeView();
    //this.treeView1 = new MyTreeView();
    public class MyTreeView : TreeView
    {

        private Brush b = null;//节点字体颜色  
        private Point p;//画CheckBox的位置  
        public MyTreeView()
        {
            this.DrawMode = TreeViewDrawMode.OwnerDrawText;//自己画文本  
        }
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            b = Brushes.Black;//默认字体为黑色  
            if (e.Node.ForeColor == Color.Blue)
            {
                p = e.Bounds.Location;//获取节点的位置  
                p.X = p.X - 12;//覆盖到默认画CheckBox的位置  
                CheckBoxRenderer.DrawCheckBox(e.Graphics, p, CheckBoxState.MixedHot);//画一个半选中的CheckBox 

                b = Brushes.Blue;
            }
            else if (e.Node.ForeColor == Color.Gray)
            {
                p = e.Bounds.Location;//获取节点的位置  
                p.X = p.X - 12;//覆盖到默认画CheckBox的位置  
                if (e.Node.Checked == true)
                {
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, p, CheckBoxState.CheckedDisabled);//画一个禁用的选中的CheckBox 
                }
                else if (e.Node.StateImageKey == "select")
                {
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, p, CheckBoxState.MixedDisabled);//画一个禁用的选中的CheckBox 
                }
                else
                {
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, p, CheckBoxState.UncheckedDisabled);//画一个禁用的选中的CheckBox 
                }
                b = Brushes.Gray;
            }

            //if ((e.State & TreeNodeStates.Focused) != 0)
            //    b = Brushes.White;//点击某节点时节点字体颜色为白色 
            e.Graphics.DrawString(e.Node.Text, this.Font, b, e.Bounds.Location);//画文本           
        }

        protected override void WndProc(ref Message m)
        {
            // Suppress WM_LBUTTONDBLCLK
            if (m.Msg == 0x203) { m.Result = IntPtr.Zero; }
            else base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }


    public class tcSelectInfo
    {
        public List<string> tcSelectInfoList { get; set; }
        public tcSelectInfo()
        {
            this.tcSelectInfoList = new List<string>();
        }
    }
}
