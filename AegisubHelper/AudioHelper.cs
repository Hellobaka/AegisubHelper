using NAudio.Wave;
using System;
using System.IO;
using System.Threading;

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
            writer = new WaveFileWriter(outputFilePath, capture.WaveFormat = new(16000, 16, 1));
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
    }
}
