using EscNet.Hashers.Algorithms;
using EscNet.Hashers.Interfaces;
using FluentAssertions;
using Isopoh.Cryptography.Argon2;
using System;
using System.Text;
using Xunit;

namespace EscNet.Tests.Hashers
{
    public class Argon2IdHasherTests
    {
        private readonly IHasher _sut;
        private readonly int _lanes = 5;
        private readonly int _memoryCost = 32768;
        private readonly int _timeCost = 10;
        private readonly string _salt = "zlqAUART49m5CI0qC1IQKpDiMjQLSRss707vvczOWs0eVsJe1XG8Y6d5umAqJlABGF5wZ1fa5GddlYOUoBNXJzp2mSJ4sSR6Apni";

        public Argon2IdHasherTests()
        {
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = _timeCost,
                MemoryCost = _memoryCost, 
                Lanes = _lanes,
                Threads = Environment.ProcessorCount,
                Salt = Encoding.UTF8.GetBytes(_salt),
                HashLength = 20
            };

            _sut = new Argon2IdHasher(config);
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
            var textHash = "$argon2i$v=19$m=32768,t=10,p=5$emxxQVVBUlQ0OW01Q0kwcUMxSVFLcERpTWpRTFNSc3M3MDd2dmN6T1dzMGVWc0plMVhHOFk2ZDV1bUFxSmxBQkdGNXdaMWZhNUdkZGxZT1VvQk5YSnpwMm1TSjRzU1I2QXBuaQ$QU2/EK1cSb793eRkMYiDrWhRh7s";

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
            var textHash = "$argon2i$v=19$m=32768,t=10,p=5$emxxQVVBUlQ0OW01Q0kwcUMxSVFLcERpTWpRTFNSc3M3MDd2dmN6T1dzMGVWc0plMVhHOFk2ZDV1bUFxSmxBQkdGNXdaMWZhNUdkZGxZT1VvQk5YSnpwMm1TSjRzU1I2QXBuaQ$QU2/EK1cSb793eRkMYiDrWhRh7s";

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
