using MyApplicationServiceLayer.Authenticate.Login.Models;

namespace MyApplicationClient.Services
{
    public interface IAccountService
    {
        event Action<string?, string?>? LoginChange;

        ValueTask<string> GetJwt();
        Task Login(LoginModel model);
        Task Logout();
        Task<bool> Refresh();
    }
}