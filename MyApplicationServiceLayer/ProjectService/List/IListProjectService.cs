using MyApplicationDomain.Entities;

namespace MyApplicationServiceLayer.ProjectService.List
{
    public interface IListProjectService
    {
        Task<Project?> Get(int id);
        IQueryable<Project> GetAll();
    }
}