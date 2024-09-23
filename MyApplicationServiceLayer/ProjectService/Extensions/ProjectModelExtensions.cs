using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService.Extensions
{
    public static class ProjectModelExtensions
    {
        public static IQueryable<ProjectModel> ToModel(this IQueryable<Project> source)
        {
            return source.Select(p => new ProjectModel
            {
                Title = p.Title,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
            });
        }

        public static ProjectModel ToModel(this Project source)
        {
            return new ProjectModel
            {
                Title = source.Title,
                Description = source.Description,
                ImageUrl = source.ImageUrl,
            };
        }
    }
}
