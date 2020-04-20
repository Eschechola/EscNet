using System;
using System.Text;
using ESCHENet.Crypto.Interfaces;
using System.Security.Cryptography;

namespace ESCHENet.Crypto.Functions
{
    public class Sha1 : ICrypto
    {
        private readonly SHA1CryptoServiceProvider cryptoTransformSHA1;

        public Sha1()
        {
            cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
        }

        public string Generate(string hash)
        {
            byte[] buffer = Encoding.Default.GetBytes(hash);
            string report = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer))
                                .Replace("-", "")
                                .ToLower();

            return report;
        }

        private new static string Encrypt(string text) => string.Empty;

        private new static string Decrypt(string text) => string.Empty;
    }
}
