using MyApplicationDomain.Entities;

namespace MyApplicationServiceLayer.ProjectService.Edit
{
    public interface IEditProjectService
    {
        Task<Project?> Edit(int id, Project project);
    }
}