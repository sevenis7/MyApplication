using MyApplicationClient.States;
using System.IdentityModel.Tokens.Jwt;

namespace MyApplicationClient.Services
{
    public class Startup
    {
        private readonly HttpClient _httpClient;
        private readonly UserState _userState;
        private readonly IAccountService _accountService;

        private const string JWT = nameof(JWT);
        private const string REFRESH = nameof(REFRESH);

        public Startup(
            IHttpClientFactory factory,
            UserState userState,
            IAccountService accountService
            )
        {
            _httpClient = factory.CreateClient("ServerApi");
            _userState = userState;
            _accountService = accountService;
        }

        public async Task Start()
        {
            var jwt = await _accountService.GetJwt();

            if (!string.IsNullOrEmpty(jwt))
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadToken(jwt);
                var expired = jwtToken.ValidTo >= DateTime.UtcNow;

                var name = _accountService.GetUserName(jwt);
                var role = _accountService.GetRole(jwt);

                if (!expired)
                    await _accountService.Refresh();

                _userState.SetUser(name, role);
            }
        }
    }
}
