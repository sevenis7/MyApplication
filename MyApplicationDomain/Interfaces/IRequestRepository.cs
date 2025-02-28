using MyApplicationDomain.Entities;

namespace MyApplicationDomain.Repositories
{
    public interface IRequestRepository
    {
        Task Add(Request request);
        Task Delete(Request request);
        Task<Request?> Get(int id);
        IQueryable<Request> GetAll();
        Task Update(Request request);
        IQueryable<Request> GetByStatus(RequestStatus status);
    }
}