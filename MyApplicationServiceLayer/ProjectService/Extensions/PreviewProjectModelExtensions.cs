using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.ProjectService.Models;

namespace MyApplicationServiceLayer.ProjectService.Extensions
{
    public static class PreviewProjectModelExtensions
    {
        public static IQueryable<PreviewProjectModel> ToModel(this IQueryable<Project> source)
        {
            return source.Select(p => new PreviewProjectModel
            {
                Title = p.Title,
                ImageUrl = p.ImageUrl
            });
        }
    }
}
