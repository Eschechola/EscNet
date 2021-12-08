using Bogus.DataSets;
using EscNet.Mails.Functions;
using EscNet.Mails.Interfaces;
using EscNet.Mails.Models;
using FluentAssertions;
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

        [Fact(DisplayName = "SendEmail when email is sended")]
        public void SendEmail_WhenSendEmailIsDone_ReturnsTrue()
        {
            // Arrange
            var email = new Email
            {
                Receiver = new Internet().Email(),
                Subject = new Lorem().Letter(15),
                Body = new Lorem().Text()
            };

            // Act
            var result = _sut.SendEmail(email, sendToLocalFolder: true);

            // Assert
            result.Should()
                .BeTrue();
        }

        [Fact(DisplayName = "SendEmailAsync when email is sended")]
        public async void SendEmailAsync_WhenSendEmailIsDone_ReturnsTrue()
        {
            // Arrange
            var email = new Email
            {
                Receiver = new Internet().Email(),
                Subject = new Lorem().Letter(15),
                Body = new Lorem().Text()
            };

            // Act
            var result = await _sut.SendEmailAsync(email, sendToLocalFolder: true);

            // Assert
            result.Should()
                .BeTrue();
        }
    }
}
