namespace Tool.UI
{
    partial class OpinionSelectForm
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
            this.OpinionDataGridView = new System.Windows.Forms.DataGridView();
            this.opinionSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.opinionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opinionDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opinionRange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tempRadioButton = new System.Windows.Forms.RadioButton();
            this.menuRadioButton = new System.Windows.Forms.RadioButton();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OCRRadioButton = new System.Windows.Forms.RadioButton();
            this.LogRadioButton = new System.Windows.Forms.RadioButton();
            this.ignoreCaseCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.OpinionDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpinionDataGridView
            // 
            this.OpinionDataGridView.AllowUserToAddRows = false;
            this.OpinionDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpinionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OpinionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.opinionSelect,
            this.opinionName,
            this.opinionDetail,
            this.opinionRange});
            this.OpinionDataGridView.Location = new System.Drawing.Point(28, 140);
            this.OpinionDataGridView.Name = "OpinionDataGridView";
            this.OpinionDataGridView.RowHeadersWidth = 60;
            this.OpinionDataGridView.RowTemplate.Height = 21;
            this.OpinionDataGridView.Size = new System.Drawing.Size(697, 348);
            this.OpinionDataGridView.TabIndex = 0;
            this.OpinionDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.OpinionDataGridView_CellValueChanged);
            this.OpinionDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.OpinionDataGridView_CurrentCellDirtyStateChanged);
            // 
            // opinionSelect
            // 
            this.opinionSelect.HeaderText = "Select Opinion";
            this.opinionSelect.Name = "opinionSelect";
            // 
            // opinionName
            // 
            this.opinionName.HeaderText = "Opinion Name";
            this.opinionName.Name = "opinionName";
            this.opinionName.ReadOnly = true;
            // 
            // opinionDetail
            // 
            this.opinionDetail.FillWeight = 150F;
            this.opinionDetail.HeaderText = "詳細";
            this.opinionDetail.Name = "opinionDetail";
            this.opinionDetail.ReadOnly = true;
            this.opinionDetail.Width = 200;
            // 
            // opinionRange
            // 
            this.opinionRange.FillWeight = 150F;
            this.opinionRange.HeaderText = "適用範囲";
            this.opinionRange.Name = "opinionRange";
            this.opinionRange.ReadOnly = true;
            this.opinionRange.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.opinionRange.Width = 200;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tempRadioButton);
            this.groupBox1.Controls.Add(this.menuRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(28, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TestType";
            // 
            // tempRadioButton
            // 
            this.tempRadioButton.AutoSize = true;
            this.tempRadioButton.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tempRadioButton.Location = new System.Drawing.Point(107, 28);
            this.tempRadioButton.Name = "tempRadioButton";
            this.tempRadioButton.Size = new System.Drawing.Size(61, 18);
            this.tempRadioButton.TabIndex = 1;
            this.tempRadioButton.Text = "Temp";
            this.tempRadioButton.UseVisualStyleBackColor = true;
            this.tempRadioButton.CheckedChanged += new System.EventHandler(this.tempRadioButton_CheckedChanged);
            // 
            // menuRadioButton
            // 
            this.menuRadioButton.AutoSize = true;
            this.menuRadioButton.Checked = true;
            this.menuRadioButton.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.menuRadioButton.Location = new System.Drawing.Point(17, 28);
            this.menuRadioButton.Name = "menuRadioButton";
            this.menuRadioButton.Size = new System.Drawing.Size(60, 18);
            this.menuRadioButton.TabIndex = 0;
            this.menuRadioButton.TabStop = true;
            this.menuRadioButton.Text = "Menu";
            this.menuRadioButton.UseVisualStyleBackColor = true;
            this.menuRadioButton.CheckedChanged += new System.EventHandler(this.menuRadioButton_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(111, 516);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(487, 516);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OCRRadioButton);
            this.groupBox2.Controls.Add(this.LogRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(263, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 70);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OcrFlag";
            // 
            // OCRRadioButton
            // 
            this.OCRRadioButton.AutoSize = true;
            this.OCRRadioButton.Location = new System.Drawing.Point(113, 28);
            this.OCRRadioButton.Name = "OCRRadioButton";
            this.OCRRadioButton.Size = new System.Drawing.Size(47, 16);
            this.OCRRadioButton.TabIndex = 1;
            this.OCRRadioButton.Text = "OCR";
            this.OCRRadioButton.UseVisualStyleBackColor = true;
            this.OCRRadioButton.CheckedChanged += new System.EventHandler(this.OCRRadioButton_CheckedChanged);
            // 
            // LogRadioButton
            // 
            this.LogRadioButton.AutoSize = true;
            this.LogRadioButton.Checked = true;
            this.LogRadioButton.Location = new System.Drawing.Point(22, 28);
            this.LogRadioButton.Name = "LogRadioButton";
            this.LogRadioButton.Size = new System.Drawing.Size(41, 16);
            this.LogRadioButton.TabIndex = 0;
            this.LogRadioButton.TabStop = true;
            this.LogRadioButton.Text = "Log";
            this.LogRadioButton.UseVisualStyleBackColor = true;
            this.LogRadioButton.CheckedChanged += new System.EventHandler(this.LogRadioButton_CheckedChanged);
            // 
            // ignoreCaseCheckBox
            // 
            this.ignoreCaseCheckBox.AutoSize = true;
            this.ignoreCaseCheckBox.Location = new System.Drawing.Point(45, 107);
            this.ignoreCaseCheckBox.Name = "ignoreCaseCheckBox";
            this.ignoreCaseCheckBox.Size = new System.Drawing.Size(132, 16);
            this.ignoreCaseCheckBox.TabIndex = 5;
            this.ignoreCaseCheckBox.Text = "文言检查忽略大小写";
            this.ignoreCaseCheckBox.UseVisualStyleBackColor = true;
            this.ignoreCaseCheckBox.Visible = false;
            // 
            // OpinionSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 555);
            this.Controls.Add(this.ignoreCaseCheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OpinionDataGridView);
            this.Name = "OpinionSelectForm";
            this.Text = "OpinionSelectForm";
            this.Load += new System.EventHandler(this.OpinionSelectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OpinionDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView OpinionDataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton tempRadioButton;
        private System.Windows.Forms.RadioButton menuRadioButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn opinionSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn opinionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn opinionDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn opinionRange;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton OCRRadioButton;
        private System.Windows.Forms.RadioButton LogRadioButton;
        private System.Windows.Forms.CheckBox ignoreCaseCheckBox;
    }
}
