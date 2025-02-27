using System;
using EscNet.Hashes.Algorithms;
using EscNet.Hashes.Interfaces;
using FluentAssertions;
using Xunit;

namespace EscNet.Tests.Hashes;

public class BCryptHasherTests
{
    private readonly IHasher _sut = new BCryptHasher("$2a$11$s8BuDSLuoQuYQiiGz1YwVO");

    [Fact(DisplayName = "Hash when text is null or empty")]
    public void Hash_WhenTextIsNullOrEmpty_ThrowsNullReferenceException()
    {
        // Arrange & Act
        var act = () => _sut.Hash(string.Empty);

        // Assert
        act.Should()
            .Throw<NullReferenceException>();
    }


    [Fact(DisplayName = "Hash when text is valid")]
    public void Hash_WhenTextIsValid_ReturnsCorrectHash()
    {
        // Arrange
        const string text = "Hello World!";
        const string textHash = "$2a$11$s8BuDSLuoQuYQiiGz1YwVO/BVo6RZvx.kQ74DpemrSyUbMhKNxc02";

        // Act
        var result = _sut.Hash(text);

        // Assert
        result.Should()
            .BeEquivalentTo(textHash);
    }

    [Fact(DisplayName = "VerifyHashedText when text is correct")]
    public void VerifyHashedText_WhenHashIsCorrect_ReturnsTrue()
    {
        // Arrange
        const string text = "Hello World!";
        const string textHash = "$2a$11$s8BuDSLuoQuYQiiGz1YwVO/BVo6RZvx.kQ74DpemrSyUbMhKNxc02";

        // Act
        var result = _sut.VerifyHashedText(text, textHash);

        // Assert
        result.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "VerifyHashedText when text is incorrect")]
    public void VerifyHashedText_WhenHashIsIncorrect_ReturnsFalse()
    {
        // Arrange
        const string text = "Hello";
        const string textHash = "2ef7bde608ce5404e97d5f042f95f89f1c232871";

        // Act
        var result = _sut.VerifyHashedText(text, textHash);

        // Assert
        result.Should()
            .BeFalse();
    }
}