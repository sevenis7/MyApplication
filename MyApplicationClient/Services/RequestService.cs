using MyApplicationServiceLayer.RequestService.Models;
using System.Net.Http.Json;

namespace MyApplicationClient.Services
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;
        private IEnumerable<RequestModel>? requests = null;

        public RequestService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<IEnumerable<RequestModel>> GetAll()
        {
            if (requests == null)
                requests = await _httpClient.GetFromJsonAsync<IEnumerable<RequestModel>>("request");

            return requests!;
        }

        public async Task<RequestModel> Get(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<RequestModel>($"request/{id}");

            return response!;
        }
    }
}
