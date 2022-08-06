using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WebSocketSharp;

namespace AegisubHelper
{
    public class YoudaoAsr
    {
        public static WebSocket SocketConnect { get; set; }
        private static bool Usable { get; set; } = false;
        public delegate void SocketEnd_Handler(string text);
        public static event SocketEnd_Handler OnSocketEnd;
        private static string last_Text = "";
        public static void Connect()
        {
            string url = "wss://openapi.youdao.com/stream_asropenapi?";
            Directory.CreateDirectory("Asr");
            Dictionary<string, string> dic = new();
            
            string appKey = Config.GetConfig<string>("Youdao_AppId");
            string appSecret = Config.GetConfig<string>("Youdao_Key");
            string format = "wav";
            string curtime = Helper.TimeStamp.ToString();
            string signType = "v4";
            string langType = "ja";
            string channel = "1";
            string version = "v1";
            string rate = "16000";
            string salt = Guid.NewGuid().ToString();
            string signStr = appKey + salt + curtime + appSecret;
            string sign = Helper.SHA256(signStr).ToLower();

            dic.Add("langType", langType);
            dic.Add("curtime", curtime);
            dic.Add("appKey", appKey);
            dic.Add("format", format);
            dic.Add("signType", signType);
            dic.Add("channel", channel);
            dic.Add("version", version);
            dic.Add("rate", rate);
            dic.Add("salt", salt);
            dic.Add("sign", sign);
            var formStr = string.Join('&', dic.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
            url += formStr;
            SocketConnect = new(url);
            SocketConnect.OnOpen += SocketConnect_OnOpen;
            SocketConnect.OnClose += SocketConnect_OnClose;
            SocketConnect.OnMessage += SocketConnect_OnMessage;
            SocketConnect.OnError += SocketConnect_OnError;
            SocketConnect.Connect();
        }

        private static void SocketConnect_OnError(object? sender, WebSocketSharp.ErrorEventArgs e)
        {
        }

        private static void SocketConnect_OnMessage(object? sender, MessageEventArgs e)
        {
            JObject json = JObject.Parse(e.Data);
            Debug.WriteLine(e.Data);
            if (json["action"].ToString() == "error")
            {
                MessageBox.Show(json["errorCode"].ToString());
                return;
            }
            if(json["action"].ToString() == "started")
            {
                Usable = true;
                FileStream stream = new(AudioHelper.outputFilePath, FileMode.Open);
                while (stream.Position < stream.Length)
                {
                    SocketConnect.Send(stream, 6400);
                }
                stream.Dispose();
                SocketConnect.Send(Encoding.UTF8.GetBytes("{\"end\": \"true\"}"));
            }
            if (json["action"].ToString() == "recognition")
            {
                last_Text = (json["result"] as JArray)?[0]?["st"]?["sentence"]?.ToString();
            }
        }

        private static void SocketConnect_OnClose(object? sender, CloseEventArgs e)
        {
            Debug.WriteLine("Socket Close.");
            OnSocketEnd?.Invoke(last_Text);
        }

        private static void SocketConnect_OnOpen(object? sender, EventArgs e)
        {
            last_Text = "err";
            Debug.WriteLine("Socket Open.");
        }
    }
}
