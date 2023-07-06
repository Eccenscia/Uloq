
using System.Security.Cryptography;
using System.Text;

namespace System
{
    internal static class StringExtensions
    {
        public static byte[] ToSha256(this string value)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return value.GetBytes().ToSha256();
            }
        }

        public static string ToSha256Base64String(this string value)
        {
            return value.ToSha256().ToBase64String();
        }

        public static byte[] GetBytes(this string value)
        {
            if (value != null)
                return value.GetUTF8Bytes();
            else
                return new byte[0];
        }

        public static byte[] FromBase64String(this string value)
        {
            return Convert.FromBase64String(value);
        }

        public static byte[] GetUTF8Bytes(this string value)
        {
            if (value != null)
                return Encoding.UTF8.GetBytes(value);
            else
                return new byte[0];
        }

        public static byte[] GetASCIIBytes(this string value)
        {
            if (value != null)
                return Encoding.ASCII.GetBytes(value);
            else
                return new byte[0];
        }

       
    }
}