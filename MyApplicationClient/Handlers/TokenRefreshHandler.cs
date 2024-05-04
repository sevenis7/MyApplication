using MyApplicationClient.Services;
using System.Net;
using System.Net.Http.Headers;

namespace MyApplicationClient.Handlers
{
    public class TokenRefreshHandler : DelegatingHandler
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private bool _refreshing;

        public TokenRefreshHandler(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _accountService.GetJwt();
            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

            var response = await base.SendAsync(request, cancellationToken);

            if (!string.IsNullOrEmpty(jwt) 
                && isToServer
                && response.StatusCode == HttpStatusCode.Unauthorized
                && !_refreshing)
            {
                try
                {
                    _refreshing = true;

                    if (await _accountService.Refresh())
                    {
                        jwt = await _accountService.GetJwt();

                        if (!string.IsNullOrEmpty(jwt) && isToServer)
                        {
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                        }

                        response = await base.SendAsync(request, cancellationToken);
                    }
                }
                finally 
                {
                    _refreshing = false;
                }
               
            }

            return response;
        }

    }
}
