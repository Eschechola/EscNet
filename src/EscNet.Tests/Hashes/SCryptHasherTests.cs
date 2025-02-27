using System;
using EscNet.Hashes.Algorithms;
using EscNet.Hashes.Interfaces;
using FluentAssertions;
using Scrypt;
using Xunit;

namespace EscNet.Tests.Hashes;

public class SCryptHasherTests
{
    private readonly IHasher _sut = new SCryptHasher(new ScryptEncoder());

    [Fact(DisplayName = "Hash when text is null or empty")]
    public void Hash_WhenTextIsNullOrEmpty_ThrowsNullReferenceException()
    {
        // Arrange & Act
        var act = () => _sut.Hash(string.Empty);

        // Assert
        act.Should()
            .Throw<NullReferenceException>();
    }

    [Fact(DisplayName = "VerifyHashedText when text is correct")]
    public void VerifyHashedText_WhenHashIsCorrret_ReturnsTrue()
    {
        // Arrange
        const string text = "Hello World!";
        const string textHash = "$s2$16384$8$1$zKNQ51ZzRvP3yi4HORYxtvxpWs1bdAxBVEcpgI6ePyw=$pxrFoPZA16AVIdY0QBveCi84beIhFTyCcNS4XROHirc=";

        // Act
        var result = _sut.VerifyHashedText(text, textHash);

        // Assert
        result.Should()
            .BeTrue();
    }

    [Fact(DisplayName = "VerifyHashedText when text is incorrect")]
    public void VerifyHashedText_WhenHashIsIncorrret_ReturnsFalse()
    {
        // Arrange
        const string text = "Hello";
        const string textHash = "$s2$16384$8$1$zKNQ51ZzRvP3yi4HORYxtvxpWs1bdAxBVEcpgI6ePyw=$pxrFoPZA16AVIdY0QBveCi84beIhFTyCcNS4XROHircc";

        // Act
        var result = _sut.VerifyHashedText(text, textHash);

        // Assert
        result.Should()
            .BeFalse();
    }
}