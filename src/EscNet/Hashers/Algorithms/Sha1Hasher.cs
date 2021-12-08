using System;
using System.Text;
using System.Security.Cryptography;
using EscNet.Shared.Validators;
using EscNet.Hashers.Interfaces.Algorithms;

namespace EscNet.Hashers.Algorithms
{
    public class Sha1Hasher : ISha1Hasher
    {
        private readonly SHA1CryptoServiceProvider sha1CryptoTransform;

        public Sha1Hasher()
        {
            sha1CryptoTransform = new SHA1CryptoServiceProvider();
        }

        public string Hash(string text)
        {
            Validator.ValidateStringIsNotNullOrEmpty(text);

            byte[] buffer = Encoding.Default.GetBytes(text);
            string report = BitConverter.ToString(sha1CryptoTransform.ComputeHash(buffer))
                                .Replace("-", "")
                                .ToLower();

            return report;
        }

        public bool VerifyHashedText(string text, string hashedText)
            => hashedText == Hash(text);
    }
}
