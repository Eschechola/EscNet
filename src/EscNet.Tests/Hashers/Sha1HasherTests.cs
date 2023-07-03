using EscNet.Hashers.Algorithms;
using EscNet.Hashers.Interfaces;
using FluentAssertions;
using System;
using Xunit;

namespace EscNet.Tests.Hashers
{
    public class Sha1HasherTests
    {
        private readonly IHasher _sut;

        public Sha1HasherTests()
        {
            _sut = new Sha1Hasher();
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
            var textHash = "2ef7bde608ce5404e97d5f042f95f89f1c232871";

            // Act
            var result = _sut.Hash(text);

            // Assert
            result.Should()
                .BeEquivalentTo(textHash);
        }

        [Fact(DisplayName = "VerifyHashedText when text is correct")]
        public void VerifyHashedText_WhenHashIsCorrret_ReturnsTrue()
        {
            // Arrange
            var text = "Hello World!";
            var textHash = "2ef7bde608ce5404e97d5f042f95f89f1c232871";

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
            var textHash = "2ef7bde608ce5404e97d5f042f95f89f1c232871";

            // Act
            var result = _sut.VerifyHashedText(text, textHash);

            // Assert
            result.Should()
                .BeFalse();
        }
    }
}
