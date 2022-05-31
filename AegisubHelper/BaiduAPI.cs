using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AegisubHelper
{
    public static class BaiduAPI
    {
        public static async Task<string> VoiceTranslate(string path)
        {
            string url = "https://fanyi-api.baidu.com/api/trans/v2/voicetrans";
            string appid = Config.GetConfig<string>("Baidu_AppId");
            string key = Config.GetConfig<string>("Baidu_Key");
            long timestamp = Helper.TimeStamp;
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("X-Appid", new string[] { appid });
            client.DefaultRequestHeaders.Add("X-Timestamp", new string[] { timestamp.ToString() });
            if (File.Exists(path) == false)
                throw new FileNotFoundException("No audio file found");
            var info = new FileInfo(path);
            string base64 = Convert.ToBase64String(File.ReadAllBytes(path));
            string sign = Helper.HMACSHA256(appid + timestamp + base64, key);
            client.DefaultRequestHeaders.Add("X-Sign", new string[] { sign });
            var json = new
            {
                from = "jp",
                to = "zh",
                voice = base64,
                format = "wav",
            };
            var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<string> TextTranslate(string text)
        {
            string url = "https://fanyi-api.baidu.com/api/trans/vip/translate";
            string appid = Config.GetConfig<string>("Baidu_AppId");
            string key = Config.GetConfig<string>("Baidu_Key");
            long timestamp = Helper.TimeStamp;
            using HttpClient client = new();
            Dictionary<string, string> dic = new();
            string salt = Helper.MD5Encrypt(key + timestamp);
            dic.Add("q", text);
            dic.Add("from", "jp");
            dic.Add("to", "zh");
            dic.Add("appid", appid);
            dic.Add("salt", salt);
            dic.Add("sign", Helper.MD5Encrypt(appid+text+salt+key));
            var formStr = string.Join('&', dic.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
            var content = new StringContent(formStr, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync(url, content);
            var json = JObject.Parse(await response.Content.ReadAsStringAsync());
            if(json.ContainsKey("error_code"))
            {
                return "err";
            }
            else
            {
                return json["trans_result"][0]["dst"].ToString();
            }
        }
    }
}
