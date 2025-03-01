using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;
using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;

namespace MyApplicationDataLayer.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project?> Get(int id)
        {
            return await
                _context
                .Projects
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Project> GetAll()
        {
            return _context.Projects;
        }

        public async Task Add(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

    }
}
