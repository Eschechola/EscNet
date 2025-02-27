namespace EscNet.Mails.Models;

public record Email
{
    public string Receiver { get; init; }
    public string Subject { get; init; }
    public string Body { get; init; }

    public Email()
    {
    }

    public Email(
        string receiver,
        string subject,
        string body)
    {
        Receiver = receiver;
        Subject = subject;
        Body = body;
    }
}