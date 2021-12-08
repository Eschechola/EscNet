using EscNet.Cryptography.Algorithms;
using EscNet.Cryptography.Interfaces.Cryptography;
using EscNet.Shared.Exceptions;
using FluentAssertions;
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

        [Fact(DisplayName = "CaesarCipher instance when keyup is invalid")]
        public void CaesarCipher_ThorwsInvalidKeyUpExceptionWhenKeyUpKeyIsInvalid()
        {
            // Arrange
            int zeroKeyUp = 0;
            int negativeKeyUp = -2;

            // Act
            Action actZeroKeyUp = () => new CaesarCipherCryptography(zeroKeyUp);
            Action actNegativeKeyUp = () => new CaesarCipherCryptography(negativeKeyUp);

            // Assert
            actZeroKeyUp.Should()
                .Throw<InvalidKeyUpException>();

            actNegativeKeyUp.Should()
                .Throw<InvalidKeyUpException>();
        }

        [Fact(DisplayName = "Encrypt when text is correct")]
        public void Encrypt_WhenTextIsCorrect_ReturnsCorrectEncryptedText()
        {
            // Arrange
            string text = "Hello World!";
            string correctEncryptedText = @"Mjqqt \twqi&";

            // Act
            var result = _sut.Encrypt(text);

            // Assert
            result.Should()
                .Equals(correctEncryptedText);
        }

        [Fact(DisplayName = "Encrypt when text is null or empty")]
        public void Encrypt_WhenTextIsNullOrEmpty_ThrowsException()
        {
            // Arrange & Act
            Func<string> act = () => _sut.Encrypt(string.Empty);

            // Assert
            act.Should()
                .Throw<NullReferenceException>();
        }


        [Fact(DisplayName = "Dencrypt when text is correct")]
        public void Decrypt_WhenTextIsCorrect_ReturnsCorrectDecryptedText()
        {
            // Arrange
            string text = @"Mjqqt \twqi&";
            string correctDecryptedText = "Hello World!";

            // Act
            var result = _sut.Decrypt(text);

            // Assert
            result.Should()
                .Equals(correctDecryptedText);
        }

        [Fact(DisplayName = "Decrypt when text is null or empty")]
        public void Decrypt_WhenTextIsNullOrEmpty_ThrowsException()
        {
            //Arrange & Act
            Func<string> act = () => _sut.Decrypt(string.Empty);

            // Assert
            act.Should()
                .Throw<NullReferenceException>();
        }
    }
}
