using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AegisubHelper
{
    public partial class Settings : Form
    {
        public bool SaveStatus { get; set; }
        public Settings()
        {
            InitializeComponent();
        }

        private void Opactiy_Scroll(object sender, EventArgs e)
        {
            opacityText.Text = $"主窗口透明度：{OpactiySelector.Value}%";
            MainForm.Instance.Opacity = OpactiySelector.Value / 100.0;
        }
        double MainFormOpcaitySave = 0.8;
        private void Settings_Load(object sender, EventArgs e)
        {
            Opacity = 1;
            MainFormOpcaitySave = MainForm.Instance.Opacity;
            Baidu_AppId.Text = Config.GetConfig<string>("Baidu_AppId");
            Baidu_AppKey.Text = Config.GetConfig<string>("Baidu_Key");
            Youdao_AppKey.Text = Config.GetConfig<string>("Youdao_AppId");
            Youdao_Secret.Text = Config.GetConfig<string>("Youdao_Key");
            Aliyun_AppKey.Text = Config.GetConfig<string>("Aliyun_AppKey");
            Aliyun_AccessID.Text = Config.GetConfig<string>("Aliyun_AccessKeyID");
            Aliyun_AccessSecret.Text = Config.GetConfig<string>("Aliyun_AccessKeySecret");
            string asrEngine = Config.GetConfig<string>("AsrEngine");
            switch (asrEngine)
            {
                case "Youdao":
                    Youdao_Engine.Checked = true;
                    break; 
                case "Aliyun":
                    Aliyun_Engine.Checked = true;
                    break;
                default:
                    break;
            }
            int opacity = Config.GetConfig<int>("Opacity");
            if (opacity == 0)
            {
                OpactiySelector.Value = 80;
            }
            else
            {
                OpactiySelector.Value = opacity;
            }
            opacityText.Text = $"主窗口透明度：{OpactiySelector.Value}%";
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveStatus = true;
            Config.WriteConfig("Baidu_AppId", Baidu_AppId.Text);
            Config.WriteConfig("Baidu_Key", Baidu_AppKey.Text);
            Config.WriteConfig("Youdao_AppId", Youdao_AppKey.Text);
            Config.WriteConfig("Youdao_Key", Youdao_Secret.Text);
            Config.WriteConfig("Aliyun_AppKey", Aliyun_AppKey.Text);
            Config.WriteConfig("Aliyun_AccessKeyID", Aliyun_AccessID.Text);
            Config.WriteConfig("Aliyun_AccessKeySecret", Aliyun_AccessSecret.Text);
            Config.WriteConfig("Opacity", OpactiySelector.Value);
            if (Youdao_Engine.Checked)
            {
                Config.WriteConfig("AsrEngine", "Youdao");
            }
            else if (Aliyun_Engine.Checked)
            {
                Config.WriteConfig("AsrEngine", "Aliyun");
            }
            Close();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            Baidu_AppId.Text = "";
            Baidu_AppKey.Text = "";
            Aliyun_AppKey.Text = "";
            Aliyun_AccessID.Text = "";
            OpactiySelector.Value = 80;
            opacityText.Text = $"主窗口透明度：{OpactiySelector.Value}%";
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveStatus)
            {
                MainForm.Instance.Opacity = MainFormOpcaitySave;
            }
        }
    }
}
