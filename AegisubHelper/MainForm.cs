using Newtonsoft.Json.Linq;
using PInvoke;
using System;
using System.Diagnostics;
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
        }
        public async void CallRecordAndTranslate(bool r)
        {
            if (r)
            {
                AudioHelper.StopRecord();
                RecordBtn.Text = "录制";
                networkProgress.Visible = true;
                networkProcessLabel.Visible = true;
                var origin = await YouDaoAPI.Voice2Text(AudioHelper.outputFilePath);
                string translate = "";
                if (origin != "err")
                {
                    translate = await BaiduAPI.TextTranslate(origin);                    
                }
                BeginInvoke(new MethodInvoker(() =>
                {
                    networkProgress.Visible = false;
                    networkProcessLabel.Visible = false;
                    translatedText.Text += origin + "\n";
                    translatedText.Text += translate + "\n\n";
                    translatedText.Select(translatedText.Text.Length, 0);
                    translatedText.ScrollToCaret();
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
    }
}
