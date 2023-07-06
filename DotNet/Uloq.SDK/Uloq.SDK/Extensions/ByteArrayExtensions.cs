using System.Security.Cryptography;
using System.Text;

namespace System
{
    internal static class ByteArrayExtensions
    {
        public static byte[] ToSha256(this byte[] array)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(array);
            }
        }

        public static string ToHexString(this byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string ToSha256HexString(this byte[] array)
        {
            return array.ToSha256().ToHexString();
        }

        public static string ToSha256Base64String(this byte[] array)
        {

            return array.ToSha256().ToBase64String();
            
        }

        public static string ToBase64String(this byte[] array)
        {
            return Convert.ToBase64String(array, Base64FormattingOptions.None);
        }

        public static string GetUTF8String(this byte[] array)
        {
            if (array == null)
                return null;
            return Encoding.UTF8.GetString(array);
        }

        public static string GetASCIIString(this byte[] array)
        {
            return Encoding.ASCII.GetString(array);
        }
    }
}