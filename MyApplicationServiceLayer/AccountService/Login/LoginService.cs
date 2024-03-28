using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.Authenticate.Login.Models;

namespace MyApplicationServiceLayer.Authenticate.Login
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;

        public LoginService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> Login(LoginModel model)
        {
            var user = await _userManager.Users.
                Include(u => u.Role).
                FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, model.Password))
                return null;

            return user;
        }
    }
}
