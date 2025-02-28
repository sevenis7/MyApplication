using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.RequestService.Models;

namespace MyApplicationServiceLayer.RequestService.Extensions
{
    public static class RequestModelExtensions
    {
        public static IQueryable<RequestModel> ToModel(this IQueryable<Request> source)
        {
            return source.Select(r => new RequestModel
            {
                Date = r.Date.ToShortDateString(),
                Email = r.User.Email,
                Text = r.Text,
                UserName = r.User.FullName,
                Status = r.Status.ToString()
            });
        }

        public static RequestModel ToModel(this Request source)
        {
            return new RequestModel
            {
                Date = source.Date.ToShortDateString(),
                Email = source.User.Email,
                Text = source.Text,
                UserName = source.User.FullName,
                Status = source.Status.ToString()
            };
        }
    }
}
