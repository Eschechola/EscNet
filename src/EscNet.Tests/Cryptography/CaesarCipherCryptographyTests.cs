using EscNet.Cryptography.Algorithms;
using EscNet.Shared.Exceptions;
using FluentAssertions;
using System;
using EscNet.Cryptography.Interfaces;
using Xunit;

namespace EscNet.Tests.Cryptography;

public class CaesarCipherCryptographyTests
{
    private readonly ICryptography _sut;

    public CaesarCipherCryptographyTests()
    {
        const int keyUp = 5;
        _sut = new CaesarCipherCryptography(keyUp);
    }

    [Fact(DisplayName = "CaesarCipher instance when keyup is invalid")]
    public void CaesarCipher_ThrowsInvalidKeyUpExceptionWhenKeyUpKeyIsInvalid()
    {
        // Arrange
        const int zeroKeyUp = 0;
        const int negativeKeyUp = -2;

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
        const string text = "Hello World!";
        const string correctEncryptedText = @"Mjqqt \twqi&";

        // Act
        var result = _sut.Encrypt(text);

        // Assert
        result.Should()
            .BeEquivalentTo(correctEncryptedText);
    }

    [Fact(DisplayName = "Encrypt when text is null or empty")]
    public void Encrypt_WhenTextIsNullOrEmpty_ThrowsException()
    {
        // Arrange & Act
        var act = () => _sut.Encrypt(string.Empty);

        // Assert
        act.Should()
            .Throw<NullReferenceException>();
    }


    [Fact(DisplayName = "Decrypt when text is correct")]
    public void Decrypt_WhenTextIsCorrect_ReturnsCorrectDecryptedText()
    {
        // Arrange
        const string text = @"Mjqqt \twqi&";
        const string correctDecryptedText = "Hello World!";

        // Act
        var result = _sut.Decrypt(text);

        // Assert
        result.Should()
            .BeEquivalentTo(correctDecryptedText);
    }

    [Fact(DisplayName = "Decrypt when text is null or empty")]
    public void Decrypt_WhenTextIsNullOrEmpty_ThrowsException()
    {
        //Arrange & Act
        var act = () => _sut.Decrypt(string.Empty);

        // Assert
        act.Should()
            .Throw<NullReferenceException>();
    }
}