namespace EscNet.Hashers.Interfaces
{
    public interface IHasher
    {
        string Hash(string text);
        bool VerifyHashedText(string text, string hashedText);
    }
}
