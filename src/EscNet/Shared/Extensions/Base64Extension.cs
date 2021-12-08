using System;
using System.Text;

namespace EscNet.Shared.Extensions
{
    public static class Base64Extension
    {
        public static string ToBase64(this byte[] bytes)
            => Convert.ToBase64String(bytes);

        public static string ToBase64(this string text)
            => Convert.ToBase64String(Encoding.ASCII.GetBytes(text));
        
        public static byte[] FromBase64(this string text)
            => Convert.FromBase64String(text);
    }
}