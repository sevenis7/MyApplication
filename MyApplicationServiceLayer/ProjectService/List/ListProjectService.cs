using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;

namespace MyApplicationServiceLayer.ProjectService.List
{
    public class ListProjectService : IListProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ListProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IQueryable<Project> GetAll()
        {
            return _projectRepository.GetAll();
        }

        public async Task<Project?> Get(int id)
        {
            return await _projectRepository.Get(id);
        }
    }
}
