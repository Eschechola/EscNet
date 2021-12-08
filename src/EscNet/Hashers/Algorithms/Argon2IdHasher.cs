using EscNet.Hashers.Interfaces.Algorithms;
using EscNet.Shared.Validators;
using Isopoh.Cryptography.Argon2;
using System.Text;

namespace EscNet.Hashers.Algorithms
{
    public class Argon2IdHasher : IArgon2IdHasher
    {
        private readonly Argon2Config _argon2Config;

        public Argon2IdHasher(Argon2Config argon2Config)
        {
            _argon2Config = argon2Config;
        }

        public string Hash(string text)
        {
            Validator.ValidateStringIsNotNullOrEmpty(text);

            _argon2Config.Password = Encoding.UTF8.GetBytes(text);
            var argon2A = new Argon2(_argon2Config);

            return _argon2Config.EncodeString(argon2A.Hash().Buffer);
        }

        public bool VerifyHashedText(string text, string hashedText)
            => hashedText == Hash(text);
    }
}
