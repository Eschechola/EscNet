using EscNet.Cryptography.Algorithms;
using EscNet.Cryptography.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EscNet.IoC.Cryptography;

public static class CryptographyDependencies
{
    public static IServiceCollection AddRijndaelCryptography(this IServiceCollection services, string encryptionKey)
    {
        services.AddSingleton<IRijndaelCryptography>(new RijndaelCryptography(encryptionKey));
        return services;
    }

    public static IServiceCollection AddCaesarCipherCryptography(this IServiceCollection services, int keyUp)
    {
        services.AddSingleton<ICaesarCipherCryptography>(new CaesarCipherCryptography(keyUp));
        return services;
    }
}