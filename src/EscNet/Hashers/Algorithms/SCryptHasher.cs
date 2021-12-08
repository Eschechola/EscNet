using EscNet.Hashers.Interfaces.Algorithms;
using EscNet.Shared.Validators;
using Scrypt;

namespace EscNet.Hashers.Algorithms
{
    public class SCryptHasher : ISCryptHasher
    {
        private readonly ScryptEncoder _encoder;
        public SCryptHasher(ScryptEncoder encoder)
        {
            _encoder = encoder;
        }

        public string Hash(string text)
        {
            Validator.ValidateStringIsNotNullOrEmpty(text);
            return _encoder.Encode(text);
        }

        public bool VerifyHashedText(string text, string hashedText)
            => _encoder.Compare(text, hashedText);
    }
}
