using Microsoft.AspNetCore.Identity;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer
{
    public class DataInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _context;

        public DataInitializer(UserManager<User> userManager, AppDbContext context, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task Initialize()
        {
            if (!await _roleManager.RoleExistsAsync("user"))
            {
                var role = new Role { Name = "user" };
                await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                var role = new Role { Name = "admin" };
                await _roleManager.CreateAsync(role);
            }

            if (await _userManager.FindByNameAsync("user") == null)
            {
                var userRole = await _roleManager.FindByNameAsync("user");

                var user = new User
                {
                    UserName = "user",
                    FirstName = "userName",
                    LastName = "userLastName",
                    Email = "user@ex.com",
                    Role = await _roleManager.FindByNameAsync("user") ?? throw new InvalidOperationException("There is no user role")
                };

                await _userManager.CreateAsync(user, "user");
            }

            if (await _userManager.FindByNameAsync("admin") == null)
            {
                var admin = new User
                {
                    UserName = "admin",
                    FirstName = "adminName",
                    LastName = "adminLastName",
                    Email = "admin@ex.com",
                    Role = await _roleManager.FindByNameAsync("admin") ?? throw new InvalidOperationException("There is no admin role")
                };

                await _userManager.CreateAsync(admin, "admin");
            }

            if (!_context.Requests.Any())
            {
                var user = await _userManager.FindByNameAsync("user");

                if (user != null)
                {
                    _context.Requests.Add(
                    new Request
                    {
                        Status = RequestStatus.Received,
                        Text = "requestTest",
                        Date = DateTime.Now,
                        User = await _userManager.FindByNameAsync("user")
                    });
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
