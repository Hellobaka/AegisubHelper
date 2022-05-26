using Memory;
using System.Diagnostics;
using System.Linq;

namespace AegisubHelper
{
    public static class MemoryHelper
    {
        public static string PlayPattern { get; set; } = "00 00 00 00 BC C1 A0 00 00 00 00 00 00 00 00 00 00 00 00 00";
        public static Mem Memory { get; set; }
        public static long PlayMemory { get; set; }
        public static bool Init(Process process)
        {
            Memory = new Mem();
            return Memory.OpenProcess(process.Id);
        }
        public static bool SearchPlayMemory()
        {
            var ls = Memory.AoBScan(0x04000000, 0x05000000, PlayPattern, true, true);
            ls.Wait();
            if(ls.Result.Count() > 0)
            {
                PlayMemory = ls.Result.First();
                return true;
            }
            return false;
        }
        public static bool IsPlaying()
        {
            return Memory.ReadByte(PlayMemory.ToString("x0")) != 0;
        }
    }
}
