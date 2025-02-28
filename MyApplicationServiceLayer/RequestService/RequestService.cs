using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.RequestService.EditStatus;
using MyApplicationServiceLayer.RequestService.List;
using MyApplicationServiceLayer.RequestService.PostRequest;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;

namespace MyApplicationServiceLayer.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly IEditStatusService _editStatusService;
        private readonly IPostRequestService _postRequestService;
        private readonly IRequestListService _requestListService;

        public RequestService(
            IEditStatusService editStatusService,
            IPostRequestService postRequestService,
            IRequestListService requestListService)
        {
            _editStatusService = editStatusService;
            _postRequestService = postRequestService;
            _requestListService = requestListService;
        }

        public async Task<Request?> EditStatus(int id, RequestStatus status)
        {
            return await _editStatusService.EditStatus(id, status);
        }

        public async Task<Request?> Post(PostRequestModel model, int userId)
        {
            return await _postRequestService.Post(model, userId);
        }

        public async Task<Request?> Get(int id)
        {
            return await _requestListService.Get(id);
        }

        public IQueryable<Request>? GetAll()
        {
            return _requestListService.GetAll();
        }

        public IQueryable<Request> GetByStatus(RequestStatus status)
        {
            return _requestListService.GetByStatus(status);
        }
    }
}
