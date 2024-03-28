using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;

namespace MyApplicationServiceLayer.Tokens.RefreshTokenService
{
    public class RefreshTokenValidator : IRefreshTokenValidator
    {
        private readonly AppDbContext _context;

        public RefreshTokenValidator(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Validate(string refreshToken)
        {
            var existingToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (existingToken == null)
                return false;

            if (existingToken.Expired < DateTime.Now)
            {
                _context.RefreshTokens.Remove(existingToken);

                await _context.SaveChangesAsync();

                return false;
            }

            return true;
        }
    }
}
