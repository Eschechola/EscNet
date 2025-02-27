using EscNet.Shared.Exceptions;
using EscNet.Shared.Validators;
using System;
using EscNet.Cryptography.Interfaces;

namespace EscNet.Cryptography.Algorithms;

public class CaesarCipherCryptography : ICaesarCipherCryptography
{
    private readonly int _keyUp;

    public CaesarCipherCryptography(int keyup)
    {
        ValidateKeyUp(keyup);
        _keyUp = keyup;
    }

    public string Encrypt(string text)
    {
        Validator.ValidateStringIsNotNullOrEmpty(text);

        var encryptedText = string.Empty;

        foreach (var t in text)
        {
            // ignore blank spaces
            if (t == ' ')
            {
                encryptedText += ' ';
                continue;
            }

            var character = Convert.ToChar(t);
            int asciiNumber = character;

            encryptedText += (char)(asciiNumber + _keyUp);
        }

        return encryptedText;
    }

    public string Decrypt(string text)
    {
        Validator.ValidateStringIsNotNullOrEmpty(text);

        var decryptedText = string.Empty;

        foreach (var t in text)
        {
            // ignore blank spaces
            if (t == ' ')
            {
                decryptedText += ' ';
                continue;
            }

            var character = Convert.ToChar(t);
            int asciiNumber = character;

            decryptedText += (char)(asciiNumber - _keyUp);
        }

        return decryptedText;
    }

    private static void ValidateKeyUp(int keyup)
    {
        if (keyup <= 0)
            throw new InvalidKeyUpException("Keyup must be greater than or equal 1");
    }
}