using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using ESCHENet.Crypto.Interfaces;

namespace ESCHENet.Crypto.Functions
{
    public class RijndaelCrypto : ICrypto
    {
        private static readonly byte[] Bytes = { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18, 0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };
        private static string CryptoKey = string.Empty;

        public RijndaelCrypto(string cryptoKey)
        {
            CryptoKey = cryptoKey;
        }

        //palavra new oculta o método herdado Decrypt
        public string Encrypt(string text)
        {
            try
            {
                // Se a string não está vazia, executa a criptografia
                if (!string.IsNullOrEmpty(text))
                {
                    // Cria instancias de vetores de bytes com as chaves
                    byte[] bytesText;
                    byte[] bytesKey;

                    bytesKey = Convert.FromBase64String(CryptoKey);
                    bytesText = new UTF8Encoding().GetBytes(text);

                    // Instancia a classe de criptografia Rijndael
                    RijndaelManaged rijndael = new RijndaelManaged();

                    // Define o tamanho da chave "256 = 8 * 32"
                    // Lembre-se: chaves possíves:
                    // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)

                    rijndael.KeySize = 256;

                    // Cria o espaço de memória para guardar o valor criptografado:
                    MemoryStream memoryStream = new MemoryStream();

                    // Instancia o encriptador 
                    CryptoStream encryptor = new CryptoStream(memoryStream, rijndael.CreateEncryptor(bytesKey, Bytes), CryptoStreamMode.Write);

                    // Faz a escrita dos dados criptografados no espaço de memória
                    encryptor.Write(bytesText, 0, bytesText.Length);

                    // Despeja toda a memória.
                    encryptor.FlushFinalBlock();

                    // Pega o vetor de bytes da memória e gera a string criptografada
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
                else
                {
                    // Se a string for vazia retorna nulo
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        //palavra new oculta o método herdado Decrypt
        public string Decrypt(string text)
        {
            try
            {
                // Se a string não está vazia, executa a criptografia
                if (!string.IsNullOrEmpty(text))
                {
                    // Cria instancias de vetores de bytes com as chaves
                    byte[] bytesText;
                    byte[] bytesKey;

                    bytesKey = Convert.FromBase64String(CryptoKey);
                    bytesText = Convert.FromBase64String(text);

                    // Instancia a classe de criptografia Rijndael
                    RijndaelManaged rijndael = new RijndaelManaged();

                    // Define o tamanho da chave "256 = 8 * 32"
                    // Lembre-se: chaves possíves:
                    // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)
                    rijndael.KeySize = 256;

                    // Cria o espaço de memória para guardar o valor DEScriptografado:
                    MemoryStream memoryStream = new MemoryStream();

                    // Instancia o Decriptador 

                    CryptoStream decryptor = new CryptoStream(memoryStream, rijndael.CreateDecryptor(bytesKey, Bytes), CryptoStreamMode.Write);

                    // Faz a escrita dos dados criptografados no espaço de memória
                    decryptor.Write(bytesText, 0, bytesText.Length);

                    // Despeja toda a memória.
                    decryptor.FlushFinalBlock();

                    // Instancia a classe de codificação para que a string venha de forma correta
                    UTF8Encoding utf8 = new UTF8Encoding();

                    // Com o vetor de bytes da memória, gera a string descritografada em UTF8
                    return utf8.GetString(memoryStream.ToArray());
                }
                else
                {
                    // Se a string for vazia retorna nulo
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                // Se a string for vazia retorna nulo
                return string.Empty;
            }
        }
    }
}
