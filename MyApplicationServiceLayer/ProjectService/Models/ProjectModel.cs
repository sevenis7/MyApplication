namespace MyApplicationServiceLayer.ProjectService.Models
{
    public class ProjectModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
