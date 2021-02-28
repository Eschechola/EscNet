using Bogus.DataSets;
using EscNet.Cryptography.Algorithms;
using EscNet.Cryptography.Interfaces.Cryptography;
using EscNet.Shared.Exceptions;
using System;
using Xunit;

namespace EscNet.Tests.Cryptography
{
    public class RijndaelCryptographyTests
    {
        private readonly ICryptography _sut;
        private readonly string _encryptionKey;

        public RijndaelCryptographyTests()
        {
            _encryptionKey = "fe78866aecf848da80d19d46494172ab";
            _sut = new RijndaelCryptography(_encryptionKey);
        }

        [Fact]
        public void RijndaelCryptography_ThorwsWrongEncryptionKeyExceptionWhenEncryptionKeyIsInvalid()
        {
            // Arrange
            var keyEmpty = string.Empty;
            var keyNotEightMultiple = new Lorem().Letter(5);
            var keyToLong = new Lorem().Letter(121);

            // Act
            Action actEmpty = () => new RijndaelCryptography(keyEmpty);
            Action actNotEightMultiple = () => new RijndaelCryptography(keyNotEightMultiple);
            Action actToLong = () => new RijndaelCryptography(keyToLong);

            // Assert
            Assert.Throws<WrongEncryptionKeyException>(actEmpty);
            Assert.Throws<WrongEncryptionKeyException>(actNotEightMultiple);
            Assert.Throws<WrongEncryptionKeyException>(actToLong);
        }

        [Fact]
        public void RijndaelCryptography_Encrypt_ReturnsCorrectEncryptedText()
        {
            // Arrange
            string text = "Hello World!";
            string correctEncryptedText = "HZdioE5eNm2hgG98DRBqqA==";

            // Act
            var result = _sut.Encrypt(text);

            // Assert
            Assert.Equal(correctEncryptedText, result);
        }

        [Fact]
        public void RijndaelCryptography_Encrypt_ThrowExceptionWhenTextToEncryptIsInvalid()
        {
            // Arrange & Act
            Func<string> act = () => _sut.Encrypt(string.Empty);

            // Assert
            Assert.Throws<NullReferenceException>(act);
        }


        [Fact]
        public void RijndaelCryptography_Decrypt_ReturnsCorrectDecryptedText()
        {
            // Arrange
            string text = "HZdioE5eNm2hgG98DRBqqA==";
            string correctDecryptedText = "Hello World!";

            // Act
            var result = _sut.Decrypt(text);

            // Assert
            Assert.Equal(correctDecryptedText, result);
        }

        [Fact]
        public void RijndaelCryptography_Decrypt_ThrowExceptionWhenTextToDecryptIsInvalid()
        {
            //Arrange & Act
            Func<string> act = () => _sut.Decrypt(string.Empty);

            // Assert
            Assert.Throws<NullReferenceException>(act);
        }

        [Fact]
        public void RijndaelCryptography_EncryptAndDecrypt_ReturnsCorrectTexts()
        {
            // Arrange
            string decryptedText = "Hello World!";
            string encryptedText = "HZdioE5eNm2hgG98DRBqqA==";

            // Act
            var encryptResult = _sut.Encrypt(decryptedText);
            var decryptResult = _sut.Decrypt(encryptedText);

            // Assert
            Assert.Equal(encryptedText, encryptResult);
            Assert.Equal(decryptedText, decryptResult);
        }
    }
}
