using System;
using System.Text;
using ESCHENet.Crypto.Interfaces;
using System.Security.Cryptography;

namespace ESCHENet.Crypto.Functions
{
    public class Sha1 : IHash
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
    }
}
