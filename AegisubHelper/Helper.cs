using System;
using System.Security.Cryptography;
using System.Text;

namespace AegisubHelper
{
    public static class Helper
    {
        public static string HMACSHA256(string message, string secret)
        {
            secret ??= "";
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using var hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
        public static string SHA256(string message)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(message);
            byte[] hashedBytes = new SHA256CryptoServiceProvider().ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
        public static string MD5Encrypt(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using var md5 = MD5.Create();
            byte[] hashmessage = md5.ComputeHash(messageBytes);
            return BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
        }
        public static long TimeStamp => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        public static long TimeStampms => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }
}
