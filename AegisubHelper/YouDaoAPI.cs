using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AegisubHelper
{
    internal class YouDaoAPI
    {
        public static async Task<string> VoiceTranslate(string path)
        {
            string url = "https://openapi.youdao.com/speechtransapi";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            
            if (File.Exists(path) == false)
                throw new FileNotFoundException("No audio file found");
            string base64 = Convert.ToBase64String(File.ReadAllBytes(path));

            string appKey = Config.GetConfig<string>("Youdao_AppId");
            string appSecret = Config.GetConfig<string>("Youdao_Key");
            string format = "wav";
            string rate = "16000";
            string channel = "1";
            string type = "1";
            string salt = Guid.NewGuid().ToString();
            string from = "ja";
            string to = "zh-CHS";
            dic.Add("from", from);
            dic.Add("to", to);
            string signStr = appKey + base64 + salt + appSecret; ;
            string sign = Helper.MD5Encrypt(signStr);
            
            dic.Add("q", base64);
            dic.Add("appKey", appKey);
            dic.Add("format", format);
            dic.Add("rate", rate);
            dic.Add("channel", channel);
            dic.Add("type", type);
            dic.Add("salt", salt);
            dic.Add("sign", sign);
            dic.Add("signType", "v1");
            
            using HttpClient client = new();
            var formStr = string.Join('&', dic.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
            var content = new StringContent(formStr, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<string> Voice2Text(string path)
        {
            string url = "https://openapi.youdao.com/asrapi";
            Dictionary<string, string> dic = new ();
            
            if (File.Exists(path) == false)
                throw new FileNotFoundException("No audio file found");
            string base64 = Convert.ToBase64String(File.ReadAllBytes(path));

            string appKey = Config.GetConfig<string>("Youdao_AppId");
            string appSecret = Config.GetConfig<string>("Youdao_Key");
            string format = "wav";
            string rate = "16000";
            string channel = "1";
            string type = "1";
            string salt = Guid.NewGuid().ToString();
            string curtime = Helper.TimeStamp.ToString();
            string signStr = appKey + Truncate(base64) + salt+ curtime + appSecret;
            string sign = Helper.SHA256(signStr);
            
            dic.Add("q", base64);
            dic.Add("langType", "ja");
            dic.Add("appKey", appKey);
            dic.Add("format", format);
            dic.Add("rate", rate);
            dic.Add("channel", channel);
            dic.Add("type", type);
            dic.Add("salt", salt);
            dic.Add("sign", sign);
            dic.Add("signType", "v2");
            dic.Add("curtime", curtime);
            
            using HttpClient client = new();
            var formStr = string.Join('&', dic.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
            var content = new StringContent(formStr, Encoding.UTF8, "application/x-www-form-urlencoded");
            var result = await client.PostAsync(url, content);
            var json = JObject.Parse(await result.Content.ReadAsStringAsync());
            if (((int)json["errorCode"]) == 0)
            {
                return json["result"][0].ToString();
            }
            else
            {
                return "err";
            }
        }
        public static async Task<string> TTS(string text, bool type)
        {
            string url = "https://openapi.youdao.com/ttsapi";
            Dictionary<string, string> dic = new ();

            string appKey = Config.GetConfig<string>("Youdao_AppId");
            string appSecret = Config.GetConfig<string>("Youdao_Key");
            string format = "mp3";
            string voice = "0";
            string speed = "1";
            string volume = "1.00";
            string salt = Guid.NewGuid().ToString();
            string signStr = appKey + text + salt + appSecret;
            string sign = Helper.MD5Encrypt(signStr).ToUpper();
            
            dic.Add("q", text);
            dic.Add("langType", type?"ja": "zh-CHS");
            dic.Add("appKey", appKey);
            dic.Add("format", format);
            dic.Add("voice", voice);
            dic.Add("speed", speed);
            dic.Add("volume", volume);
            dic.Add("salt", salt);
            dic.Add("sign", sign);
            
            using HttpClient client = new();
            var formStr = string.Join('&', dic.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
            var content = new StringContent(formStr, Encoding.UTF8);
            content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
            var result = await client.PostAsync(url, content);
            if (result.Content.Headers.ContentType.MediaType == "audio/mp3")
            {
                byte[] file = await result.Content.ReadAsByteArrayAsync();
                Directory.CreateDirectory("tmp");
                File.WriteAllBytes(Path.Combine("tmp", "audio.mp3"), file);
                return Path.Combine("tmp", "audio.mp3");
            }
            else
            {
                return "err";
            }
        }
        private static string Truncate(string q)
        {
            if (q == null)
            {
                return string.Empty;
            }
            int len = q.Length;
            return len <= 20 ? q : (q.Substring(0, 10) + len + q.Substring(len - 10, 10));
        }
    }
}
