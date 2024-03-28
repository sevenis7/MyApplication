using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.RequestService.Models;

namespace MyApplicationServiceLayer.RequestService.Extensions
{
    public static class RequestModelExtensions
    {
        public static IQueryable<RequestModel> ToModel(this IQueryable<Request> source)
        {
            return source.Select(r => new RequestModel
            {
                Id = r.Id,
                Date = r.Date.ToShortDateString(),
                Email = r.User.Email,
                Text = r.Text,
                UserName = r.User.FullName,
                Status = r.Status.ToString()
            });
        }
    }
}
