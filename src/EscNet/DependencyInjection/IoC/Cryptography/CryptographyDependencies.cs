using EscNet.Cryptography.Algorithms;
using EscNet.Cryptography.Interfaces;
using EscNet.Cryptography.Interfaces.Cryptography;
using EscNet.Hashers.Algorithms;
using EscNet.Hashers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EscNet.DependencyInjection.IoC.Cryptography
{
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

        public static IServiceCollection AddSha1Hash(this IServiceCollection services)
        {
            services.AddSingleton<ISha1Hasher, Sha1Hasher>();
            return services;
        }
    }
}
