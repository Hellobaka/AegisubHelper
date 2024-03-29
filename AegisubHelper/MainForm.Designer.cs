﻿namespace AegisubHelper
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
            this.components = new System.ComponentModel.Container();
            this.processTitle = new System.Windows.Forms.TextBox();
            this.playText = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.processLoadStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.memoryStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.copyStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.RecordBtn = new System.Windows.Forms.Button();
            this.HotKeyStatus = new System.Windows.Forms.Label();
            this.networkProgress = new System.Windows.Forms.ProgressBar();
            this.networkProcessLabel = new System.Windows.Forms.Label();
            this.settingBtn = new System.Windows.Forms.Button();
            this.buttonTip = new System.Windows.Forms.ToolTip(this.components);
            this.translatedText = new System.Windows.Forms.ListBox();
            this.clearBtn = new System.Windows.Forms.Button();
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
            this.buttonTip.SetToolTip(this.playText, "Aegisub中的播放状态");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processLoadStatus,
            this.memoryStatus,
            this.copyStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 341);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(430, 22);
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
            // copyStatus
            // 
            this.copyStatus.Name = "copyStatus";
            this.copyStatus.Size = new System.Drawing.Size(68, 17);
            this.copyStatus.Text = "已复制文本";
            this.copyStatus.Visible = false;
            // 
            // RecordBtn
            // 
            this.RecordBtn.Location = new System.Drawing.Point(306, 12);
            this.RecordBtn.Name = "RecordBtn";
            this.RecordBtn.Size = new System.Drawing.Size(75, 23);
            this.RecordBtn.TabIndex = 3;
            this.RecordBtn.Text = "录制";
            this.buttonTip.SetToolTip(this.RecordBtn, "切换音频录制状态");
            this.RecordBtn.UseVisualStyleBackColor = true;
            this.RecordBtn.Click += new System.EventHandler(this.Record_Click);
            // 
            // HotKeyStatus
            // 
            this.HotKeyStatus.AutoSize = true;
            this.HotKeyStatus.Location = new System.Drawing.Point(306, 44);
            this.HotKeyStatus.Name = "HotKeyStatus";
            this.HotKeyStatus.Size = new System.Drawing.Size(68, 17);
            this.HotKeyStatus.TabIndex = 5;
            this.HotKeyStatus.Text = "不捕获模式";
            // 
            // networkProgress
            // 
            this.networkProgress.Location = new System.Drawing.Point(306, 93);
            this.networkProgress.MarqueeAnimationSpeed = 30;
            this.networkProgress.Name = "networkProgress";
            this.networkProgress.Size = new System.Drawing.Size(112, 23);
            this.networkProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.networkProgress.TabIndex = 6;
            this.networkProgress.Visible = false;
            // 
            // networkProcessLabel
            // 
            this.networkProcessLabel.AutoSize = true;
            this.networkProcessLabel.Location = new System.Drawing.Point(306, 73);
            this.networkProcessLabel.Name = "networkProcessLabel";
            this.networkProcessLabel.Size = new System.Drawing.Size(65, 17);
            this.networkProcessLabel.TabIndex = 7;
            this.networkProcessLabel.Text = "获取结果...";
            // 
            // settingBtn
            // 
            this.settingBtn.Location = new System.Drawing.Point(387, 12);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(28, 23);
            this.settingBtn.TabIndex = 8;
            this.settingBtn.Text = "⚙️";
            this.buttonTip.SetToolTip(this.settingBtn, "打开设置窗口");
            this.settingBtn.UseVisualStyleBackColor = true;
            this.settingBtn.Click += new System.EventHandler(this.settingBtn_Click);
            // 
            // translatedText
            // 
            this.translatedText.FormattingEnabled = true;
            this.translatedText.HorizontalScrollbar = true;
            this.translatedText.ItemHeight = 17;
            this.translatedText.Location = new System.Drawing.Point(12, 73);
            this.translatedText.Name = "translatedText";
            this.translatedText.Size = new System.Drawing.Size(288, 259);
            this.translatedText.TabIndex = 9;
            this.translatedText.DoubleClick += new System.EventHandler(this.translatedText_DoubleClick);
            this.translatedText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.translatedText_KeyUp);
            this.translatedText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.translatedText_MouseUp);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(306, 309);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 10;
            this.clearBtn.Text = "清空";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 363);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.translatedText);
            this.Controls.Add(this.settingBtn);
            this.Controls.Add(this.networkProcessLabel);
            this.Controls.Add(this.networkProgress);
            this.Controls.Add(this.HotKeyStatus);
            this.Controls.Add(this.RecordBtn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.playText);
            this.Controls.Add(this.processTitle);
            this.Name = "MainForm";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AegisubHelper";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
        private System.Windows.Forms.Button RecordBtn;
        private System.Windows.Forms.Label HotKeyStatus;
        private System.Windows.Forms.ProgressBar networkProgress;
        private System.Windows.Forms.Label networkProcessLabel;
        private System.Windows.Forms.Button settingBtn;
        private System.Windows.Forms.ToolTip buttonTip;
        private System.Windows.Forms.ListBox translatedText;
        private System.Windows.Forms.ToolStripStatusLabel copyStatus;
        private System.Windows.Forms.Button clearBtn;
    }
}
