using MyApplicationDomain.Entities;

namespace MyApplicationServiceLayer.Tokens.RefreshTokenService
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken?> Add(RefreshToken refreshToken);
        Task<User?> GetUser(string refreshToken);
        Task Delete(string refreshToken);
        Task<bool> Validate(string refreshToken);
        Task Revoke(string userName);
    }
}