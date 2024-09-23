using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.RequestService.List
{
    public interface IRequestListService
    {
        Task<IQueryable<Request>?>? GetAll();
        Task<IQueryable<Request>?> GetByStatus(RequestStatus status);
        Task<Request?> Get(int id);
    }
}
