using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;

namespace MyApplicationServiceLayer.ProjectService.Remove
{
    public class RemoveProjectService : IRemoveProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public RemoveProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project?> Remove(int id)
        {
            var project = await _projectRepository.Get(id);

            if (project == null)
                return null;

            await _projectRepository.Delete(project);

            return project;
        }
    }
}
