using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.Tokens.RefreshTokenService
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenValidator _validator;
        private readonly IRefreshTokenDbAccess _dbAccess;

        public RefreshTokenService(
            IRefreshTokenValidator validator, 
            IRefreshTokenDbAccess dbAccess)
        {
            _validator = validator;
            _dbAccess = dbAccess;
        }

        public async Task<bool> Validate(string refreshToken)
        {
            return await _validator.Validate(refreshToken);
        }

        public async Task<User?> GetUser(string refreshToken)
        {
            return await _dbAccess.GetUser(refreshToken);
        }

        public async Task Delete(string refreshToken)
        {
            await _dbAccess.Delete(refreshToken);
        }

        public async Task Revoke(string userName)
        {
            await _dbAccess.Revoke(userName);
        }

        public async Task<RefreshToken?> Add(RefreshToken refreshToken)
        {
            return await _dbAccess.Add(refreshToken);
        }
    }
}
