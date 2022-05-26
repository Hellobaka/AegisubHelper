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

namespace AegisubHelper
{
    public static class BaiduAPI
    {
        public static async Task<HttpResponseMessage> Translate(string path)
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
            return client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json")).Result;
        }
    }
}
