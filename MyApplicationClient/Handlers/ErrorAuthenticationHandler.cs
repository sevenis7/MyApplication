using Microsoft.AspNetCore.Components;
using System.Net;

namespace MyApplicationClient.Handlers
{
    public class ErrorAuthenticationHandler : DelegatingHandler
    {
        private readonly NavigationManager _navigationManager;

        public ErrorAuthenticationHandler(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                _navigationManager.NavigateTo("401");

            if (response.StatusCode == HttpStatusCode.Forbidden)
                _navigationManager.NavigateTo("403");

            return response;
        }
    }
}
