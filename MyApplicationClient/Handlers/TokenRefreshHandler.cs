using MyApplicationClient.Services;
using System.Net;
using System.Net.Http.Headers;

namespace MyApplicationClient.Handlers
{
    public class TokenRefreshHandler : DelegatingHandler
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public TokenRefreshHandler(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _accountService.GetJwt();

            var response = await base.SendAsync(request, cancellationToken);

            if (!string.IsNullOrEmpty(jwt) && IsToServer(request) && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (await _accountService.Refresh())
                {
                    jwt = await _accountService.GetJwt();

                    if (!string.IsNullOrEmpty(jwt))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                        response = await base.SendAsync(request, cancellationToken);
                    }
                }
            }

            return response;
        }

        private bool IsToServer(HttpRequestMessage request)
        {
            return request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;
        }

    }
}
