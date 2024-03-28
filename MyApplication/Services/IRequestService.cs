using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;

namespace MyApplication.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>> All(PostRequestModel model);
    }
}