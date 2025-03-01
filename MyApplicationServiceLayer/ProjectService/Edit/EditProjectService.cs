using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;

namespace MyApplicationServiceLayer.ProjectService.Edit
{
    public class EditProjectService : IEditProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public EditProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project?> Edit(int id, Project project)
        {
            var existedProject = await _projectRepository.Get(id);

            if (existedProject == null)
                return null;

            existedProject = project;

            await _projectRepository.Update(existedProject);
            return existedProject;
        }
    }
}
