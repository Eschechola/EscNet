using EscNet.Cryptography.Algorithms;
using EscNet.Cryptography.Interfaces.Hash;
using System;
using Xunit;

namespace EscNet.Tests.Cryptography
{
    public class Sha1HashTests
    {
        private readonly IHash _sut;

        public Sha1HashTests()
        {
            _sut = new Sha1Hash();
        }

        [Fact]
        public void Sha1Hash_GenerateHash_ThrowNullReferenceExceptionWhenTextIsEmpty()
        {
            // Arrange & Act
            Func<string> act = () => _sut.GenerateHash(string.Empty);

            // Assert
            Assert.Throws<NullReferenceException>(act);
        }


        [Fact]
        public void Sha1Hash_GenerateHash_ReturnsCorrectHash()
        {
            // Arrange
            var text = "Hello World!";
            var textHash = "2ef7bde608ce5404e97d5f042f95f89f1c232871";

            // Act
            var result = _sut.GenerateHash(text);

            // Assert
            Assert.Equal(textHash, result);
        }
    }
}
