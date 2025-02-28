using MyApplicationDataLayer.DataContext;
using MyApplicationDomain.Entities;
using MyApplicationDataLayer.Repositories;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;

namespace MyApplicationServiceLayer.RequestService.PostRequest
{
    public class PostRequestService : IPostRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public PostRequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<Request?> Post(PostRequestModel model, int userId)
        {
            if (String.IsNullOrWhiteSpace(model.Text)) return null;

            var newRequest = new Request
            {
                Text = model.Text,
                Status = RequestStatus.Received,
                Date = DateTime.UtcNow,
                UserId = userId
            };

            await _requestRepository.Add(newRequest);
            var result = await _requestRepository.Get(newRequest.Id);

            return result;
        }
    }
}
