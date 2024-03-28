using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.Authenticate.Registration.Models;

namespace MyApplicationServiceLayer.Authenticate.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        const string USER = "user";

        public RegistrationService(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Register(RegistrationModel model)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName || u.Email == model.Email);

            if (existingUser != null)
            {
                if (existingUser.UserName == model.UserName)
                    return IdentityResult.Failed(
                        new IdentityError
                        {
                            Description = "User with this username already exists."
                        });
                else
                    return IdentityResult.Failed(
                        new IdentityError
                        {
                            Description = "User with this email already exists."
                        });
            }

            var role = await _roleManager.FindByNameAsync(USER);

            if (role == null)
                throw new ArgumentNullException("User role is not exists");

            var newUser = new User
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Role = role
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
                return result;

            return IdentityResult.Failed(
                new IdentityError
                {
                    Description = "Failed to create user."
                });
        }

    }
}
