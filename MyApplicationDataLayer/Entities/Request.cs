using System.ComponentModel.DataAnnotations;

namespace MyApplicationDataLayer.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required RequestStatus Status { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }

    public enum RequestStatus
    {
        [Display(Name = "Получена")]
        Received,

        [Display(Name = "В работе")]
        InWork,

        [Display(Name = "Выполнена")]
        Completed,

        [Display(Name = "Отклонена")]
        Rejected,

        [Display(Name = "Получена")]
        Canceled
    }
}
