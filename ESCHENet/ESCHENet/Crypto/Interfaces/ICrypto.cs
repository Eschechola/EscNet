namespace ESCHENet.Crypto.Interfaces
{
    public interface ICrypto
    {
        string Encrypt(string text);
        string Decrypt(string text);
    }
}
