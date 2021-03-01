using EscNet.Mails.Enums;
using EscNet.Shared.Exceptions;

namespace EscNet.Mails.Factory
{
    public static class SMTPFactory
    {
        public static string GetSMTPUrl(SMTPType smtpType)
        {
            switch (smtpType)
            {
                case SMTPType.Gmail:
                    return "smtp.gmail.com";

                case SMTPType.Outlook:
                    return "smtp-mail.outlook.com";

                default:
                    throw new InvalidSMTPException("The SMTP Type doesn't not exists em EscNet package!");
            }
        }
    }
}
