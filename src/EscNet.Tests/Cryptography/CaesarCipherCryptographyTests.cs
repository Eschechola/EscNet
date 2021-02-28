using EscNet.Cryptography.Algorithms;
using EscNet.Cryptography.Interfaces.Cryptography;
using EscNet.Shared.Exceptions;
using System;
using Xunit;

namespace EscNet.Tests.Cryptography
{
    public class CaesarCipherCryptographyTests
    {
        private readonly ICryptography _sut;
        private readonly int _keyUp;

        public CaesarCipherCryptographyTests()
        {
            _keyUp = 5;
            _sut = new CaesarCipherCryptography(_keyUp);
        }

        [Fact]
        public void CaesarCipher_ThorwsInvalidKeyUpExceptionWhenKeyUpKeyIsInvalid()
        {
            // Arrange
            int zeroKeyUp = 0;
            int negativeKeyUp = -2;

            // Act
            Action actZeroKeyUp = () => new CaesarCipherCryptography(zeroKeyUp);
            Action actNegativeKeyUp = () => new CaesarCipherCryptography(negativeKeyUp);

            // Assert
            Assert.Throws<InvalidKeyUpException>(actZeroKeyUp);
            Assert.Throws<InvalidKeyUpException>(actNegativeKeyUp);
        }

        [Fact]
        public void CaesarCipher_Encrypt_ReturnsCorrectEncryptedText()
        {
            // Arrange
            string text = "Hello World!";
            string correctEncryptedText = @"Mjqqt \twqi&";

            // Act
            var result = _sut.Encrypt(text);

            // Assert
            Assert.Equal(correctEncryptedText, result);
        }

        [Fact]
        public void CaesarCipher_Encrypt_ThrowExceptionWhenTextToEncryptIsInvalid()
        {
            // Arrange & Act
            Func<string> act = () => _sut.Encrypt(string.Empty);

            // Assert
            Assert.Throws<NullReferenceException>(act);
        }


        [Fact]
        public void CaesarCipher_Decrypt_ReturnsCorrectDecryptedText()
        {
            // Arrange
            string text = @"Mjqqt \twqi&";
            string correctDecryptedText = "Hello World!";

            // Act
            var result = _sut.Decrypt(text);

            // Assert
            Assert.Equal(correctDecryptedText, result);
        }

        [Fact]
        public void CaesarCipher_Decrypt_ThrowExceptionWhenTextToDecryptIsInvalid()
        {
            //Arrange & Act
            Func<string> act = () => _sut.Decrypt(string.Empty);

            // Assert
            Assert.Throws<NullReferenceException>(act);
        }

        [Fact]
        public void CaesarCipher_EncryptAndDecrypt_ReturnsCorrectTexts()
        {
            // Arrange
            string decryptedText = "Hello World!";
            string encryptedText = @"Mjqqt \twqi&";

            // Act
            var encryptResult = _sut.Encrypt(decryptedText);
            var decryptResult = _sut.Decrypt(encryptedText);

            // Assert
            Assert.Equal(encryptedText, encryptResult);
            Assert.Equal(decryptedText, decryptResult);
        }
    }
}
