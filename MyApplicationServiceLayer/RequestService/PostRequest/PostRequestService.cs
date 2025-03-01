using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;
using MyApplicationDomain.Repositories;
using MyApplicationServiceLayer.RequestService.Extensions;
using MyApplicationServiceLayer.RequestService.Models;
using MyApplicationServiceLayer.RequestService.List;

namespace MyApplicationServiceLayer.RequestService.PostRequest
{
    public class PostRequestService : IPostRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IRequestListService _requestListService;

        public PostRequestService
            (IRequestRepository requestRepository, 
            IRequestListService requestListService)
        {
            _requestRepository = requestRepository;
            _requestListService = requestListService;
        }

        public async Task<RequestModel?> Post(PostRequestModel model, int userId)
        {
            if (String.IsNullOrWhiteSpace(model.Text)) return null;

            var newRequest = new Request
            {
                Text = model.Text,
                Status = RequestStatus.Received,
                Date = DateTime.UtcNow,
                UserId = userId
            };
            
            var addedRequest = await _requestRepository.Add(newRequest);

            return addedRequest?.ToModel();
        }
    }
}
