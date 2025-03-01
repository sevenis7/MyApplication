using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService.Post
{
    public class PostProjectService : IPostProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public PostProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
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

            await _projectRepository.Add(project);
            return project;
        }
    }
}
