namespace MyApplicationServiceLayer.RequestService.Models
{
    public class RequestModel
    {
        public required int Id { get; set; }
        public required string Date { get; set; }
        public required string Text { get; set; }
        public required string UserName { get; set; }
        public required string Status { get; set; }
        public required string Email { get; set; }
    }
}
