namespace MyApplicationServiceLayer.RequestService.Models
{
    public class RequestModel
    {
        public required string Date { get; set; }
        public required string Text { get; set; }
        public required string UserName { get; set; }
        public required string Status { get; set; }
        public string? Email { get; set; }
    }
}
