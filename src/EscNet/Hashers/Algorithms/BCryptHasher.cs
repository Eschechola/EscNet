using EscNet.Hashers.Interfaces.Algorithms;
using EscNet.Shared.Validators;

namespace EscNet.Hashers.Algorithms
{
    public class BCryptHasher : IBCryptHasher
    {
        private readonly string Salt;

        public BCryptHasher(string salt)
        {
            Salt = salt;
        }

        public string Hash(string text)
        {
            Validator.ValidateStringIsNotNullOrEmpty(text);
            return BCrypt.Net.BCrypt.HashPassword(text, Salt);
        }

        public bool VerifyHashedText(string text, string hashedText)
            => hashedText == Hash(text);
    }
}
