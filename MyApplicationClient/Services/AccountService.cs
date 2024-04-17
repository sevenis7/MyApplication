using Blazored.LocalStorage;
using MyApplicationServiceLayer.Authenticate;
using MyApplicationServiceLayer.Authenticate.Login.Models;
using MyApplicationServiceLayer.Tokens.RefreshTokenService.Models;
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
        private const string REFRESH = nameof(REFRESH);

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

            await _localStorageService.SetItemAsync(JWT, content.AccessToken);
            await _localStorageService.SetItemAsync(REFRESH, content.RefreshToken);

            LoginChange?.Invoke(GetUserName(_jwt), GetRole(_jwt));
        }

        public async Task<bool> Refresh()
        {
            var model = new RefreshModel
            {
                RefreshToken = await _localStorageService.GetItemAsync<string>(REFRESH)
            };

            var response = await _httpClient.PostAsync("api/account/refresh", JsonContent.Create(model));

            if (!response.IsSuccessStatusCode)
            {
                await Logout();

                return false;
            }

            var content = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();

            if (content == null)
                throw new InvalidDataException();

            await _localStorageService.SetItemAsync(JWT, content.AccessToken);
            await _localStorageService.SetItemAsync(REFRESH, content.RefreshToken);

            _jwt = content.AccessToken;

            return true;

        } 

        public async ValueTask<string> GetJwt()
        {
            if (string.IsNullOrEmpty(_jwt))
                _jwt = await _localStorageService.GetItemAsync<string>(JWT);

            return _jwt;
        }

        public async Task Logout()
        {
            await _httpClient.DeleteAsync("api/account/revoke");

            await _localStorageService.RemoveItemAsync(JWT);
            await _localStorageService .RemoveItemAsync(REFRESH);

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
