namespace Tool.UI
{
    partial class FTBTestForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SaveProfileButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.BvboardRadioButton = new System.Windows.Forms.RadioButton();
            this.COMPortRadioButton = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.SerialPortComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.opinionSelectButton = new System.Windows.Forms.Button();
            this.ConditionButton = new System.Windows.Forms.Button();
            this.TCSelextButton = new System.Windows.Forms.Button();
            this.MachineTypeComboBox = new System.Windows.Forms.ComboBox();
            this.LanguageComboBox = new System.Windows.Forms.ComboBox();
            this.CountryComboBox = new System.Windows.Forms.ComboBox();
            this.ContinentComboBox = new System.Windows.Forms.ComboBox();
            this.ModelComboBox = new System.Windows.Forms.ComboBox();
            this.FTBFileTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.JsonFileBrowseButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CorrespondButton = new System.Windows.Forms.Button();
            this.jsonToFtb = new System.Windows.Forms.Button();
            this.scanCalibrateButton = new System.Windows.Forms.Button();
            this.screenCalibrateButton = new System.Windows.Forms.Button();
            this.conditionCalibrateButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.creatReportButton = new System.Windows.Forms.Button();
            this.ExoprtButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(549, 581);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ExoprtButton);
            this.tabPage1.Controls.Add(this.SaveProfileButton);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.progressBar2);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.opinionSelectButton);
            this.tabPage1.Controls.Add(this.ConditionButton);
            this.tabPage1.Controls.Add(this.TCSelextButton);
            this.tabPage1.Controls.Add(this.MachineTypeComboBox);
            this.tabPage1.Controls.Add(this.LanguageComboBox);
            this.tabPage1.Controls.Add(this.CountryComboBox);
            this.tabPage1.Controls.Add(this.ContinentComboBox);
            this.tabPage1.Controls.Add(this.ModelComboBox);
            this.tabPage1.Controls.Add(this.FTBFileTextBox);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.JsonFileBrowseButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(541, 555);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "FTB自動テストツール";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SaveProfileButton
            // 
            this.SaveProfileButton.Location = new System.Drawing.Point(435, 34);
            this.SaveProfileButton.Name = "SaveProfileButton";
            this.SaveProfileButton.Size = new System.Drawing.Size(85, 29);
            this.SaveProfileButton.TabIndex = 15;
            this.SaveProfileButton.Text = "現在配置保存";
            this.SaveProfileButton.UseVisualStyleBackColor = true;
            this.SaveProfileButton.Click += new System.EventHandler(this.SaveProfileButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.PortLabel);
            this.groupBox1.Controls.Add(this.BvboardRadioButton);
            this.groupBox1.Controls.Add(this.COMPortRadioButton);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.SerialPortComboBox);
            this.groupBox1.Location = new System.Drawing.Point(21, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 102);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "To Connect：";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox2.Location = new System.Drawing.Point(151, 58);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(233, 22);
            this.textBox2.TabIndex = 15;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.PortLabel.Location = new System.Drawing.Point(100, 61);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(42, 15);
            this.PortLabel.TabIndex = 13;
            this.PortLabel.Text = "Port：";
            // 
            // BvboardRadioButton
            // 
            this.BvboardRadioButton.AutoSize = true;
            this.BvboardRadioButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BvboardRadioButton.Location = new System.Drawing.Point(11, 62);
            this.BvboardRadioButton.Name = "BvboardRadioButton";
            this.BvboardRadioButton.Size = new System.Drawing.Size(72, 16);
            this.BvboardRadioButton.TabIndex = 1;
            this.BvboardRadioButton.TabStop = true;
            this.BvboardRadioButton.Text = "Bvboard";
            this.BvboardRadioButton.UseVisualStyleBackColor = true;
            // 
            // COMPortRadioButton
            // 
            this.COMPortRadioButton.AutoSize = true;
            this.COMPortRadioButton.Checked = true;
            this.COMPortRadioButton.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.COMPortRadioButton.Location = new System.Drawing.Point(11, 27);
            this.COMPortRadioButton.Name = "COMPortRadioButton";
            this.COMPortRadioButton.Size = new System.Drawing.Size(81, 16);
            this.COMPortRadioButton.TabIndex = 0;
            this.COMPortRadioButton.TabStop = true;
            this.COMPortRadioButton.Text = "COM Port";
            this.COMPortRadioButton.UseVisualStyleBackColor = true;
            this.COMPortRadioButton.CheckedChanged += new System.EventHandler(this.comPortChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label11.Location = new System.Drawing.Point(98, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 15);
            this.label11.TabIndex = 8;
            this.label11.Text = "COM：";
            // 
            // SerialPortComboBox
            // 
            this.SerialPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialPortComboBox.FormattingEnabled = true;
            this.SerialPortComboBox.Location = new System.Drawing.Point(151, 27);
            this.SerialPortComboBox.Name = "SerialPortComboBox";
            this.SerialPortComboBox.Size = new System.Drawing.Size(233, 20);
            this.SerialPortComboBox.TabIndex = 7;
            this.SerialPortComboBox.SelectedIndexChanged += new System.EventHandler(this.SerialPortComboBox_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(18, 512);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 23);
            this.label13.TabIndex = 13;
            this.label13.Text = "Partial:";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(18, 469);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 23);
            this.label12.TabIndex = 12;
            this.label12.Text = "Total:";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(135, 469);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(343, 23);
            this.progressBar2.TabIndex = 11;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(135, 512);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(343, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 10;
            // 
            // opinionSelectButton
            // 
            this.opinionSelectButton.Enabled = false;
            this.opinionSelectButton.Location = new System.Drawing.Point(435, 367);
            this.opinionSelectButton.Name = "opinionSelectButton";
            this.opinionSelectButton.Size = new System.Drawing.Size(95, 28);
            this.opinionSelectButton.TabIndex = 9;
            this.opinionSelectButton.Text = "Opinion Select";
            this.opinionSelectButton.UseVisualStyleBackColor = true;
            this.opinionSelectButton.Click += new System.EventHandler(this.opinionSelectButton_Click);
            // 
            // ConditionButton
            // 
            this.ConditionButton.Enabled = false;
            this.ConditionButton.Location = new System.Drawing.Point(21, 367);
            this.ConditionButton.Name = "ConditionButton";
            this.ConditionButton.Size = new System.Drawing.Size(101, 28);
            this.ConditionButton.TabIndex = 6;
            this.ConditionButton.Text = "Condition Select";
            this.ConditionButton.UseVisualStyleBackColor = true;
            this.ConditionButton.Click += new System.EventHandler(this.ConditionButton_Click);
            // 
            // TCSelextButton
            // 
            this.TCSelextButton.Enabled = false;
            this.TCSelextButton.Location = new System.Drawing.Point(231, 367);
            this.TCSelextButton.Name = "TCSelextButton";
            this.TCSelextButton.Size = new System.Drawing.Size(94, 28);
            this.TCSelextButton.TabIndex = 5;
            this.TCSelextButton.Text = "TC Select";
            this.TCSelextButton.UseVisualStyleBackColor = true;
            this.TCSelextButton.Click += new System.EventHandler(this.TCSelextButton_Click);
            // 
            // MachineTypeComboBox
            // 
            this.MachineTypeComboBox.FormattingEnabled = true;
            this.MachineTypeComboBox.Location = new System.Drawing.Point(279, 296);
            this.MachineTypeComboBox.Name = "MachineTypeComboBox";
            this.MachineTypeComboBox.Size = new System.Drawing.Size(79, 20);
            this.MachineTypeComboBox.TabIndex = 4;
            this.MachineTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.MachineTypeComboBox_SelectedIndexChanged);
            // 
            // LanguageComboBox
            // 
            this.LanguageComboBox.FormattingEnabled = true;
            this.LanguageComboBox.Location = new System.Drawing.Point(445, 296);
            this.LanguageComboBox.Name = "LanguageComboBox";
            this.LanguageComboBox.Size = new System.Drawing.Size(85, 20);
            this.LanguageComboBox.TabIndex = 3;
            this.LanguageComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguageComboBox_SelectedIndexChanged);
            // 
            // CountryComboBox
            // 
            this.CountryComboBox.FormattingEnabled = true;
            this.CountryComboBox.Location = new System.Drawing.Point(90, 296);
            this.CountryComboBox.Name = "CountryComboBox";
            this.CountryComboBox.Size = new System.Drawing.Size(73, 20);
            this.CountryComboBox.TabIndex = 3;
            this.CountryComboBox.SelectedIndexChanged += new System.EventHandler(this.CountryComboBox_SelectedIndexChanged);
            // 
            // ContinentComboBox
            // 
            this.ContinentComboBox.FormattingEnabled = true;
            this.ContinentComboBox.Location = new System.Drawing.Point(341, 236);
            this.ContinentComboBox.Name = "ContinentComboBox";
            this.ContinentComboBox.Size = new System.Drawing.Size(98, 20);
            this.ContinentComboBox.TabIndex = 3;
            this.ContinentComboBox.SelectedIndexChanged += new System.EventHandler(this.ContinentComboBox_SelectedIndexChanged);
            // 
            // ModelComboBox
            // 
            this.ModelComboBox.FormattingEnabled = true;
            this.ModelComboBox.Location = new System.Drawing.Point(119, 236);
            this.ModelComboBox.Name = "ModelComboBox";
            this.ModelComboBox.Size = new System.Drawing.Size(103, 20);
            this.ModelComboBox.TabIndex = 3;
            this.ModelComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelComboBox_SelectedIndexChanged);
            // 
            // FTBFileTextBox
            // 
            this.FTBFileTextBox.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FTBFileTextBox.Location = new System.Drawing.Point(119, 176);
            this.FTBFileTextBox.Name = "FTBFileTextBox";
            this.FTBFileTextBox.Size = new System.Drawing.Size(286, 22);
            this.FTBFileTextBox.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(169, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Machine Type：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(364, 296);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 1;
            this.label10.Text = "Language：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(18, 296);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "Country：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(247, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "Continent：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(18, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "Model：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(18, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "FTBJsonFile：";
            // 
            // JsonFileBrowseButton
            // 
            this.JsonFileBrowseButton.Location = new System.Drawing.Point(411, 175);
            this.JsonFileBrowseButton.Name = "JsonFileBrowseButton";
            this.JsonFileBrowseButton.Size = new System.Drawing.Size(119, 26);
            this.JsonFileBrowseButton.TabIndex = 0;
            this.JsonFileBrowseButton.Text = "SelectTestFTBJson";
            this.JsonFileBrowseButton.UseVisualStyleBackColor = true;
            this.JsonFileBrowseButton.Click += new System.EventHandler(this.FTBFileBrowseButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CorrespondButton);
            this.tabPage2.Controls.Add(this.jsonToFtb);
            this.tabPage2.Controls.Add(this.scanCalibrateButton);
            this.tabPage2.Controls.Add(this.screenCalibrateButton);
            this.tabPage2.Controls.Add(this.conditionCalibrateButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(541, 555);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tools";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CorrespondButton
            // 
            this.CorrespondButton.Enabled = false;
            this.CorrespondButton.Location = new System.Drawing.Point(171, 20);
            this.CorrespondButton.Name = "CorrespondButton";
            this.CorrespondButton.Size = new System.Drawing.Size(182, 36);
            this.CorrespondButton.TabIndex = 3;
            this.CorrespondButton.Text = "USA_UK_English_Correspond";
            this.CorrespondButton.UseVisualStyleBackColor = true;
            this.CorrespondButton.Click += new System.EventHandler(this.CorrespondButton_Click);
            // 
            // jsonToFtb
            // 
            this.jsonToFtb.Location = new System.Drawing.Point(20, 150);
            this.jsonToFtb.Name = "jsonToFtb";
            this.jsonToFtb.Size = new System.Drawing.Size(113, 36);
            this.jsonToFtb.TabIndex = 2;
            this.jsonToFtb.Text = "JsonToFtb";
            this.jsonToFtb.UseVisualStyleBackColor = true;
            this.jsonToFtb.Click += new System.EventHandler(this.jsonToFtb_Click);
            // 
            // scanCalibrateButton
            // 
            this.scanCalibrateButton.Location = new System.Drawing.Point(171, 84);
            this.scanCalibrateButton.Name = "scanCalibrateButton";
            this.scanCalibrateButton.Size = new System.Drawing.Size(182, 39);
            this.scanCalibrateButton.TabIndex = 2;
            this.scanCalibrateButton.Text = "ScanCalibrate";
            this.scanCalibrateButton.UseVisualStyleBackColor = true;
            this.scanCalibrateButton.Click += new System.EventHandler(this.scanCalibrateButton_Click);
            // 
            // screenCalibrateButton
            // 
            this.screenCalibrateButton.Enabled = false;
            this.screenCalibrateButton.Location = new System.Drawing.Point(20, 85);
            this.screenCalibrateButton.Name = "screenCalibrateButton";
            this.screenCalibrateButton.Size = new System.Drawing.Size(113, 36);
            this.screenCalibrateButton.TabIndex = 1;
            this.screenCalibrateButton.Text = "Screen";
            this.screenCalibrateButton.UseVisualStyleBackColor = true;
            this.screenCalibrateButton.Click += new System.EventHandler(this.screenCalibrateButton_Click);
            // 
            // conditionCalibrateButton
            // 
            this.conditionCalibrateButton.Enabled = false;
            this.conditionCalibrateButton.Location = new System.Drawing.Point(20, 20);
            this.conditionCalibrateButton.Name = "conditionCalibrateButton";
            this.conditionCalibrateButton.Size = new System.Drawing.Size(113, 36);
            this.conditionCalibrateButton.TabIndex = 0;
            this.conditionCalibrateButton.Text = "ConditionCalibrate";
            this.conditionCalibrateButton.UseVisualStyleBackColor = true;
            this.conditionCalibrateButton.Click += new System.EventHandler(this.conditionCalibrateButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(220, 603);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "開始する";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(541, 603);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // creatReportButton
            // 
            this.creatReportButton.Enabled = false;
            this.creatReportButton.Location = new System.Drawing.Point(349, 603);
            this.creatReportButton.Name = "creatReportButton";
            this.creatReportButton.Size = new System.Drawing.Size(72, 23);
            this.creatReportButton.TabIndex = 4;
            this.creatReportButton.Text = "停止する";
            this.creatReportButton.UseVisualStyleBackColor = true;
            this.creatReportButton.Click += new System.EventHandler(this.creatReportButton_Click);
            // 
            // ExoprtButton
            // 
            this.ExoprtButton.Location = new System.Drawing.Point(435, 82);
            this.ExoprtButton.Name = "ExoprtButton";
            this.ExoprtButton.Size = new System.Drawing.Size(85, 29);
            this.ExoprtButton.TabIndex = 16;
            this.ExoprtButton.Text = "配置Import";
            this.ExoprtButton.UseVisualStyleBackColor = true;
            this.ExoprtButton.Click += new System.EventHandler(this.ExoprtButton_Click);
            // 
            // FTBTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 638);
            this.Controls.Add(this.creatReportButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.tabControl1);
            this.Name = "FTBTestForm";
            this.Load += new System.EventHandler(this.FTBTestForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox FTBFileTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button JsonFileBrowseButton;
        private System.Windows.Forms.ComboBox LanguageComboBox;
        private System.Windows.Forms.ComboBox CountryComboBox;
        private System.Windows.Forms.ComboBox ContinentComboBox;
        private System.Windows.Forms.ComboBox ModelComboBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox MachineTypeComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button TCSelextButton;
        //private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ConditionButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox SerialPortComboBox;
        private System.Windows.Forms.Button opinionSelectButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button conditionCalibrateButton;
        private System.Windows.Forms.Button screenCalibrateButton;
        private System.Windows.Forms.Button jsonToFtb;
        private System.Windows.Forms.Button scanCalibrateButton;
        private System.Windows.Forms.Button creatReportButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button CorrespondButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton BvboardRadioButton;
        private System.Windows.Forms.RadioButton COMPortRadioButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Button SaveProfileButton;
        private System.Windows.Forms.Button ExoprtButton;
    }
}
