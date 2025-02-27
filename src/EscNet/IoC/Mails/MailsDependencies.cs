using EscNet.Mails.Functions;
using EscNet.Mails.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EscNet.IoC.Mails;

public static class MailsDependencies
{
    public static IServiceCollection AddEmailSender(
        this IServiceCollection services, string emailSender, string passwordSender)
    {
        services.AddScoped<IEmailSender>(_ => new EmailSender(emailSender, passwordSender));
        return services;
    }
}