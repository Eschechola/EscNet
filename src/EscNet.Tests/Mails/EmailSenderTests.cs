using Bogus.DataSets;
using EscNet.Mails.Functions;
using EscNet.Mails.Interfaces;
using EscNet.Mails.Models;
using Xunit;

namespace EscNet.Tests.Mails
{
    public class EmailSenderTests
    {
        private readonly string _emailSender;
        private readonly string _passwordSender;
        private readonly IEmailSender _sut;

        public EmailSenderTests()
        {
            _emailSender = new Internet().Email();
            _passwordSender = new Internet().Password();
            _sut = new EmailSender(_emailSender, _passwordSender);
        }

        [Fact]
        public void EmailSender_SendEmail_SendEmailCorrectly()
        {
            // Arrange
            var email = new Email
            {
                Receiver = new Internet().Email(),
                Subject = new Lorem().Letter(15),
                Body = new Lorem().Text()
            };

            // Act & Assert
            _sut.SendEmail(email, sendToLocalFolder: true);
        }
    }
}
