using System;
using System.Security.Cryptography;
using System.Text;
using EscNet.Hashes.Interfaces.Algorithms;
using EscNet.Shared.Validators;

namespace EscNet.Hashes.Algorithms;

public class Sha1Hasher : Hasher, ISha1Hasher
{
    private readonly SHA1CryptoServiceProvider sha1CryptoTransform = new();

    public override string Hash(string text)
    {
        Validator.ValidateStringIsNotNullOrEmpty(text);

        var buffer = Encoding.Default.GetBytes(text);
        var report = BitConverter.ToString(sha1CryptoTransform.ComputeHash(buffer))
            .Replace("-", "")
            .ToLower();

        return report;
    }
}