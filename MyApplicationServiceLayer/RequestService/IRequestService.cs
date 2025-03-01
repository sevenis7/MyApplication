using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.RequestService.Models;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;

namespace MyApplicationServiceLayer.RequestService
{
    public interface IRequestService
    {
        Task<Request?> EditStatus(int id, RequestStatus status);
        IQueryable<Request> GetAll();
        IQueryable<Request> GetByStatus(RequestStatus status);
        Task<RequestModel?> Post(PostRequestModel model, int userId);
        Task<Request?> Get(int id);
    }
}