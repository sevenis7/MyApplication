using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;

namespace MyApplicationServiceLayer.RequestService.PostRequest
{
    public interface IPostRequestService
    {
        Task<Request?> Post(PostRequestModel model, int userId);
    }
}