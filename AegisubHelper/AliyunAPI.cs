using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AegisubHelper
{
    public class AliyunAPI
    {
        public static string BaseURL { get; set; } = "https://nls-gateway-cn-beijing.aliyuncs.com/stream/v1/asr";
        public static string Aliyun_AppKey { get; set; } = Config.GetConfig<string>("Aliyun_AppKey");
        public static string Aliyun_AccessKeyID { get; set; } = Config.GetConfig<string>("Aliyun_AccessKeyID");
        public static string Aliyun_AccessKeySecret { get; set; } = Config.GetConfig<string>("Aliyun_AccessKeySecret");
        public static string Token { get; set; }
        public static DateTime GetTokenTime { get; set; }
        public static void RefreshToken()
        {
            IClientProfile profile = DefaultProfile.GetProfile(
            "cn-shanghai",
            Aliyun_AccessKeyID,
            Aliyun_AccessKeySecret);
            DefaultAcsClient client = new(profile);

            try
            {
                CommonRequest request = new()
                {
                    Domain = "nls-meta.cn-shanghai.aliyuncs.com",
                    Version = "2019-02-28",
                    Action = "CreateToken"
                };

                CommonResponse response = client.GetCommonResponse(request);
                JObject json = JObject.Parse(response.Data);
                Token = json["Token"]["Id"].ToString();
                GetTokenTime = DateTime.Now;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static async Task<string> Voice2Text(string path)
        {
            if(string.IsNullOrEmpty(Aliyun_AppKey))
            {
                throw new Exception("缺少阿里云APPKey");
            }
            string url = BaseURL + $"?appkey={Aliyun_AppKey}";
            if(!File.Exists(path))
            {
                throw new Exception("文件不存在");
            }
            byte[] audioData = File.ReadAllBytes(path);
            using HttpClient client = new();
            if (GetTokenTime.AddDays(1) < DateTime.Now)
            {
                RefreshToken();
            }
            client.DefaultRequestHeaders.Add("X-NLS-Token", Token);
            ByteArrayContent content = new(audioData);
            content.Headers.Add("Content-Type", "application/octet-stream");
            
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            string responseBodyAsText = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                JObject obj = JObject.Parse(responseBodyAsText);
                string result = obj["result"].ToString();
                return result;
            }
            else
            {
                Debug.WriteLine(responseBodyAsText);
                return "err";
            }
        }
    }
}
