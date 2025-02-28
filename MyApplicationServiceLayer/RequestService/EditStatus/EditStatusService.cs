using MyApplicationDomain.Entities;
using MyApplicationDomain.Repositories;

namespace MyApplicationServiceLayer.RequestService.EditStatus
{
    public class EditStatusService : IEditStatusService
    {
        private readonly IRequestRepository _requestRepository;

        public EditStatusService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<Request?> EditStatus(int id, RequestStatus status)
        {
            var request = await _requestRepository.Get(id);

            if (request == null)
                return null;

            request.Status = status;

            await _requestRepository.Update(request);
            return request;
        }
    }
}
