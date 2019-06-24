namespace FTBExcel2Script
{
    partial class RunningForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunningForm));
            this.RunningPicture = new System.Windows.Forms.PictureBox();
            this.labelMsgRunning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RunningPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // RunningPicture
            // 
            this.RunningPicture.Image = ((System.Drawing.Image)(resources.GetObject("RunningPicture.Image")));
            this.RunningPicture.InitialImage = null;
            this.RunningPicture.Location = new System.Drawing.Point(130, 22);
            this.RunningPicture.Margin = new System.Windows.Forms.Padding(4);
            this.RunningPicture.Name = "RunningPicture";
            this.RunningPicture.Size = new System.Drawing.Size(36, 34);
            this.RunningPicture.TabIndex = 2;
            this.RunningPicture.TabStop = false;
            // 
            // labelMsgRunning
            // 
            this.labelMsgRunning.AutoSize = true;
            this.labelMsgRunning.Location = new System.Drawing.Point(47, 72);
            this.labelMsgRunning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMsgRunning.Name = "labelMsgRunning";
            this.labelMsgRunning.Size = new System.Drawing.Size(219, 15);
            this.labelMsgRunning.TabIndex = 3;
            this.labelMsgRunning.Text = "実行中です。しばらくお待ちください。";
            // 
            // RunningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(309, 146);
            this.ControlBox = false;
            this.Controls.Add(this.labelMsgRunning);
            this.Controls.Add(this.RunningPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RunningForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "実行中";
            ((System.ComponentModel.ISupportInitialize)(this.RunningPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RunningPicture;
        private System.Windows.Forms.Label labelMsgRunning;
    }
}
