namespace Tool.UI
{
    partial class ConditionSelect
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
            this.NoConditionCheckBox = new System.Windows.Forms.CheckBox();
            this.InsideCheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.conditionCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.hardwareDeviceCheckBox = new System.Windows.Forms.CheckBox();
            this.selsecButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NoConditionCheckBox
            // 
            this.NoConditionCheckBox.AutoSize = true;
            this.NoConditionCheckBox.Location = new System.Drawing.Point(16, 44);
            this.NoConditionCheckBox.Name = "NoConditionCheckBox";
            this.NoConditionCheckBox.Size = new System.Drawing.Size(86, 16);
            this.NoConditionCheckBox.TabIndex = 0;
            this.NoConditionCheckBox.Text = "no condition";
            this.NoConditionCheckBox.UseVisualStyleBackColor = true;
            this.NoConditionCheckBox.CheckedChanged += new System.EventHandler(this.NoConditionCheckBox_CheckedChanged);
            // 
            // InsideCheckBox
            // 
            this.InsideCheckBox.AutoSize = true;
            this.InsideCheckBox.Location = new System.Drawing.Point(265, 47);
            this.InsideCheckBox.Name = "InsideCheckBox";
            this.InsideCheckBox.Size = new System.Drawing.Size(94, 16);
            this.InsideCheckBox.TabIndex = 1;
            this.InsideCheckBox.Text = "内部condition";
            this.InsideCheckBox.UseVisualStyleBackColor = true;
            this.InsideCheckBox.CheckedChanged += new System.EventHandler(this.InsideCheckBox_CheckedChanged);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(35, 553);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // conditionCheckedListBox
            // 
            this.conditionCheckedListBox.FormattingEnabled = true;
            this.conditionCheckedListBox.HorizontalScrollbar = true;
            this.conditionCheckedListBox.Location = new System.Drawing.Point(16, 191);
            this.conditionCheckedListBox.Name = "conditionCheckedListBox";
            this.conditionCheckedListBox.Size = new System.Drawing.Size(516, 312);
            this.conditionCheckedListBox.TabIndex = 4;
            this.conditionCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.conditionCheckedListBox_SelectedIndexChanged);
            // 
            // hardwareDeviceCheckBox
            // 
            this.hardwareDeviceCheckBox.AutoSize = true;
            this.hardwareDeviceCheckBox.Location = new System.Drawing.Point(16, 136);
            this.hardwareDeviceCheckBox.Name = "hardwareDeviceCheckBox";
            this.hardwareDeviceCheckBox.Size = new System.Drawing.Size(94, 16);
            this.hardwareDeviceCheckBox.TabIndex = 5;
            this.hardwareDeviceCheckBox.Text = "外部condition";
            this.hardwareDeviceCheckBox.UseVisualStyleBackColor = true;
            // 
            // selsecButton
            // 
            this.selsecButton.Location = new System.Drawing.Point(186, 553);
            this.selsecButton.Name = "selsecButton";
            this.selsecButton.Size = new System.Drawing.Size(75, 23);
            this.selsecButton.TabIndex = 6;
            this.selsecButton.Text = "SelectAll";
            this.selsecButton.UseVisualStyleBackColor = true;
            this.selsecButton.Click += new System.EventHandler(this.selsecButton_Click);
            // 
            // ConditionSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 606);
            this.Controls.Add(this.selsecButton);
            this.Controls.Add(this.hardwareDeviceCheckBox);
            this.Controls.Add(this.conditionCheckedListBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.InsideCheckBox);
            this.Controls.Add(this.NoConditionCheckBox);
            this.Name = "ConditionSelect";
            this.Text = "Condition";
            this.Load += new System.EventHandler(this.Condition_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox NoConditionCheckBox;
        private System.Windows.Forms.CheckBox InsideCheckBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.CheckedListBox conditionCheckedListBox;
        private System.Windows.Forms.CheckBox hardwareDeviceCheckBox;
        private System.Windows.Forms.Button selsecButton;
    }
}
