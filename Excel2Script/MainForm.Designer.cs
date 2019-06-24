namespace FTBExcel2Script
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("画面タイトル");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("リスト内容");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("メニュー有効性");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("値入力画面");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("情報表示画面");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("TC1_デフォルト値", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode29});
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("TC2_オプション設定", new System.Windows.Forms.TreeNode[] {
            treeNode31,
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35});
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboBox_Series = new System.Windows.Forms.ComboBox();
            this.TextBox_FTBFile = new System.Windows.Forms.TextBox();
            this.TextBox_Output = new System.Windows.Forms.TextBox();
            this.BTN_SelFTB = new System.Windows.Forms.Button();
            this.BTN_SelOutput = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_LogMessage = new System.Windows.Forms.Label();
            this.BTN_Start = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ComboBox_Model = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboBox_Country = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ComboBox_Area = new System.Windows.Forms.ComboBox();
            this.ComboBox_Language = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BTN_ReadFTBInfo = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BTN_SelModelRelate = new System.Windows.Forms.Button();
            this.TextBox_ModelRelateFile = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TreeView_SelTestView = new System.Windows.Forms.TreeView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.BTN_Stop = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "機種";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ファイルパス";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "出力フォルダ";
            // 
            // ComboBox_Series
            // 
            this.ComboBox_Series.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Series.FormattingEnabled = true;
            this.ComboBox_Series.Location = new System.Drawing.Point(67, 25);
            this.ComboBox_Series.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_Series.Name = "ComboBox_Series";
            this.ComboBox_Series.Size = new System.Drawing.Size(128, 20);
            this.ComboBox_Series.TabIndex = 1;
            this.ComboBox_Series.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Series_SelectedIndexChanged);
            // 
            // TextBox_FTBFile
            // 
            this.TextBox_FTBFile.Location = new System.Drawing.Point(67, 50);
            this.TextBox_FTBFile.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_FTBFile.Name = "TextBox_FTBFile";
            this.TextBox_FTBFile.Size = new System.Drawing.Size(457, 19);
            this.TextBox_FTBFile.TabIndex = 3;
            this.TextBox_FTBFile.TextChanged += new System.EventHandler(this.TextBox_FTBFile_TextChanged);
            // 
            // TextBox_Output
            // 
            this.TextBox_Output.Location = new System.Drawing.Point(67, 79);
            this.TextBox_Output.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_Output.Name = "TextBox_Output";
            this.TextBox_Output.Size = new System.Drawing.Size(222, 19);
            this.TextBox_Output.TabIndex = 15;
            this.TextBox_Output.TextChanged += new System.EventHandler(this.TextBox_Output_TextChanged);
            // 
            // BTN_SelFTB
            // 
            this.BTN_SelFTB.Location = new System.Drawing.Point(527, 46);
            this.BTN_SelFTB.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_SelFTB.Name = "BTN_SelFTB";
            this.BTN_SelFTB.Size = new System.Drawing.Size(40, 24);
            this.BTN_SelFTB.TabIndex = 4;
            this.BTN_SelFTB.Text = ">>";
            this.BTN_SelFTB.UseVisualStyleBackColor = true;
            this.BTN_SelFTB.Click += new System.EventHandler(this.BTN_SelFTB_Click);
            // 
            // BTN_SelOutput
            // 
            this.BTN_SelOutput.Location = new System.Drawing.Point(292, 75);
            this.BTN_SelOutput.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_SelOutput.Name = "BTN_SelOutput";
            this.BTN_SelOutput.Size = new System.Drawing.Size(40, 24);
            this.BTN_SelOutput.TabIndex = 16;
            this.BTN_SelOutput.Text = ">>";
            this.BTN_SelOutput.UseVisualStyleBackColor = true;
            this.BTN_SelOutput.Click += new System.EventHandler(this.BTN_SelOutput_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_LogMessage);
            this.groupBox1.Location = new System.Drawing.Point(16, 410);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(346, 73);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LogMessage";
            // 
            // label_LogMessage
            // 
            this.label_LogMessage.AutoSize = true;
            this.label_LogMessage.Location = new System.Drawing.Point(11, 24);
            this.label_LogMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_LogMessage.Name = "label_LogMessage";
            this.label_LogMessage.Size = new System.Drawing.Size(68, 12);
            this.label_LogMessage.TabIndex = 0;
            this.label_LogMessage.Text = "LogMessage";
            // 
            // BTN_Start
            // 
            this.BTN_Start.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BTN_Start.Location = new System.Drawing.Point(388, 416);
            this.BTN_Start.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(88, 62);
            this.BTN_Start.TabIndex = 17;
            this.BTN_Start.Text = "開始";
            this.BTN_Start.UseVisualStyleBackColor = true;
            this.BTN_Start.Click += new System.EventHandler(this.BTN_Start_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "モデル";
            // 
            // ComboBox_Model
            // 
            this.ComboBox_Model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Model.FormattingEnabled = true;
            this.ComboBox_Model.Location = new System.Drawing.Point(67, 28);
            this.ComboBox_Model.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_Model.Name = "ComboBox_Model";
            this.ComboBox_Model.Size = new System.Drawing.Size(136, 20);
            this.ComboBox_Model.TabIndex = 6;
            this.ComboBox_Model.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Model_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 30);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "国別";
            // 
            // ComboBox_Country
            // 
            this.ComboBox_Country.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Country.DropDownWidth = 180;
            this.ComboBox_Country.FormattingEnabled = true;
            this.ComboBox_Country.Location = new System.Drawing.Point(280, 28);
            this.ComboBox_Country.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_Country.Name = "ComboBox_Country";
            this.ComboBox_Country.Size = new System.Drawing.Size(136, 20);
            this.ComboBox_Country.TabIndex = 10;
            this.ComboBox_Country.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Country_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 62);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "地域";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 62);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "言語";
            // 
            // ComboBox_Area
            // 
            this.ComboBox_Area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Area.FormattingEnabled = true;
            this.ComboBox_Area.Location = new System.Drawing.Point(67, 59);
            this.ComboBox_Area.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_Area.Name = "ComboBox_Area";
            this.ComboBox_Area.Size = new System.Drawing.Size(136, 20);
            this.ComboBox_Area.TabIndex = 8;
            this.ComboBox_Area.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Area_SelectedIndexChanged);
            // 
            // ComboBox_Language
            // 
            this.ComboBox_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Language.FormattingEnabled = true;
            this.ComboBox_Language.Location = new System.Drawing.Point(280, 59);
            this.ComboBox_Language.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_Language.Name = "ComboBox_Language";
            this.ComboBox_Language.Size = new System.Drawing.Size(136, 20);
            this.ComboBox_Language.TabIndex = 12;
            this.ComboBox_Language.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Language_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BTN_ReadFTBInfo);
            this.groupBox2.Controls.Add(this.ComboBox_Country);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.ComboBox_Model);
            this.groupBox2.Controls.Add(this.ComboBox_Area);
            this.groupBox2.Controls.Add(this.ComboBox_Language);
            this.groupBox2.Location = new System.Drawing.Point(16, 114);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(584, 94);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "テスト情報選択";
            // 
            // BTN_ReadFTBInfo
            // 
            this.BTN_ReadFTBInfo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BTN_ReadFTBInfo.Location = new System.Drawing.Point(454, 25);
            this.BTN_ReadFTBInfo.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_ReadFTBInfo.Name = "BTN_ReadFTBInfo";
            this.BTN_ReadFTBInfo.Size = new System.Drawing.Size(90, 48);
            this.BTN_ReadFTBInfo.TabIndex = 13;
            this.BTN_ReadFTBInfo.Text = "情報読む";
            this.BTN_ReadFTBInfo.UseVisualStyleBackColor = true;
            this.BTN_ReadFTBInfo.Click += new System.EventHandler(this.BTN_ReadFTBInfo_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BTN_SelFTB);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.ComboBox_Series);
            this.groupBox3.Controls.Add(this.TextBox_FTBFile);
            this.groupBox3.Location = new System.Drawing.Point(16, 14);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(584, 83);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FTB選択";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BTN_SelOutput);
            this.groupBox4.Controls.Add(this.TextBox_Output);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(16, 224);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(346, 159);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "出力選択";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BTN_SelModelRelate);
            this.groupBox5.Controls.Add(this.TextBox_ModelRelateFile);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(16, 224);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(346, 67);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "modelRelate選択";
            this.groupBox5.Visible = false;
            // 
            // BTN_SelModelRelate
            // 
            this.BTN_SelModelRelate.Location = new System.Drawing.Point(292, 24);
            this.BTN_SelModelRelate.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_SelModelRelate.Name = "BTN_SelModelRelate";
            this.BTN_SelModelRelate.Size = new System.Drawing.Size(40, 24);
            this.BTN_SelModelRelate.TabIndex = 16;
            this.BTN_SelModelRelate.Text = ">>";
            this.BTN_SelModelRelate.UseVisualStyleBackColor = true;
            this.BTN_SelModelRelate.Click += new System.EventHandler(this.BTN_SelModelRelate_Click);
            // 
            // TextBox_ModelRelateFile
            // 
            this.TextBox_ModelRelateFile.Location = new System.Drawing.Point(67, 28);
            this.TextBox_ModelRelateFile.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_ModelRelateFile.Name = "TextBox_ModelRelateFile";
            this.TextBox_ModelRelateFile.Size = new System.Drawing.Size(222, 19);
            this.TextBox_ModelRelateFile.TabIndex = 15;
            this.TextBox_ModelRelateFile.TextChanged += new System.EventHandler(this.TextBox_ModelRelateFile_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "ファイルパス";
            // 
            // TreeView_SelTestView
            // 
            this.TreeView_SelTestView.CheckBoxes = true;
            this.TreeView_SelTestView.Location = new System.Drawing.Point(13, 23);
            this.TreeView_SelTestView.Margin = new System.Windows.Forms.Padding(2);
            this.TreeView_SelTestView.Name = "TreeView_SelTestView";
            treeNode25.Name = "Node3";
            treeNode25.Text = "画面タイトル";
            treeNode26.Name = "Node4";
            treeNode26.Text = "リスト内容";
            treeNode27.Name = "Node5";
            treeNode27.Text = "メニュー有効性";
            treeNode28.Name = "Node6";
            treeNode28.Text = "値入力画面";
            treeNode29.Name = "Node7";
            treeNode29.Text = "情報表示画面";
            treeNode30.Checked = true;
            treeNode30.Name = "CheckBox_TC_Default";
            treeNode30.Text = "TC1_デフォルト値";
            treeNode31.Name = "Node8";
            treeNode31.Text = "Node8";
            treeNode32.Name = "Node9";
            treeNode32.Text = "Node9";
            treeNode33.Name = "Node10";
            treeNode33.Text = "Node10";
            treeNode34.Name = "Node11";
            treeNode34.Text = "Node11";
            treeNode35.Name = "Node12";
            treeNode35.Text = "Node12";
            treeNode36.Name = "CheckBox_TC_OptionSet";
            treeNode36.Text = "TC2_オプション設定";
            this.TreeView_SelTestView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode30,
            treeNode36});
            this.TreeView_SelTestView.Size = new System.Drawing.Size(204, 125);
            this.TreeView_SelTestView.TabIndex = 18;
            this.TreeView_SelTestView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_SelTestView_AfterCheck);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.TreeView_SelTestView);
            this.groupBox8.Location = new System.Drawing.Point(375, 224);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox8.Size = new System.Drawing.Size(226, 159);
            this.groupBox8.TabIndex = 19;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "TC観点の選択";
            // 
            // BTN_Stop
            // 
            this.BTN_Stop.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BTN_Stop.Location = new System.Drawing.Point(502, 416);
            this.BTN_Stop.Margin = new System.Windows.Forms.Padding(2);
            this.BTN_Stop.Name = "BTN_Stop";
            this.BTN_Stop.Size = new System.Drawing.Size(88, 62);
            this.BTN_Stop.TabIndex = 17;
            this.BTN_Stop.Text = "取消";
            this.BTN_Stop.UseVisualStyleBackColor = true;
            this.BTN_Stop.Click += new System.EventHandler(this.BTN_Stop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 508);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.BTN_Stop);
            this.Controls.Add(this.BTN_Start);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "FTB Excel To Test Script";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboBox_Series;
        private System.Windows.Forms.TextBox TextBox_FTBFile;
        private System.Windows.Forms.TextBox TextBox_Output;
        private System.Windows.Forms.Button BTN_SelFTB;
        private System.Windows.Forms.Button BTN_SelOutput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BTN_Start;
        private System.Windows.Forms.Label label_LogMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ComboBox_Model;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ComboBox_Country;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ComboBox_Area;
        private System.Windows.Forms.ComboBox ComboBox_Language;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BTN_ReadFTBInfo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button BTN_SelModelRelate;
        private System.Windows.Forms.TextBox TextBox_ModelRelateFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TreeView TreeView_SelTestView;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button BTN_Stop;
    }
}

