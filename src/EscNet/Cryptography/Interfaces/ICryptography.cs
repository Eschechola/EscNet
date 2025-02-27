namespace EscNet.Cryptography.Interfaces;

public interface ICryptography
{
    string Encrypt(string text);
    string Decrypt(string text);
}