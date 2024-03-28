
namespace MyApplicationServiceLayer.Tokens.RefreshTokenService
{
    public interface IRefreshTokenValidator
    {
        Task<bool> Validate(string refreshToken);
    }
}