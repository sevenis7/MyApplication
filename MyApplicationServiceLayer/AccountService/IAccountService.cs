using Microsoft.AspNetCore.Identity;
using MyApplicationDataLayer.Entities;
using MyApplicationServiceLayer.AccountService.Login.Models;
using MyApplicationServiceLayer.Authenticate;
using MyApplicationServiceLayer.Authenticate.Registration.Models;

namespace MyApplicationServiceLayer.AccountService
{
    public interface IAccountService
    {
        Task<AuthenticatedResponse> Authenticate(User user);
        Task<User?> Login(LoginModel model);
        Task<IdentityResult> Register(RegistrationModel model);
    }
}