using MyApplicationDomain.Entities;

namespace MyApplicationServiceLayer.ProjectService.Remove
{
    public interface IRemoveProjectService
    {
        Task<Project?> Remove(int id);
    }
}