using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.AccountService.Login.Models;

namespace MyApplicationServiceLayer.AccountService.Login
{
    public interface ILoginService
    {
        Task<User?> Login(LoginModel model);
    }
}