using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool.BLL;
using System.IO;

namespace Tool.UI
{
    public partial class OpinionSelectForm : Form
    {
        TotalOpinionInfo totalOpinionInfo;
        string path;
        bool isCreate = false;
        DAL.IFTBCommonAPI treeMemory;
        opinionSelectInfo nowOpinionSelectInfo;
        List<string> profileNameList = new List<string>();
        public void setOpinionSelectInfo()
        {
            nowOpinionSelectInfo = new opinionSelectInfo();
            if (this.menuRadioButton.Checked == true)
            {
                nowOpinionSelectInfo.TestType = this.menuRadioButton.Text;
            }
            if (this.tempRadioButton.Checked == true)
            {
                nowOpinionSelectInfo.TestType = this.tempRadioButton.Text;
            }
            if (this.LogRadioButton.Checked == true)
            {
                nowOpinionSelectInfo.OCRFlag = this.LogRadioButton.Text;
            }
            if (this.OCRRadioButton.Checked == true)
            {
                nowOpinionSelectInfo.OCRFlag = this.OCRRadioButton.Text;
            }
            nowOpinionSelectInfo.selectedTcRunManagerList = TestRuntimeAggregate.getSelectedTcRunManager();
            nowOpinionSelectInfo.selectedOpinionNameList = TestRuntimeAggregate.getSelectedOpinion();
        }

        public opinionSelectInfo getNowOpinionSelectInfo()
        {
            return nowOpinionSelectInfo;
        }

        public OpinionSelectForm(Object treeMemory)
        {
            this.treeMemory = (DAL.IFTBCommonAPI)treeMemory;
            InitializeComponent();
            isCreate = false;
            ToolTip toolTipSettings = new ToolTip();
            toolTipSettings.InitialDelay = 200;
            toolTipSettings.AutoPopDelay = 10 * 1000;
            toolTipSettings.ReshowDelay = 200;
            toolTipSettings.ShowAlways = true;
            toolTipSettings.IsBalloon = true;
            string tipOcr = "選択OCR時需要外接摄像头";
            toolTipSettings.SetToolTip(OCRRadioButton, tipOcr);
            string tipLog = "選択Log時需要连接Debug基板";
            toolTipSettings.SetToolTip(LogRadioButton, tipLog);
        }

        private void OpinionSelectForm_Load(object sender, EventArgs e)
        {
            if (isCreate == false)
            {
                this.OpinionDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                OpinionDataGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;

                path = StaticEnvironInfo.getMenuTcOpinionFullFileName();
                loadOpinionInfo();
                isCreate = true;
            }
            string modelPath = StaticEnvironInfo.getIntPutFilePath();
            if (!modelPath.Contains("Menu"))
            {
                this.tempRadioButton.Checked = true;
            }
        }

        private void loadOpinionInfo()
        {
            totalOpinionInfo = new TotalOpinionInfo();
            totalOpinionInfo.loadOpinionInfo(path);

            this.OpinionDataGridView.Rows.Clear();
            for (int count = 0; count < totalOpinionInfo.opinionList.Count; count++)
            {
                this.OpinionDataGridView.Rows.Add();
                this.OpinionDataGridView.Rows[count].Cells[1].Value = totalOpinionInfo.opinionList[count].opinionName;
                this.OpinionDataGridView.Rows[count].Cells[2].Value = totalOpinionInfo.opinionList[count].opinionDetail;
                this.OpinionDataGridView.Rows[count].Cells[3].Value = totalOpinionInfo.opinionList[count].opinionRange;
                this.OpinionDataGridView.Rows[count].HeaderCell.Value = (count + 1).ToString();
            }
            for (int i = 0; i < this.OpinionDataGridView.RowCount; i++)
            {
                this.OpinionDataGridView.Rows[i].Cells[0].Value = "true";//设置全选
            }
        }

        private void menuRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(this.menuRadioButton.Checked == true)
            {
                path = StaticEnvironInfo.getMenuTcOpinionFullFileName();
                loadOpinionInfo();
            }
        }

