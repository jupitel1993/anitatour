using System;
using Application.Common.Interfaces;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class HasherService : IHasherService
    {
        private readonly byte[] _salt;
        public HasherService(IOptions<HasherSettings> settings)
        {
            _salt = Convert.FromBase64String(settings.Value.Salt);
        }

        // asp.net core 3.1 password hashing
        // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.1
        public string GetHash(string password)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

    }
}
