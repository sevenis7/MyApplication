using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService.Edit
{
    public interface IEditProjectService
    {
        Task<Project?> Edit(int id, ProjectModel project);
    }
}