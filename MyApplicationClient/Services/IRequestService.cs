using MyApplicationServiceLayer.RequestService.Models;

namespace MyApplicationClient.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestModel>> GetAll();
    }
}