        private void tempRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.tempRadioButton.Checked == true)
            {
                path = StaticEnvironInfo.getTempTcOpinionFullFileName();
                loadOpinionInfo();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //clear select opinion and tcmanager
            TestRuntimeAggregate.clearSelectedOpinion();
            TestRuntimeAggregate.clearSelectedTcRunManager();
            //set select opinion and tcmanager
            for (int i = 0; i < OpinionDataGridView.RowCount; i++)
            {
                if((bool)this.OpinionDataGridView.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    TestRuntimeAggregate.addSelectedOpinion(totalOpinionInfo.opinionList[i].opinionName,
                                                            totalOpinionInfo.opinionList[i].opinionDetail,
                                                            totalOpinionInfo.opinionList[i].opinionRange,
                                                            totalOpinionInfo.opinionList[i].opinionType
                                                            );

                    Dictionary<string, List<string>>.KeyCollection keyColl = totalOpinionInfo.tcRunManagerShip.Keys;
                    foreach(string tcMangerName in keyColl)
                    {
                        if(totalOpinionInfo.tcRunManagerShip[tcMangerName].Contains(totalOpinionInfo.opinionList[i].opinionName))
                        {
                            TestRuntimeAggregate.setSelectedTcRunManager(tcMangerName, totalOpinionInfo.opinionList[i].opinionName);
                        }
                    }
                }
            }
            StaticEnvironInfo.setMenuItemTestFlag(menuRadioButton.Checked);
            StaticEnvironInfo.setOcrUsedFlag(OCRRadioButton.Checked);

            if(this.ignoreCaseCheckBox.Visible == true)
            {
                StaticEnvironInfo.setIgnoreCase(this.ignoreCaseCheckBox.Checked);
            }
            setOpinionSelectInfo();
            this.Close();
        }

        //to remove
        private void LogRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(this.LogRadioButton.Checked == true)
            {
                StaticEnvironInfo.setOcrUsedFlag(false);
            }
        }

        //to remove
        private void OCRRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(this.OCRRadioButton.Checked == true)
            {
                StaticEnvironInfo.setOcrUsedFlag(true);
            }
        }

        private void OpinionDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.OpinionDataGridView.IsCurrentCellDirty)
            {
                this.OpinionDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void OpinionDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != -1 && !OpinionDataGridView.Rows[e.RowIndex].IsNewRow)
            {
                if (this.OpinionDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString() == "FtbStringChecker")
                {
                    if ((bool)this.OpinionDataGridView.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                    {
                        this.ignoreCaseCheckBox.Visible = true;
                    }
                    else
                    {
                        this.ignoreCaseCheckBox.Visible = false;
                    }
                }
            }
            //show screenTest
            if (e.RowIndex >= 0 && e.RowIndex != -1 && !OpinionDataGridView.Rows[e.RowIndex].IsNewRow)
            {
                ScreenTest screenTest = new ScreenTest(treeMemory);
                if (this.OpinionDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString() == "FtbScreenItemListChecker")
                {
                    if ((bool)this.OpinionDataGridView.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                    {
                        List<string> list = TestRuntimeAggregate.getTreeMemory().getModelAndCountrySelectInfoList();
                        if (list.Count() == 0)
                        {
                            screenTest.OpinionSelectFormFlag = true;
                            screenTest.ShowDialog();
                        }
                    }
                }
            }
        }
    }

    public class opinionSelectInfo
    {
        public string TestType { get; set; }
        public string OCRFlag { get; set; }
        public List<string> selectedTcRunManagerList { get; set; }
        public List<string> selectedOpinionNameList { get; set; }
        public opinionSelectInfo()
        {
            this.TestType = TestType;
            this.OCRFlag = OCRFlag;
            this.selectedTcRunManagerList = selectedTcRunManagerList;
            this.selectedOpinionNameList = selectedOpinionNameList;
        }
    }
}
