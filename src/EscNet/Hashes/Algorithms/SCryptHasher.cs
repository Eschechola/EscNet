using EscNet.Hashes.Interfaces.Algorithms;
using EscNet.Shared.Validators;
using Scrypt;

namespace EscNet.Hashes.Algorithms;

public class SCryptHasher(ScryptEncoder encoder) : Hasher, ISCryptHasher
{
    public override string Hash(string text)
    {
        Validator.ValidateStringIsNotNullOrEmpty(text);
        return encoder.Encode(text);
    }

    public override bool VerifyHashedText(string text, string hashedText)
        => encoder.Compare(text, hashedText);
}