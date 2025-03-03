using MyApplicationServiceLayer.ProjectService.Validation;

namespace MyApplicationServiceLayer.ProjectService.Models
{
    public class ProjectModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }

        [ImageUrlValidation]
        public string? ImageUrl { get; set; }
    }
}
