using MyApplicationDomain.Entities;

namespace MyApplicationServiceLayer.RequestService.List
{
    public interface IRequestListService
    {
        IQueryable<Request> GetAll();
        IQueryable<Request> GetByStatus(RequestStatus status);
        Task<Request?> Get(int id);
    }
}
