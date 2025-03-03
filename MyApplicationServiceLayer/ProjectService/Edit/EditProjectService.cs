using Microsoft.Extensions.Configuration;
using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService.Edit
{
    public class EditProjectService : IEditProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly string _defaultImageUrl;

        public EditProjectService
            (IProjectRepository projectRepository,
            IConfiguration configuration)
        {
            _projectRepository = projectRepository;
            _defaultImageUrl = configuration["DefaultImagePath"]!;
        }

        public async Task<Project?> Edit(int id, ProjectModel model)
        {
            var existedProject = await _projectRepository.Get(id);

            if (existedProject == null)
                return null;

            existedProject.Title = model.Title;
            existedProject.Description = model.Description;
            existedProject.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? _defaultImageUrl : model.ImageUrl;

            await _projectRepository.Update(existedProject);
            return existedProject;
        }
    }
}
