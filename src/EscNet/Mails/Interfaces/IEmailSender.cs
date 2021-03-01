using EscNet.Mails.Enums;
using EscNet.Mails.Models;

namespace EscNet.Mails.Interfaces
{
    public interface IEmailSender
    {
        void SetPort(int port);
        void SetSMTP(string smtp);
        void SetSMTP(SMTPType smtpType);
        void AddCopyEmail(string emailCopy);
        void RemoveCopyEmail(string emailRemove);
        void SendEmail(Email email, bool isHtml = false, bool sendToLocalFolder = false);
    }
}
