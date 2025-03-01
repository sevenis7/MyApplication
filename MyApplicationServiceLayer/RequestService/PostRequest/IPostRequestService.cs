using MyApplicationServiceLayer.RequestService.Models;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;

namespace MyApplicationServiceLayer.RequestService.PostRequest
{
    public interface IPostRequestService
    {
        Task<RequestModel?> Post(PostRequestModel model, int userId);
    }
}