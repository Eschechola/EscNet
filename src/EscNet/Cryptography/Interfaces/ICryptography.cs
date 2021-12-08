namespace EscNet.Cryptography.Interfaces.Cryptography
{
    public interface ICryptography
    {
        string Encrypt(string text);
        string Decrypt(string text);
    }
}