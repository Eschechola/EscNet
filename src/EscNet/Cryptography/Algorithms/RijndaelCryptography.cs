using EscNet.Cryptography.Interfaces;
using EscNet.Shared.Exceptions;
using EscNet.Shared.Extensions;
using EscNet.Shared.Validators;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EscNet.Cryptography.Algorithms
{
    public class RijndaelCryptography : IRijndaelCryptography
    {
        private readonly byte[] _encryptionBytes = { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18, 0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };
        private readonly string _encryptionKey;

        public RijndaelCryptography(string encryptionKey)
        {
            ValidateEncryptionKey(encryptionKey);
            _encryptionKey = encryptionKey.ToBase64();
        }

        public string Encrypt(string text)
        {
            Validator.ValidateStringIsNotNullOrEmpty(text);

            byte[] encryptionKeyBytes = _encryptionKey.FromBase64();
            byte[] textBytes = new UTF8Encoding().GetBytes(text);

            var rijndaelAlgorithm = new RijndaelManaged();
            rijndaelAlgorithm.KeySize = 256;

            var memoryStream = new MemoryStream();

            var encryptor = new CryptoStream(
                memoryStream,
                rijndaelAlgorithm.CreateEncryptor(encryptionKeyBytes, _encryptionBytes),
                CryptoStreamMode.Write);

            encryptor.Write(textBytes, 0, textBytes.Length);
            encryptor.FlushFinalBlock();

            return memoryStream.ToArray().ToBase64();
        }

        public string Decrypt(string text)
        {
            Validator.ValidateStringIsNotNullOrEmpty(text);

            byte[] encryptionKeyBytes = _encryptionKey.FromBase64();
            byte[] textBytes = text.FromBase64();

            var rijndaelAlgorithm = new RijndaelManaged();
            rijndaelAlgorithm.KeySize = 256;

            var memoryStream = new MemoryStream();

            var decryptor = new CryptoStream(
                memoryStream,
                rijndaelAlgorithm.CreateDecryptor(encryptionKeyBytes, _encryptionBytes),
                CryptoStreamMode.Write);

            decryptor.Write(textBytes, 0, textBytes.Length);
            decryptor.FlushFinalBlock();

            var utf8 = new UTF8Encoding();

            return utf8.GetString(memoryStream.ToArray());
        }

        private void ValidateEncryptionKey(string encryptionKey)
        {
            if(string.IsNullOrEmpty(encryptionKey))
                throw new WrongEncryptionKeyException("The encryption key can not be null or empty.");

            if(encryptionKey.Length % 8 != 0)
                throw new WrongEncryptionKeyException("The encryption key needs to be a multiple of eight.");

            if(encryptionKey.Length > 256)
                throw new WrongEncryptionKeyException("The encryption key lenght cannot be greater than 256.");
        }
    }
}