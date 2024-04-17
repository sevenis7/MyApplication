using MyApplicationClient.Services;
using System.Net.Http.Headers;

namespace MyApplicationClient.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AuthenticationHandler(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var text = _configuration["ServerUrl"];

            var jwt = await _accountService.GetJwt();

            if (IsToServer(request) && !string.IsNullOrEmpty(jwt))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            return await base.SendAsync(request, cancellationToken);
        }

        private bool IsToServer(HttpRequestMessage request)
        {
            return request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;
        }
    }
}
