namespace MyApplicationDataLayer.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Text { get; set; }
        public byte[]? Image { get; set; }
    }
}
