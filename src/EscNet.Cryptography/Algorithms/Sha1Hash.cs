using EscNet.Cryptography.Interfaces.Hash;
using System;
using System.Security.Cryptography;
using System.Text;

namespace EscNet.Cryptography.Algorithms
{
    public class Sha1Hash : ISha1Hash
    {
        private readonly SHA1CryptoServiceProvider sha1CryptoTransform;

        public Sha1Hash()
        {
            sha1CryptoTransform = new SHA1CryptoServiceProvider();
        }

        public string GenerateHash(string text)
        {
            ValidateText(text);

            byte[] buffer = Encoding.Default.GetBytes(text);
            string report = BitConverter.ToString(sha1CryptoTransform.ComputeHash(buffer))
                                .Replace("-", "")
                                .ToLower();

            return report;
        }

        private void ValidateText(string encryptionText)
        {
            if (string.IsNullOrEmpty(encryptionText))
                throw new NullReferenceException("The text cannot be null or empty.");
        }
    }
}
