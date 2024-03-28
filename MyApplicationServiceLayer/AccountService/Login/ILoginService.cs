using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.Authenticate.Login.Models;

namespace MyApplicationServiceLayer.Authenticate.Login
{
    public interface ILoginService
    {
        Task<User?> Login(LoginModel model);
    }
}