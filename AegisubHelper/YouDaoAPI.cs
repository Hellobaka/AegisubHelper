using System;
using System.Collections.Generic;
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
        public static async Task<HttpResponseMessage> Translate(string path)
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
            return client.PostAsync(url, content).Result;            
        }
    }
}
