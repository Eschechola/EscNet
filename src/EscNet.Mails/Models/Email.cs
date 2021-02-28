namespace EscNet.Mails.Models
{
    public class Email
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Email()
        {
        }

        public Email(
            string receiver,
            string subject,
            string body)
        {
            Receiver = receiver;
            Subject = subject;
            Body = body;
        }
    }
}
