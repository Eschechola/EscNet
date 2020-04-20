using ESCHENet.Emails.Functions;
using ESCHENet.Emails.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESCHENet.Tests.Emails
{
    [TestClass]
    public class EmailsTests
    {
        //credênciais do email que vai ENVIAR a mensagem
        private string myEmail = "[SEU EMAIL]";
        private string myPassword = "[SUA SENHA]";

        [TestMethod]
        public void ExecuteSendEmailTest()
        {
            var email = new Email
            {
                Receiver = "email.destinatario@email.com",
                Subject = "Assunto da mensagem",
                Body = "Corpo da mensagem"
            };


            //instância da classe que envia o email
            var messager = new EmailSender(myEmail, myPassword);

            //envia o email
            messager.SendEmail(email);

            //envia o email com formatação HTML
            messager.SendEmail(email);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ExecuteSendHTMLEmailTest()
        {
            var email = new Email
            {
                Receiver = "email.destinatario@email.com",
                Subject = "Assunto da mensagem",
                Body = "<h1><strong>Corpo da mensagem</strong></h1>"
            };

            //instância da classe que envia o email
            var messager = new EmailSender(myEmail, myPassword);

            //envia o email
            messager.SendEmail(email);

            //envia o email com formatação HTML
            messager.SendEmail(email, isHtml: true);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ExecuteSendEmailWithCCTest()
        {
            var email = new Email
            {
                Receiver = "email.destinatario@email.com",
                Subject = "Assunto da mensagem",
                Body = "Corpo da mensagem"
            };

            //instância da classe que envia o email
            var messager = new EmailSender(myEmail, myPassword);

            messager.AddCopyEmail("email.copia@email.com");
            messager.AddCopyEmail("email.copia2@email.com");

            //testa remover email copia
            messager.RemoveCopyEmail("email.copia2@email.com");

            //envia o email
            messager.SendEmail(email);

            //envia o email com formatação HTML
            messager.SendEmail(email);

            Assert.IsTrue(true);
        }
    }
}
