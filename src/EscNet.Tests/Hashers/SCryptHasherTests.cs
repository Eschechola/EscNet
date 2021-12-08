using EscNet.Hashers.Algorithms;
using EscNet.Hashers.Interfaces;
using FluentAssertions;
using Scrypt;
using System;
using Xunit;

namespace EscNet.Tests.Hashers
{
    public class SCryptHasherTests
    {
        private readonly IHasher _sut;

        public SCryptHasherTests()
        {
            _sut = new SCryptHasher(new ScryptEncoder());
        }

        [Fact(DisplayName = "Hash when text is null or empty")]
        public void Hash_WhenTextIsNullOrEmpty_ThrowsNullReferenceException()
        {
            // Arrange & Act
            Func<string> act = () => _sut.Hash(string.Empty);

            // Assert
            act.Should()
                .Throw<NullReferenceException>();
        }


        [Fact(DisplayName = "Hash when text is valid")]
        public void Hash_WhenTextIsValid_ReturnsCorrectHash()
        {
            // Arrange
            var text = "Hello World!";
            var textHash = "$s2$16384$8$1$zKNQ51ZzRvP3yi4HORYxtvxpWs1bdAxBVEcpgI6ePyw=$pxrFoPZA16AVIdY0QBveCi84beIhFTyCcNS4XROHirc=";

            // Act
            var result = _sut.Hash(text);

            // Assert
            result.Should()
                .Equals(textHash);
        }

        [Fact(DisplayName = "VerifyHashedText when text is correct")]
        public void VerifyHashedText_WhenHashIsCorrret_ReturnsTrue()
        {
            // Arrange
            var text = "Hello World!";
            var textHash = "$s2$16384$8$1$zKNQ51ZzRvP3yi4HORYxtvxpWs1bdAxBVEcpgI6ePyw=$pxrFoPZA16AVIdY0QBveCi84beIhFTyCcNS4XROHirc=";

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
            var text = "Hello";
            var textHash = "$s2$16384$8$1$zKNQ51ZzRvP3yi4HORYxtvxpWs1bdAxBVEcpgI6ePyw=$pxrFoPZA16AVIdY0QBveCi84beIhFTyCcNS4XROHircc";

            // Act
            var result = _sut.VerifyHashedText(text, textHash);

            // Assert
            result.Should()
                .BeFalse();
        }
    }
}
