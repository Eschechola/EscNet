using EscNet.Mails.Enums;
using EscNet.Mails.Factory;
using EscNet.Mails.Interfaces;
using EscNet.Mails.Models;
using EscNet.Shared.Directories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace EscNet.Mails.Functions
{
    public class EmailSender : IEmailSender
    {

        #region Private Properties

        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private List<string> _emailsToCopy;
        private SMTPType _smtpType;
        private int _port;
        private string _smtpUrl;

        #endregion

        #region Public Properties

        public string SenderEmail
        { 
            get
            {
                return _senderEmail;
            }
        }

        public string SenderPassword
        {
            get
            {
                return _senderPassword;
            }
        }

        public List<string> EmailToCopy
        {
            get
            {
                return _emailsToCopy;
            }
        }

        public string SmtpUrl
        {
            get
            {
                return _smtpUrl;
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }
        }

        #endregion

        #region Constructors

        public EmailSender(string senderEmail, string senderPassword)
        {
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;

            _emailsToCopy = new List<string>();
            _port = 587;
            _smtpType = SMTPType.Gmail;
            _smtpUrl = SMTPFactory.GetSMTPUrl(_smtpType);
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

        public void SendEmail(Email email, bool isHtml = false, bool sendToLocalFolder = false)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_senderEmail);
            message.To.Add(new MailAddress(email.Receiver));

            if (_emailsToCopy.Count > 0)
                foreach (var emailCopy in _emailsToCopy)
                    message.Bcc.Add(emailCopy);

            message.Subject = email.Subject;
            message.IsBodyHtml = isHtml;
            message.Body = email.Body;

            SmtpClient smtp;

            if (sendToLocalFolder)
            {
                var mailDirectory = Path.Combine(Environment.CurrentDirectory, "mails");
                DirectoryManager.CreateDirectoryIfNotExists(mailDirectory);

                smtp = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = mailDirectory
                };
            }
            else
            {
                smtp = new SmtpClient
                {
                    Host = _smtpUrl,
                    Port = _port,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
            }

            smtp.Send(message);
        }

        #endregion
    }
}