using EscNet.Mails.Enums;
using EscNet.Shared.Exceptions;

namespace EscNet.Mails.Factory;

public static class SMTPFactory
{
    public static string GetSMTPUrl(SMTPType smtpType)
    {
        return smtpType switch
        {
            SMTPType.Gmail => "smtp.gmail.com",
            SMTPType.Outlook => "smtp-mail.outlook.com",
            _ => throw new InvalidSMTPException("The SMTP Type doesn't not exists em EscNet package!")
        };
    }
}