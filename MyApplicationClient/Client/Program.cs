using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApplicationClient.Handlers;
using MyApplicationClient.Services;
using MyApplicationClient.States;

namespace MyApplicationClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddTransient<AuthenticationHandler>();
            builder.Services.AddTransient<TokenRefreshHandler>();

            builder.Services.AddSingleton<UserState>();

            builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                .AddHttpMessageHandler<AuthenticationHandler>()
                .AddHttpMessageHandler<TokenRefreshHandler>();

            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddBlazoredLocalStorageAsSingleton();

            builder.Services.AddScoped<IRequestService, RequestService>();

            await builder.Build().RunAsync();
        }
    }
}