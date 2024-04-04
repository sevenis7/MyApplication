using Blazored.LocalStorage;
using MyApplicationServiceLayer.Authenticate;
using MyApplicationServiceLayer.Authenticate.Login.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace MyApplicationClient.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private const string JWT = nameof(JWT);

        private string? _jwt;

        public event Action<string?, string?>? LoginChange;

        public AccountService(IHttpClientFactory factory, ILocalStorageService localStorageService)
        {
            _httpClient = factory.CreateClient("ServerApi");
            _localStorageService = localStorageService;
        }

        public async Task Login(LoginModel model)
        {
            var response = await _httpClient.PostAsync("api/account/login", JsonContent.Create(model));

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Login failed.");

            var content = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();

            if (content == null)
                throw new InvalidDataException();

            _jwt = content.AccessToken;

            await _localStorageService.SetItemAsync(JWT, _jwt);

            LoginChange?.Invoke(GetUserName(_jwt), GetRole(_jwt));
        }

        public async ValueTask<string> GetJwt()
        {
            if (string.IsNullOrEmpty(_jwt))
                _jwt = await _localStorageService.GetItemAsync<string>(JWT);

            return _jwt;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(JWT);

            _jwt = null;

            LoginChange?.Invoke(null, null);
        }

        private static string GetUserName(string token)
        {
            return new JwtSecurityToken(token).Claims.First(c => c.Type == ClaimTypes.Name).Value;
        }

        private static string GetRole(string token)
        {
            return new JwtSecurityToken(token).Claims.First(c => c.Type == ClaimTypes.Role).Value;
        }
    }
}
