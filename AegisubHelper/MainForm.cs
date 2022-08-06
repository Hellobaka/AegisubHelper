using NAudio.Wave;
using Newtonsoft.Json.Linq;
using PInvoke;
using System;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AegisubHelper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            // Either WINDOWS key was held down. These keys are labeled with the Windows logo.
            // Keyboard shortcuts that involve the WINDOWS key are reserved for use by the
            // operating system.
            Windows = 8
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)User32.WindowMessage.WM_HOTKEY)
            {
                switch (m.WParam.ToInt32())
                {
                    case 0x64:
                        AutoRecord = !AutoRecord;
                        if (AutoRecord)
                        {
                            HotKeyStatus.Text = "自动捕获模式";
                        }
                        else
                        {
                            HotKeyStatus.Text = "不捕获模式";
                        }
                        break;
                    default:
                        break;
                }
            }
            base.WndProc(ref m);
        }
        public bool SearchProcessFlag { get; set; } = true;
        public static Process AegisubProcess { get; set; }
        public bool AutoRecord { get; set; }
        public bool PlayStatus { get; set; }
        public bool RecordStatus { get; set; }
        private void MainForm_Load(object sender, EventArgs e)
        {
            networkProcessLabel.Visible = false;
            RegisterHotKey(this.Handle, 100, KeyModifiers.Control, Keys.F9);
            Instance = this;
            Opacity = Config.GetConfig<int>("Opacity") / 100.0;
            if (Opacity == 0) Opacity = 0.8;
            new Thread(async () =>
            {
                while (SearchProcessFlag)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        if (AegisubProcess != null && AegisubProcess.HasExited == false)
                        {
                            processTitle.Text = $"{AegisubProcess.MainWindowTitle} - {AegisubProcess.Id}";
                            if (MemoryHelper.Memory == null)
                            {
                                if (MemoryHelper.Init(AegisubProcess) == false)
                                {
                                    memoryStatus.Text = "读取内存出错....";
                                    SearchProcessFlag = false;
                                }
                                else
                                {
                                    if (MemoryHelper.SearchPlayMemory())
                                    {
                                        memoryStatus.Text = "内存读取成功";
                                    }
                                    else
                                    {
                                        memoryStatus.Text = "搜索内存出错....";
                                        SearchProcessFlag = false;
                                    }
                                }
                            }
                            else
                            {
                                bool r = MemoryHelper.IsPlaying();
                                if (AutoRecord && PlayStatus != r)
                                {
                                    CallRecordAndTranslate(RecordStatus);
                                    RecordStatus = !RecordStatus;
                                }
                                PlayStatus = r;
                                if (PlayStatus)
                                {
                                    playText.Text = "正在播放";
                                }
                                else
                                {
                                    playText.Text = "未播放";
                                }
                            }
                        }
                        else
                        {
                            var list = Process.GetProcessesByName("aegisub32");
                            if (list.Length != 0)
                            {
                                AegisubProcess = list[0];
                                processLoadStatus.Text = "已找到进程.";
                            }
                            else
                            {
                                processTitle.Text = "Aegisub not found";
                                processLoadStatus.Text = "未找到进程.";
                                SearchProcessFlag = false;
                            }
                        }
                    }));
                    Thread.Sleep(100);
                }
            }).Start();
            YoudaoAsr.OnSocketEnd += YoudaoAsr_OnSocketEnd;
        }

        private async void YoudaoAsr_OnSocketEnd(string text)
        {
            string translate = "";
            if (text != "err")
            {
                translate = await BaiduAPI.TextTranslate(text);
            }
            else
            {
                return;
            }
            BeginInvoke(new MethodInvoker(() =>
            {
                networkProgress.Visible = false;
                networkProcessLabel.Visible = false;
                translatedText.Items.Add(text);
                translatedText.Items.Add(translate);
                translatedText.Items.Add("");
                translatedText.SelectedIndex = translatedText.Items.Count - 1;
            }));
        }

            public async void CallRecordAndTranslate(bool r)
        {
            if (r)
            {
                AudioHelper.StopRecord();
                RecordBtn.Text = "录制";
                networkProgress.Visible = true;
                networkProcessLabel.Visible = true;
                // YoudaoAsr.Connect();
                string origin = "";
                switch (Config.GetConfig<string>("AsrEngine"))                    
                {
                    case "Youdao":
                        origin = await YouDaoAPI.Voice2Text(AudioHelper.outputFilePath);
                        break;
                    case "Aliyun":
                    default:
                        origin = await AliyunAPI.Voice2Text(AudioHelper.outputFilePath);
                        break;
                }
                //var origin = await YouDaoAPI.Voice2Text(AudioHelper.outputFilePath);                
                string translate = "";
                if (origin != "err")
                {
                    translate = await BaiduAPI.TextTranslate(origin);
                }
                BeginInvoke(new MethodInvoker(() =>
                {
                    networkProgress.Visible = false;
                    networkProcessLabel.Visible = false;
                    translatedText.Items.Add(origin);
                    translatedText.Items.Add(translate);
                    translatedText.Items.Add("");
                    translatedText.SelectedIndex = translatedText.Items.Count - 1;
                }));
            }
            else
            {
                AudioHelper.StartRecord();
                RecordBtn.Text = "停止录制";
            }
        }
        private async void Record_Click(object sender, EventArgs e)
        {
            CallRecordAndTranslate(RecordStatus);
            RecordStatus = !RecordStatus;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SearchProcessFlag = false;
            UnregisterHotKey(Handle, 100);
        }
        public static MainForm Instance;
        private void settingBtn_Click(object sender, EventArgs e)
        {
            TopMost = false;
            new Settings().ShowDialog();
            TopMost = true;
        }
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        bool audioLock = false;
        private async void translatedText_DoubleClick(object sender, EventArgs e)
        {
            if (audioLock)
            {
                ShowStatusMsg("请等待音频播放完毕");
                return;
            }
            networkProgress.Visible = true;
            networkProcessLabel.Visible = true;

            string text = translatedText.SelectedItem.ToString();
            string result = "";
            switch (translatedText.SelectedIndex % 3)
            {
                case 2:
                    networkProgress.Visible = false;
                    networkProcessLabel.Visible = false;
                    return;
                case 0:
                    result = await YouDaoAPI.TTS(text, true);
                    break;
                case 1:
                    result = await YouDaoAPI.TTS(text, false);
                    break;
                default:
                    return;
            }
            networkProgress.Visible = false;
            networkProcessLabel.Visible = false;

            if (result != "err")
            {
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += (sender, args) =>
                    {
                        outputDevice.Dispose();
                        outputDevice = null;
                        audioFile.Dispose();
                        audioFile = null;
                        audioLock = false;
                        GC.Collect();
                    };
                }
                audioLock = true;
                if (audioFile == null)
                {
                    audioFile = new AudioFileReader(result);
                    outputDevice.Init(audioFile);
                }
                outputDevice.Play();
            }
        }

        private void translatedText_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && !string.IsNullOrWhiteSpace(translatedText.SelectedItem.ToString()))
            {
                Clipboard.SetText(translatedText.SelectedItem.ToString());
                ShowStatusMsg("已复制文本");
            }
        }
        public void ShowStatusMsg(string text, int timeout = 2000)
        {
            copyStatus.Text = text;
            copyStatus.Visible = true;
            new Thread(() =>
            {
                Thread.Sleep(timeout);
                copyStatus.Visible = false;
            }).Start();
        }

        private void translatedText_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine(e.KeyChar);
        }

        private void translatedText_KeyUp(object sender, KeyEventArgs e)
        {
            if (translatedText.Items.Count == 0) return;
            if(e.KeyCode == Keys.Delete)
            {
                int index = translatedText.SelectedIndex;
                switch (index % 3)
                {
                    case 0:                        
                        break;
                    case 1:
                        index--;
                        break;
                    case 2:
                        index -= 2;
                        break;
                }
                translatedText.Items.RemoveAt(index);
                translatedText.Items.RemoveAt(index);
                translatedText.Items.RemoveAt(index);
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            translatedText.Items.Clear();
        }
    }
}
