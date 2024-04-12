using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.Tokens.RefreshTokenService
{
    public interface IRefreshTokenDbAccess
    {
        Task<RefreshToken?> Add(RefreshToken refreshToken);
        Task Delete(string refreshToken);
        Task<User?> GetUser(string refreshToken);
        Task Revoke(string userName);
    }
}