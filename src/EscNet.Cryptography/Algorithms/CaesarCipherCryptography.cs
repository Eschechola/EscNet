using EscNet.Cryptography.Interfaces.Cryptography;
using EscNet.Shared.Exceptions;
using EscNet.Shared.Validators;
using System;

namespace EscNet.Cryptography.Algorithms
{
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

            string encriptedText = string.Empty;
            char character = ' ';
            int asciiNumber = 0;

            for (int i = 0; i < text.Length; i++)
            {
                //ignore blank spaces
                if (text[i] == ' ')
                {
                    encriptedText += ' ';
                    continue;
                }

                character = Convert.ToChar(text[i]);
                asciiNumber = character;

                encriptedText += (char)(asciiNumber + _keyUp);
            }

            return encriptedText;
        }

        public string Decrypt(string text)
        {
            Validator.ValidateStringIsNotNullOrEmpty(text);

            string decryptedText = string.Empty;
            char character = ' ';
            int asciiNumber = 0;

            for (int i = 0; i < text.Length; i++)
            {
                //ignore blank spaces
                if (text[i] == ' ')
                {
                    decryptedText += ' ';
                    continue;
                }

                character = Convert.ToChar(text[i]);
                asciiNumber = character;

                decryptedText += (char)(asciiNumber - _keyUp);
            }

            return decryptedText;
        }

        private void ValidateKeyUp(int keyup)
        {
            if (keyup <= 0)
                throw new InvalidKeyUpException("Keyup must be greater than or equal 1");
        }
    }
}
