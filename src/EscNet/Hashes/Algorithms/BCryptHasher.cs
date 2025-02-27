using EscNet.Hashes.Interfaces.Algorithms;
using EscNet.Shared.Validators;

namespace EscNet.Hashes.Algorithms;

public class BCryptHasher(string salt) : Hasher, IBCryptHasher
{
    public override string Hash(string text)
    {
        Validator.ValidateStringIsNotNullOrEmpty(text);
        return BCrypt.Net.BCrypt.HashPassword(text, salt);
    }
}