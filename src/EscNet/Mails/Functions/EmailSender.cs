using EscNet.Mails.Enums;
using EscNet.Mails.Extensions;
using EscNet.Mails.Factory;
using EscNet.Mails.Interfaces;
using EscNet.Mails.Models;
using EscNet.Shared.Directories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EscNet.Mails.Functions;

public class EmailSender : IEmailSender
{
    #region Private Properties

    private readonly string _senderEmail;
    private readonly string _senderPassword;
    private readonly List<string> _emailsToCopy;
    
    private int _port;
    private string _smtpUrl;
    private string _mailLocalFolder;

    #endregion

    #region Constructors

    public EmailSender(
        string senderEmail,
        string senderPassword,
        int port = 587,
        string mailLocalFolder = "",
        SMTPType smtpType = SMTPType.Gmail,
        List<string> emailsToCopy = null)
    {
        _senderEmail = senderEmail;
        _senderPassword = senderPassword;
        _port = port;
        _mailLocalFolder = mailLocalFolder;
        _smtpUrl = SMTPFactory.GetSMTPUrl(smtpType);
        _emailsToCopy = emailsToCopy ?? [];
    }

    #endregion

    #region Public Methods

    public void AddCopyEmail(string emailCopy)
        => _emailsToCopy.Add(emailCopy.ToLower());

    public void RemoveCopyEmail(string emailRemove)
        => _emailsToCopy.Remove(emailRemove.ToLower());

    public void SetPort(int port)
        => _port = port;

    public void SetSMTP(string smtp)
        => _smtpUrl = smtp.ToLower();

    public void SetSMTP(SMTPType smtpType)
        => _smtpUrl = SMTPFactory.GetSMTPUrl(smtpType);

    public void SetLocalFolder(string localFolder)
        => _mailLocalFolder = localFolder;

    public async Task<bool> SendEmailAsync(
        Email email,
        bool isHtml = false,
        bool sendToLocalFolder = false)
    {
        var message = BuildMailMessage(email, isHtml);
        var smtp = CreateSMTPClient(sendToLocalFolder);

        await smtp.SendMailAsync(message);

        return true;
    }

    public bool SendEmail(
        Email email,
        bool isHtml = false,
        bool sendToLocalFolder = false)
    {
        var message = BuildMailMessage(email, isHtml);
        var smtp = CreateSMTPClient(sendToLocalFolder);

        smtp.Send(message);

        return true;
    }

    private MailMessage BuildMailMessage(Email email, bool isHtml)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_senderEmail),
            Subject = email.Subject,
            IsBodyHtml = isHtml,
            Body = email.Body,
        };

        message.To.Add(new MailAddress(email.Receiver));

        if (_emailsToCopy.Count > 0)
            message.AddCopyEmails(_emailsToCopy);

        return message;
    }

    private SmtpClient CreateSMTPClient(bool sendToLocalFolder = false)
        => sendToLocalFolder ? CreateClientToLocalFolder() : CreateClientToRemoteMailServer();

    private SmtpClient CreateClientToLocalFolder()
    {
        if (string.IsNullOrEmpty(_mailLocalFolder))
            _mailLocalFolder = Path.Combine(Environment.CurrentDirectory, "mails");

        DirectoryManager.CreateDirectoryIfNotExists(_mailLocalFolder);

        return new SmtpClient
        {
            DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
            PickupDirectoryLocation = _mailLocalFolder
        };
    }

    private SmtpClient CreateClientToRemoteMailServer()
    {
        return new SmtpClient
        {
            Host = _smtpUrl,
            Port = _port,
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
    }

    #endregion
}