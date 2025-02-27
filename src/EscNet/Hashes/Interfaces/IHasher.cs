namespace EscNet.Hashes.Interfaces;

public interface IHasher
{
    string Hash(string text);
    bool VerifyHashedText(string text, string hashedText);
}