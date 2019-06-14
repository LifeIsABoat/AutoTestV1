using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool.DAL;

namespace Tool.UI
{
    public partial class ConditionSelect : Form
    {
        private IFTBCommonAPI tree;
        List<string> noCondition = new List<string>();
        private const int _INVALIDTCINDEX = -1;

        //ConditionInput ConditionInput_Form;
        private List<int> hardwareDeviceConditionIndexList;
        private bool hasLoded;

        public ConditionSelect()
        {
            InitializeComponent();
            //ConditionInput_Form = new ConditionInput();
            hardwareDeviceConditionIndexList = new List<int>();
            hasLoded = false;
            ToolTip toolTipSettings = new ToolTip();
            toolTipSettings.InitialDelay = 200;
            toolTipSettings.AutoPopDelay = 10 * 1000;
            toolTipSettings.ReshowDelay = 200;
            toolTipSettings.ShowAlways = true;
            toolTipSettings.IsBalloon = true;
            string tipInside = "内部Condition：本体的某些项目设定完了后会导致其他Option或者画面Appear的项目,导致其他Option或者画面DisAppear的项目无需标定";
            toolTipSettings.SetToolTip(InsideCheckBox, tipInside);
            string tipNoCondition = "NoCondition：不需要任何前置条件就能测试的项目";
            toolTipSettings.SetToolTip(NoConditionCheckBox, tipNoCondition);
            string tipHardwareDevice = "外部Conditions：需要手动设定或者提供外部硬件支持才能继续测试的项目";
            toolTipSettings.SetToolTip(hardwareDeviceCheckBox, tipHardwareDevice);
        }

        private void NoConditionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        public bool GetNoConditionCheckBoxState()
        {
            return this.NoConditionCheckBox.Checked;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //clear all condition select
            tree.setTotalContidionUnselect();
            //set condition selected
            if (this.InsideCheckBox.Checked == true)
            {
                //set OptionSetting condition selected
                tree.setTotalOptionSettingSelected();
                noCondition.Add(this.InsideCheckBox.Text);
            }
            if (this.NoConditionCheckBox.Checked == true)
            {
                //set no condition selected
                tree.setTotalNoConditionSelected();
                noCondition.Add(this.NoConditionCheckBox.Text);
            }
            if (this.hardwareDeviceCheckBox.Checked == true)
            {
                for (int i = 0; i < conditionCheckedListBox.Items.Count; i++)
                {

                    if (conditionCheckedListBox.GetItemChecked(i))
                    {
                        tree.setHardwareDeviceSelected(hardwareDeviceConditionIndexList[i]);
                    }
                }
            }
            this.Close();
        }

        private void InsideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InsideCheckBox.Checked == true)
            {
                if (!System.IO.File.Exists(StaticEnvironInfo.getConditionCalibrationFullFileName() + tree.getSelectModel() + "-" + tree.getSelectCountry() + ".txt"))
                {
                    MessageBox.Show("你可能还没有事先标定好condition,请进行事先标定!");
                }
            }
        }

        public void set_tree(object t)
        {
            this.tree = (IFTBCommonAPI)t;
        }

        public void setUpdateFlag(bool isUpdate)
        {
            hasLoded = isUpdate;
        }
        private void Condition_Load(object sender, EventArgs e)
        {
            if (hasLoded == false)
            {
                loadhardwareDeviceCondition();
                hasLoded = true;
            }
            if (tree.getStringBuff() == null) { /*donothing*/ }

            if (tree.getStringBuff() != null && tree.getStringBuff() == "NoCondition")
            {
                this.NoConditionCheckBox.Checked = true;
            }
        }

        private void conditionCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadhardwareDeviceCondition()
        {
            List<string> AllConditionList = tree.getTotalConditionList();
            if (System.IO.File.Exists(StaticEnvironInfo.getConditionCalibrationFullFileName() + tree.getSelectModel() + "-" + tree.getSelectCountry() + ".txt") == true)
            {
                ConditionCalibration conditionCalibration = new ConditionCalibration();
                List<ConditionInfo> conditionList = conditionCalibration.loadConditionInfo(StaticEnvironInfo.getConditionCalibrationFullFileName() + tree.getSelectModel() + "-" + tree.getSelectCountry() + ".txt");
                if (conditionList.Count != AllConditionList.Count)
                {
                    MessageBox.Show("select file wrong!");
                }
                else
                {
                    setCondition(conditionList);
                }
            }

            hardwareDeviceConditionIndexList.Clear();
            this.conditionCheckedListBox.Items.Clear();
            for (int index = 0; index < AllConditionList.Count; index++)
            {
                if (tree.getConditionType(index + 1) == ConditionType.HardwareDevice)
                {
                    hardwareDeviceConditionIndexList.Add(index + 1);
                    this.conditionCheckedListBox.Items.Add(AllConditionList[index]);
                }
            }
        }
        private void setCondition(List<ConditionInfo> conditionList)
        {
            //所有输入项正确，则设置condition属性
            //清除所有condition种类
            tree.setTotalContidionType();
            //清除所有condition关联的tcIndex
            tree.setTotalContidionIndex();
            //根据内容设定所有condition种类
            //内部condition再设置关联TCIndex
            for (int index = 0; index < conditionList.Count; index++)
            {
                if (conditionList[index].tcPath == "")
                {
                    tree.setTotalHardwareDevice(index + 1);
                }
                else
                {
                    //find tcIndex
                    List<int> tcIndexList = tree.getPathTcIndexList(conditionList[index].tcPath);
                    if (tcIndexList == null || tcIndexList.Count == 0)
                    {
                        string info = string.Format("这条condition：<{0}>" + 
                            Environment.NewLine + " 标定好像有点问题,要不你再看看？" + 
                            Environment.NewLine + "标定信息：<{1}>",
                            conditionList[index].condition, conditionList[index].tcPath);
                       MessageBox.Show(info,"警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        tree.setTotalOptionSetting(index + 1, tcIndexList[0]);
                    }
                }
            }
        }

        private void selsecButton_Click(object sender, EventArgs e)
        {
            bool value;
            if(this.selsecButton.Text == "SelectAll")
            {
                value = true;
                this.selsecButton.Text = "UnselectAll";
            }
            else
            {
                value = false;
                this.selsecButton.Text = "SelectAll";
            }

            for (int i = 0; i < conditionCheckedListBox.Items.Count; i++)
            {
                conditionCheckedListBox.SetItemChecked(i, value);
            }
        }
    }
}
