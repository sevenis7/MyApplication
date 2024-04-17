using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApplicationClient.Handlers;
using MyApplicationClient.Services;

namespace MyApplicationClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                .AddHttpMessageHandler<AuthenticationHandler>();

            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddBlazoredLocalStorageAsSingleton();

            builder.Services.AddScoped<IRequestService, RequestService>();


            await builder.Build().RunAsync();
        }
    }
}