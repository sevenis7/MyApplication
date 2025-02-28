using Microsoft.AspNetCore.Identity;
using MyApplicationDomain.Entities;

namespace MyApplicationServiceLayer.Initializers
{
    public class RoleInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleInitializer(
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            await CreateUser("user", "userName", "userLastName", "user", "user@ex.com", "user");
            await CreateUser("IronMan", "Tony", "Stark", "ironMan", "ironman@ex.com", "user");
            await CreateUser("SpiderMan", "Peter", "Parker", "spiderMan", "spiderMan@ex.com", "user");
            await CreateUser("Venom", "Eddie", "Brock", "Venom", "anti-spiderMan@ex.com", "user");
            await CreateUser("admin", "adminName", "adminLastName", "admin", "admin@ex.com", "admin");
        }

        private async Task<User?> CreateUser(
            string userName,
            string firstName,
            string lastName,
            string password,
            string email,
            string role
            )
        {
            if (await _userManager.FindByNameAsync(userName) == null)
            {
                var user = new User
                {
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Role = await _roleManager.FindByNameAsync(role) ?? throw new InvalidOperationException($"there is no {role} role"),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                await _userManager.CreateAsync(user, password);
                return user;
            }

            return null;
        }
    }
}
