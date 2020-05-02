using ESCHENet.ExtensionMethods;

namespace ESCHENet.Emails.Enums
{
    public enum SmtpType
    {
        [StringValue("smtp.gmail.com")]
        Gmail,

        [StringValue("smtp-mail.outlook.com")]
        Outlook
    }
}
