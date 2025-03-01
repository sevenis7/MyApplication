using MyApplicationDomain.Entities;

namespace MyApplicationDomain.Repositories
{
    public interface IProjectRepository
    {
        Task Add(Project project);
        Task Delete(Project project);
        Task<Project?> Get(int id);
        IQueryable<Project> GetAll();
        Task Update(Project request);
    }
}