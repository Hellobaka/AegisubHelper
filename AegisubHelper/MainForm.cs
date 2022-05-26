﻿using Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AegisubHelper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public bool SearchProcessFlag { get; set; } = true;
        public static Process AegisubProcess { get; set; }
        private async void MainForm_Load(object sender, EventArgs e)
        {
            new Thread(() =>
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
                                playText.Text = r ? "正在播放" : "未播放";
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
        bool flag = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if(flag)
            {
                flag = false;
                AudioHelper.StopRecord();
                var text = AudioHelper.GetRawText();
                translatedText.Text += text.Result + "\n";
                translatedText.Text += AudioHelper.TranslateText(text.Result).TargetText + "\n";
            } 
            else
            {
                flag = true;
                AudioHelper.StartRecord();                
            }
        }
    }
}
