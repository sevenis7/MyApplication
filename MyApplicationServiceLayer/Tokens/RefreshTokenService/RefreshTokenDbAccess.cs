using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.Tokens.RefreshTokenService
{
    public class RefreshTokenDbAccess : IRefreshTokenDbAccess
    {
        private readonly AppDbContext _context;

        public RefreshTokenDbAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUser(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(rt => rt.User.Role)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            var user = token.User;

            if (user == null)
                return null;

            return user;
        }

        public async Task Revoke(string userName)
        {
            var tokens = await _context.RefreshTokens
                .Include(rt=>rt.User)
                .Where(rt => rt.User.UserName == userName)
                .ToListAsync();

            _context.RefreshTokens.RemoveRange(tokens);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string refreshToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            _context.RefreshTokens.Remove(token);

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> Add(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync();

            return refreshToken;
        }
    }
}
