namespace Tool.UI
{
    partial class ConditionInput
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
            this.ConditionDataGridView = new System.Windows.Forms.DataGridView();
            this.condition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.input = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ConditionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ConditionDataGridView
            // 
            this.ConditionDataGridView.AllowUserToAddRows = false;
            this.ConditionDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConditionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConditionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.condition,
            this.input});
            this.ConditionDataGridView.Location = new System.Drawing.Point(33, 25);
            this.ConditionDataGridView.Name = "ConditionDataGridView";
            this.ConditionDataGridView.RowHeadersWidth = 60;
            this.ConditionDataGridView.RowTemplate.Height = 21;
            this.ConditionDataGridView.Size = new System.Drawing.Size(779, 435);
            this.ConditionDataGridView.TabIndex = 0;
            this.ConditionDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConditionDataGridView_CellContentClick);
            this.ConditionDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConditionDataGridView_CellDoubleClick);
            this.ConditionDataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConditionDataGridView_CellEnter);
            // 
            // condition
            // 
            this.condition.FillWeight = 200F;
            this.condition.HeaderText = "condition";
            this.condition.Name = "condition";
            this.condition.ReadOnly = true;
            this.condition.Width = 300;
            // 
            // input
            // 
            this.input.FillWeight = 250F;
            this.input.HeaderText = "input";
            this.input.Name = "input";
            this.input.ReadOnly = true;
            this.input.Width = 400;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(632, 521);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Close";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(111, 521);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(386, 521);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // ConditionInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 577);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.ConditionDataGridView);
            this.Name = "ConditionInput";
            this.Text = "ConditionInput";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConditionInput_FormClosed);
            this.Load += new System.EventHandler(this.ConditionInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ConditionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ConditionDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn condition;
        private System.Windows.Forms.DataGridViewTextBoxColumn input;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
    }
}
