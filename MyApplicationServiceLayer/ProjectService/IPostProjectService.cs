using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService
{
    public interface IPostProjectService
    {
        Task<Project?> Post(PostProjectModel projectModel);
    }
}