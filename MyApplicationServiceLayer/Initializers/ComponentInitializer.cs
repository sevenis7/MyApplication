using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.Initializers
{
    public class ComponentInitializer
    {
        private readonly AppDbContext _context;

        public ComponentInitializer(AppDbContext context)
        {
            _context = context;
        }

        public async Task Initialize()
        {
            if (!_context.Components.Any())
            {
                await _context.AddRangeAsync(
                    CreateComponent("Index Page Title", "Hello"),
                    CreateComponent("Login Page Title", "Login Page"),
                    CreateComponent("Login Page Button", "Let's Login"));
            }

            await _context.SaveChangesAsync();
        }

        private Component CreateComponent(
            string name,
            string value
            )
        {
            return new Component
            {
                Name = name,
                Value = value
            };
        }
    }
}
