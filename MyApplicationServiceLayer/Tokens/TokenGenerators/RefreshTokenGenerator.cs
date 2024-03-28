using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace MyApplicationServiceLayer.Tokens.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        public string GenerateToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
