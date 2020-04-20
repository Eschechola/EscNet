using ESCHENet.Crypto.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESCHENet.Tests.Crypto
{
    [TestClass]
    public class CryptoTests
    {
        [TestMethod]
        public void ExecuteRijndaelCryptoTest()
        {
            var key = "hgndtyuytrgbvfrerkjhugyt";
            var phrase = "Frase para ser descritpografada";

            var rijndael = new RijndaelCrypto(key);

            var cryptographedPhrase = rijndael.Encrypt(phrase);
            var decryptographedPhrase = rijndael.Decrypt(cryptographedPhrase);

            if(decryptographedPhrase == phrase)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void ExecuteCaesarCipherTest()
        {
            int keyup = 3;
            var phrase = "Frase para ser descritpografada";

            var caesarCipher = new CaesarCipherCrypto(keyup);

            var cryptographedPhrase = caesarCipher.Encrypt(phrase);
            var decryptographedPhrase = caesarCipher.Decrypt(cryptographedPhrase);

            if (decryptographedPhrase == phrase)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }


        [TestMethod]
        public void ExecuteSha1Test()
        {
            var phrase = "Frase para ser resumida";
            var sha1 = new Sha1();

            sha1.Generate(phrase);

            if (!string.IsNullOrEmpty(phrase))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }
    }
}
