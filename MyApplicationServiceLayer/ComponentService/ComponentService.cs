using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.ComponentService
{
    public class ComponentService : IComponentService
    {
        private readonly AppDbContext _context;

        public ComponentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Component?> Edit(int id, string value)
        {
            var component = await Get(id);

            if (component == null)
                return null;

            component.Value = value;

            _context.Components.Update(component);

            await _context.SaveChangesAsync();

            return component;
        }

        public async Task<Component?> Get(int id)
        {
            return await _context.Components.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IQueryable<Component?>?> GetAll()
        {
            return _context.Components;
        }
    }
}
