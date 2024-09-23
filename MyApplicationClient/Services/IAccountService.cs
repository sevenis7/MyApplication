using MyApplicationServiceLayer.AccountService.Login.Models;

namespace MyApplicationClient.Services
{
    public interface IAccountService
    {
        event Action<string?, string?>? LoginChange;

        ValueTask<string?> GetJwt();
        Task Login(LoginModel model);
        Task Logout();
        Task<bool> Refresh();
        public string GetUserName(string token);
        public string GetRole(string token);
    }
}