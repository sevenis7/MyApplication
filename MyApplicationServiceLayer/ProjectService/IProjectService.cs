using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService
{
    public interface IProjectService
    {
        Task<Project?> Edit(int id, ProjectModel project);
        Task<Project?> Get(int id);
        IQueryable<Project> GetAll();
        Task<Project?> Post(ProjectModel projectModel);
        Task<Project?> Remove(int id);
    }
}