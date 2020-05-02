using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using ESCHENet.Emails.Model;
using ESCHENet.Emails.Enums;
using ESCHENet.ExtensionMethods;

namespace ESCHENet.Emails.Functions
{
    public class EmailSender
    {
        
        private string SenderEmail { get; set; }
        private string SenderPassword { get; set; }

        //config properties
        private IList<string> EmailsToCopy = new List<string>();
        private SmtpType SmtpType = SmtpType.Gmail;
        private string AnotherSMTP = string.Empty;
        private int Port = 587;

        public EmailSender(string senderEmail, string senderPassword)
        {
            SenderEmail = senderEmail;
            SenderPassword = senderPassword;
        }

        public void SetPort(int port)
        {
            Port = port;
        }

        public void SetSMTP(string smtp)
        {
            AnotherSMTP = smtp;
        }

        public void SetSMTP(SmtpType smtptype)
        {
            SmtpType = smtptype;
        }

        public void AddCopyEmail(string emailCopy)
        {
            EmailsToCopy.Add(emailCopy.ToLower());
        }

        public void RemoveCopyEmail(string emailRemove)
        {
            EmailsToCopy.Remove(emailRemove.ToLower());
        }

        public void SendEmail(Email email, bool isHtml = false)
        {
            //cria a mensagem de email
            MailMessage message = new MailMessage();

            //email que vai enviar (no caso nós)
            message.From = new MailAddress(SenderEmail);

            //para quem vai enviar (no caso o usuario)
            message.To.Add(new MailAddress(email.Receiver));

            //adiciona os emails de copia
            if(EmailsToCopy.Count > 0)
            {
                foreach (var emailCopy in EmailsToCopy)
                {
                    message.Bcc.Add(emailCopy);
                }
            }

            //atribui assundo do email recebido como parametro na classe email
            message.Subject = email.Subject;

            //habilita usar html na mensagem
            message.IsBodyHtml = isHtml;

            //atribui a mensagem recebida como parametro na classe email
            message.Body = email.Body;

            //instancia o cliente smtp
            SmtpClient smtp;

            if (AnotherSMTP.Equals(string.Empty))
            {
                smtp = new SmtpClient(SmtpType.GetStringValue());
            }
            else
            {
                smtp = new SmtpClient(AnotherSMTP);
            }

            //porta que usarei pra enviar o email
            smtp.Port = Port;

            //habilita camada de segurança
            smtp.EnableSsl = true;

            //desabilita as credenciais padrao
            smtp.UseDefaultCredentials = false;

            //da as credenciais do nosso para poder acessar e enviar
            smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);

            //define o método de envio, no caso web
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //envia a mensagem
            smtp.Send(message);
        }
    }
}
