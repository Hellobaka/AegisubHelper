namespace AegisubHelper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.processTitle = new System.Windows.Forms.TextBox();
            this.playText = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.processLoadStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.memoryStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // processTitle
            // 
            this.processTitle.Enabled = false;
            this.processTitle.Location = new System.Drawing.Point(12, 12);
            this.processTitle.Name = "processTitle";
            this.processTitle.Size = new System.Drawing.Size(288, 23);
            this.processTitle.TabIndex = 0;
            // 
            // playText
            // 
            this.playText.Enabled = false;
            this.playText.Location = new System.Drawing.Point(12, 41);
            this.playText.Name = "playText";
            this.playText.Size = new System.Drawing.Size(288, 23);
            this.playText.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processLoadStatus,
            this.memoryStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // processLoadStatus
            // 
            this.processLoadStatus.Name = "processLoadStatus";
            this.processLoadStatus.Size = new System.Drawing.Size(77, 17);
            this.processLoadStatus.Text = "未找到进程...";
            // 
            // memoryStatus
            // 
            this.memoryStatus.Name = "memoryStatus";
            this.memoryStatus.Size = new System.Drawing.Size(77, 17);
            this.memoryStatus.Text = "等待初始化...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(306, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.playText);
            this.Controls.Add(this.processTitle);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox processTitle;
        private System.Windows.Forms.TextBox playText;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel processLoadStatus;
        private System.Windows.Forms.ToolStripStatusLabel memoryStatus;
        private System.Windows.Forms.Button button1;
    }
}
