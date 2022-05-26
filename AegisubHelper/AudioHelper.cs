using NAudio.Wave;
using System;
using System.IO;
using System.Threading;
using TencentCloud.Asr.V20190614;
using TencentCloud.Asr.V20190614.Models;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Tmt.V20180321;
using TencentCloud.Tmt.V20180321.Models;

namespace AegisubHelper
{
    public static class AudioHelper
    {
        private static WasapiLoopbackCapture capture = null;
        
        private static readonly string outputFolder = "NAudio";
        public static readonly string outputFilePath = Path.Combine(outputFolder, "recorded.wav");
        private static WaveFileWriter writer = null;
        public static void StartRecord()
        {
            Directory.CreateDirectory(outputFolder);
            File.Delete(outputFilePath);
            capture = new WasapiLoopbackCapture();
            writer = new WaveFileWriter(outputFilePath, capture.WaveFormat = new(44000, 16, 2));
            capture.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
            };
            capture.StartRecording();
        }
        public static void StopRecord()
        {
            if (capture != null)
            { 
                capture.StopRecording();
                writer.Dispose();
                writer = null;
                capture.Dispose();
            }
        }
        public static SentenceRecognitionResponse GetRawText()
        {
            if (File.Exists(outputFilePath) == false)
                throw new FileNotFoundException("No audio file found");
            var info = new FileInfo(outputFilePath);
            string base64 = Convert.ToBase64String(File.ReadAllBytes(outputFilePath));
            Credential cred = new()
            {
                SecretId = Config.GetConfig<string>("SecretId"),
                SecretKey = Config.GetConfig<string>("SecretKey")
            };

            ClientProfile clientProfile = new();
            HttpProfile httpProfile = new();
            httpProfile.Endpoint = ("asr.tencentcloudapi.com");
            clientProfile.HttpProfile = httpProfile;
            clientProfile.SignMethod = ClientProfile.SIGN_TC3SHA256;
            AsrClient client = new(cred, "", clientProfile);
            SentenceRecognitionRequest req = new()
            {
                ProjectId = 0,
                SubServiceType = 2,
                EngSerViceType = "16k_ja",
                SourceType = 1,
                VoiceFormat = "wav",
                UsrAudioKey = "aegiusb",
                Data = base64,
                DataLen = info.Length
            };
            
            return client.SentenceRecognitionSync(req);
        }
        public static TextTranslateResponse TranslateText(string text)
        {
            Credential cred = new()
            {
                SecretId = Config.GetConfig<string>("SecretId"),
                SecretKey = Config.GetConfig<string>("SecretKey")
            };

            ClientProfile clientProfile = new();
            HttpProfile httpProfile = new();
            httpProfile.Endpoint = ("tmt.tencentcloudapi.com");
            clientProfile.HttpProfile = httpProfile;

            TmtClient client = new(cred, "ap-beijing", clientProfile);
            TextTranslateRequest req = new()
            {
                SourceText = text,
                Source = "ja",
                Target = "zh",
                ProjectId = 0
            };
            return client.TextTranslateSync(req);
        }
    }
}
