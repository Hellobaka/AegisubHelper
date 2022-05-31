namespace AegisubHelper
{
    partial class Settings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Baidu_AppKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Baidu_AppId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpactiySelector = new System.Windows.Forms.TrackBar();
            this.opacityText = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Youdao_Secret = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Youdao_AppKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.resetBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpactiySelector)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Baidu_AppKey);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Baidu_AppId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "百度API";
            // 
            // Baidu_AppKey
            // 
            this.Baidu_AppKey.Location = new System.Drawing.Point(60, 50);
            this.Baidu_AppKey.Name = "Baidu_AppKey";
            this.Baidu_AppKey.Size = new System.Drawing.Size(201, 23);
            this.Baidu_AppKey.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "AppKey:";
            // 
            // Baidu_AppId
            // 
            this.Baidu_AppId.Location = new System.Drawing.Point(60, 25);
            this.Baidu_AppId.Name = "Baidu_AppId";
            this.Baidu_AppId.Size = new System.Drawing.Size(201, 23);
            this.Baidu_AppId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "AppID:";
            // 
            // OpactiySelector
            // 
            this.OpactiySelector.LargeChange = 15;
            this.OpactiySelector.Location = new System.Drawing.Point(12, 206);
            this.OpactiySelector.Maximum = 100;
            this.OpactiySelector.Minimum = 30;
            this.OpactiySelector.Name = "OpactiySelector";
            this.OpactiySelector.Size = new System.Drawing.Size(267, 45);
            this.OpactiySelector.SmallChange = 5;
            this.OpactiySelector.TabIndex = 2;
            this.OpactiySelector.TickStyle = System.Windows.Forms.TickStyle.None;
            this.OpactiySelector.Value = 80;
            this.OpactiySelector.Scroll += new System.EventHandler(this.Opactiy_Scroll);
            // 
            // opacityText
            // 
            this.opacityText.AutoSize = true;
            this.opacityText.Location = new System.Drawing.Point(12, 186);
            this.opacityText.Name = "opacityText";
            this.opacityText.Size = new System.Drawing.Size(117, 17);
            this.opacityText.TabIndex = 3;
            this.opacityText.Text = "主窗口透明度：80%";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Youdao_Secret);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Youdao_AppKey);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 82);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "有道API";
            // 
            // Youdao_Secret
            // 
            this.Youdao_Secret.Location = new System.Drawing.Point(60, 50);
            this.Youdao_Secret.Name = "Youdao_Secret";
            this.Youdao_Secret.Size = new System.Drawing.Size(201, 23);
            this.Youdao_Secret.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Secret:";
            // 
            // Youdao_AppKey
            // 
            this.Youdao_AppKey.Location = new System.Drawing.Point(60, 25);
            this.Youdao_AppKey.Name = "Youdao_AppKey";
            this.Youdao_AppKey.Size = new System.Drawing.Size(201, 23);
            this.Youdao_AppKey.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "AppKey:";
            // 
            // resetBtn
            // 
            this.resetBtn.Location = new System.Drawing.Point(12, 257);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(75, 23);
            this.resetBtn.TabIndex = 5;
            this.resetBtn.Text = "重置";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(93, 257);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 6;
            this.saveBtn.Text = "保存";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 294);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.opacityText);
            this.Controls.Add(this.OpactiySelector);
            this.Controls.Add(this.groupBox1);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpactiySelector)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar OpactiySelector;
        private System.Windows.Forms.TextBox Baidu_AppKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Baidu_AppId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label opacityText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Youdao_Secret;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Youdao_AppKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.Button saveBtn;
    }
}