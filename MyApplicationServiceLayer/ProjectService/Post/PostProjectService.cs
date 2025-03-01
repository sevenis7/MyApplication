using Microsoft.Extensions.Configuration;
using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService.Post
{
    public class PostProjectService : IPostProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly string _defaultImageUrl;

        public PostProjectService
            (IProjectRepository projectRepository,
            IConfiguration configuration)
        {
            _projectRepository = projectRepository;
            _defaultImageUrl = configuration["DefaultImagePath"]!;
        }

        public async Task<Project?> Post(ProjectModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Description)) return null;

            var project = new Project
            {
                Description = model.Title,
                Title = model.Description,
                ImageUrl = model.ImageUrl ?? _defaultImageUrl
            };

            await _projectRepository.Add(project);
            return project;
        }
    }
}
