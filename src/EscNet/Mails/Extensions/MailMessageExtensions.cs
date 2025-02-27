using System.Collections.Generic;
using System.Net.Mail;

namespace EscNet.Mails.Extensions;

public static class MailMessageExtensions
{
    public static void AddCopyEmails(this MailMessage message, List<string> emailsToCopy)
    {
        foreach (var t in emailsToCopy)
            message.Bcc.Add(t);
    }
}