using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;

namespace MyApplicationServiceLayer.RequestService.List
{
    public class RequestListService : IRequestListService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestListService(IRequestRepository requestRepository)
        {
            this._requestRepository = requestRepository;
        }

        public IQueryable<Request> GetAll()
        {
            return _requestRepository.GetAll();
        }

        public  IQueryable<Request> GetByStatus(RequestStatus status)
        {
            return _requestRepository.GetByStatus(status);
        }

        public async Task<Request?> Get(int id)
        {
            return await _requestRepository.Get(id);
        }
    }
}
