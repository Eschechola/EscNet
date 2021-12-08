using EscNet.Hashers.Algorithms;
using EscNet.Hashers.Interfaces.Algorithms;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.DependencyInjection;
using Scrypt;

namespace EscNet.IoC.Hashers
{
    public static class HashersDependencies
    {
        public static IServiceCollection AddSha1Hash(this IServiceCollection services)
        {
            services.AddSingleton<ISha1Hasher, Sha1Hasher>();
            return services;
        }

        public static IServiceCollection AddArgon2IdHasher(this IServiceCollection services, Argon2Config config)
        {
            services.AddScoped<IArgon2IdHasher>((_) => new Argon2IdHasher(config));
            return services;
        }

        public static IServiceCollection AddBCryptHasher(this IServiceCollection services, string salt)
        {
            services.AddScoped<IBCryptHasher>((_) => new BCryptHasher(salt));
            return services;
        }

        public static IServiceCollection AddSCryptHasher(this IServiceCollection services)
        {
            services.AddScoped<ISCryptHasher>((_) => new SCryptHasher(new ScryptEncoder()));
            return services;
        }
    }
}
