using NAudio.Wave;
using System;
using System.IO;
using System.Threading;

namespace AegisubHelper
{
    public static class AudioHelper
    {
        private static WasapiLoopbackCapture capture = null;
        public static void StartRecord()
        {
            var outputFolder = "NAudio";
            Directory.CreateDirectory(outputFolder);
            var outputFilePath = Path.Combine(outputFolder, "recorded.wav");
            File.Delete(outputFilePath);
            capture = new WasapiLoopbackCapture();
            // optionally we can set the capture waveformat here: e.g. capture.WaveFormat = new WaveFormat(44100, 16,2);
            var writer = new WaveFileWriter(outputFilePath, capture.WaveFormat);
            capture.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
            };
            capture.RecordingStopped += (s, a) =>
            {
                writer.Dispose();
                writer = null;
                capture.Dispose();
            };
            capture.StartRecording();
        }
        public static void StopRecord()
        {
            capture.StopRecording();
        }
    }
}
