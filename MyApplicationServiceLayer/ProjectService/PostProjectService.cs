using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService
{
    public class PostProjectService : IPostProjectService
    {
        private readonly AppDbContext _context;

        public PostProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project?> Post(PostProjectModel model)
        {
            if (String.IsNullOrWhiteSpace(model.Title) || String.IsNullOrWhiteSpace(model.Text)) return null;

            var project = new Project
            {
                Text = model.Title,
                Title = model.Text
            };

            _context.Projects.Add(project);

            await _context.SaveChangesAsync();

            return project;
        }
    }
}
