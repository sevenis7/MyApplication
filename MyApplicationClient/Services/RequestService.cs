using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.RequestService.Models;
using MyApplicationServiceLayer.RequestService.PostRequest.Models;
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
                requests = await _httpClient.GetFromJsonAsync<IEnumerable<RequestModel>>("api/request");

            return requests!;
        }

        public async Task<RequestModel> Get(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<RequestModel>($"api/request/{id}");

            return response!;
        }

        public async Task<Request?> Post(PostRequestModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/request", model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Request>();
        }


    }
}
