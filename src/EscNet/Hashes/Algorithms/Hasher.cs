namespace EscNet.Hashes.Algorithms;

public abstract class Hasher
{
    public abstract string Hash(string text);
    
    public virtual bool VerifyHashedText(string text, string hashedText)
        => hashedText == Hash(text);
}