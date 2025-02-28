using MyApplicationDataLayer.DataContext;
using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService.Post
{
    public class PostProjectService : IPostProjectService
    {
        private readonly AppDbContext _context;

        public PostProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project?> Post(ProjectModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Description)) return null;

            var project = new Project
            {
                Description = model.Title,
                Title = model.Description,
                ImageUrl = model.ImageUrl
            };

            _context.Projects.Add(project);

            await _context.SaveChangesAsync();

            return project;
        }
    }
}
