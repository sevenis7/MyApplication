using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;

namespace MyApplicationServiceLayer.RequestService.PostRequest
{
    public class PostRequestService : IPostRequestService
    {
        private readonly AppDbContext _context;

        public PostRequestService(AppDbContext context)
        {
            _context = context;
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

            _context.Requests.Add(newRequest);

            await _context.SaveChangesAsync();

            return newRequest;
        }
    }
}
