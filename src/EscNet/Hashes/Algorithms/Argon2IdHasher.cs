using System.Text;
using EscNet.Hashes.Interfaces.Algorithms;
using EscNet.Shared.Validators;
using Isopoh.Cryptography.Argon2;

namespace EscNet.Hashes.Algorithms;

public class Argon2IdHasher(Argon2Config argon2Config) : Hasher, IArgon2IdHasher
{
    public override string Hash(string text)
    {
        Validator.ValidateStringIsNotNullOrEmpty(text);

        argon2Config.Password = Encoding.UTF8.GetBytes(text);
        var argon2A = new Argon2(argon2Config);

        return argon2Config.EncodeString(argon2A.Hash().Buffer);
    }
}