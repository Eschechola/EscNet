using Bogus.DataSets;
using EscNet.Cryptography.Algorithms;
using EscNet.Shared.Exceptions;
using FluentAssertions;
using System;
using EscNet.Cryptography.Interfaces;
using Xunit;

namespace EscNet.Tests.Cryptography;

public class RijndaelCryptographyTests
{
    private readonly ICryptography _sut;

    public RijndaelCryptographyTests()
    {
        const string encryptionKey = "fe78866aecf848da80d19d46494172ab";
        _sut = new RijndaelCryptography(encryptionKey);
    }

    [Fact(DisplayName = "RijndaelCryptography instance when key is invalid")]
    public void RijndaelCryptography_ThrowsWrongEncryptionKeyExceptionWhenEncryptionKeyIsInvalid()
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
        actEmpty.Should()
            .Throw<WrongEncryptionKeyException>();

        actNotEightMultiple.Should()
            .Throw<WrongEncryptionKeyException>();

        actToLong.Should()
            .Throw<WrongEncryptionKeyException>();
    }

    [Fact(DisplayName = "Encrypt when text is valid")]
    public void Encrypt_WhenTextIsValid_ReturnsCorrectEncryptedText()
    {
        // Arrange
        const string text = "Hello World!";
        const string correctEncryptedText = "HZdioE5eNm2hgG98DRBqqA==";

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

    [Fact(DisplayName = "Encrypt when text is valid")]
    public void Decrypt_WhenTextIsValid_ReturnsCorrectDecryptedText()
    {
        // Arrange
        const string text = "HZdioE5eNm2hgG98DRBqqA==";
        const string correctDecryptedText = "Hello World!";

        // Act
        var result = _sut.Decrypt(text);

        // Assert
        result.Should()
            .BeEquivalentTo(correctDecryptedText);
    }

    [Fact(DisplayName = "Encrypt when text is null or empty")]
    public void RijndaelCryptography_Decrypt_ThrowExceptionWhenTextToDecryptIsInvalid()
    {
        //Arrange & Act
        var act = () => _sut.Decrypt(string.Empty);

        // Assert
        act.Should()
            .Throw<NullReferenceException>();
    }
}