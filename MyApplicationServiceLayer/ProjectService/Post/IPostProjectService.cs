using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService.Post
{
    public interface IPostProjectService
    {
        Task<Project?> Post(ProjectModel projectModel);
    }
}