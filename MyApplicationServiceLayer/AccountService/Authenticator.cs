using Microsoft.Extensions.Options;
using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.Tokens.RefreshTokenService;
using MyApplicationServiceLayer.Tokens.TokenGenerators;

namespace MyApplicationServiceLayer.Authenticate
{
    public class Authenticator : IAuthenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly AuthenticationConfiguration _configuration;
        private readonly IRefreshTokenService _refreshTokenService;

        public Authenticator(
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            IOptions<AuthenticationConfiguration> configuration,
            IRefreshTokenService refreshTokenService)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _configuration = configuration.Value;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<AuthenticatedResponse> Authenticate(User user)
        {
            string accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDto = new RefreshToken
            {
                Token = refreshToken,
                User = user,
                Expired = DateTime.Now.AddMinutes(_configuration.RefreshTokenExpirationMinutes)
            };

            await _refreshTokenService.Add(refreshTokenDto);

            return new AuthenticatedResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

    }
}
