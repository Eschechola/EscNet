using EscNet.Mails.Enums;
using EscNet.Mails.Models;
using System.Threading.Tasks;

namespace EscNet.Mails.Interfaces;

public interface IEmailSender
{
    void SetPort(int port);
    void SetSMTP(string smtp);
    void SetSMTP(SMTPType smtpType);
    void SetLocalFolder(string localFolder);
    void AddCopyEmail(string emailCopy);
    void RemoveCopyEmail(string emailRemove);
    bool SendEmail(Email email, bool isHtml = false, bool sendToLocalFolder = false);
    Task<bool> SendEmailAsync(Email email, bool isHtml = false, bool sendToLocalFolder = false);
}