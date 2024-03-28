using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;

namespace MyApplication.Services
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;
        private IEnumerable<Request>? requests = null;

        public RequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Request>> All(PostRequestModel model)
        {
            if (requests == null)
                requests = await _httpClient.GetFromJsonAsync<IEnumerable<Request>>("request");

            return requests!;
        }
    }
}
