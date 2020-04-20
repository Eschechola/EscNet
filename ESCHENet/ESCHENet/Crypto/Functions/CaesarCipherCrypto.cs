using System;
using ESCHENet.Crypto.Interfaces;

namespace ESCHENet.Crypto.Functions
{
    public class CaesarCipherCrypto : ICrypto
    {
        private static int Keyup;

        public CaesarCipherCrypto(int keyup)
        {
            Keyup = keyup;
        }

        //palavra new oculta o método herdado Decrypt
        public string Encrypt(string text)
        {

            string encriptedText = "";
            char character;
            int asciiNumber;

            for (int i = 0; i < text.Length; i++)
            {
                character = ' ';
                asciiNumber = 0;

                //ignora os espaços em branco
                if (text[i] == ' ')
                {
                    encriptedText += ' ';
                    continue;
                }

                character = Convert.ToChar(text[i]);
                asciiNumber = character;

                encriptedText += (char)(asciiNumber + Keyup);
            }

            return encriptedText;
        }

        //palavra new oculta o método herdado Decrypt
        public string Decrypt(string text)
        {
            string textDecripted = "";
            char character;
            int asciiNumber;

            for (int i = 0; i < text.Length; i++)
            {
                character = ' ';
                asciiNumber = 0;

                //ignora os espaços em branco
                if (text[i] == ' ')
                {
                    textDecripted += ' ';
                    continue;
                }

                character = Convert.ToChar(text[i]);
                asciiNumber = character;

                textDecripted += (char)(asciiNumber - Keyup);
            }

            return textDecripted;
        }
    }
}
