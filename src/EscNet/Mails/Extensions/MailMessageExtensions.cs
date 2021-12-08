using System.Collections.Generic;
using System.Net.Mail;

namespace EscNet.Mails.Extensions
{
    public static class MailMessageExtensions
    {
        public static MailMessage AddCopyEmails(this MailMessage message, List<string> emailsToCopy)
        {
            for(int i = 0; i < emailsToCopy.Count; i++)
                message.Bcc.Add(emailsToCopy[i]);

            return message;
        }
    }
}
