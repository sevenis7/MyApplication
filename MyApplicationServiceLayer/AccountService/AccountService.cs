using Microsoft.AspNetCore.Identity;
using MyApplicationDomain.Entities;
using MyApplicationServiceLayer.AccountService.Login;
using MyApplicationServiceLayer.AccountService.Login.Models;
using MyApplicationServiceLayer.Authenticate;
using MyApplicationServiceLayer.Authenticate.Registration;
using MyApplicationServiceLayer.Authenticate.Registration.Models;

namespace MyApplicationServiceLayer.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly ILoginService _loginService;
        private readonly IRegistrationService _registrationService;
        private readonly IAuthenticator _authenticator;

        public AccountService(
            ILoginService loginService,
            IRegistrationService registrationService,
            IAuthenticator authenticator)
        {
            _loginService = loginService;
            _registrationService = registrationService;
            _authenticator = authenticator;
        }

        public async Task<User?> Login(LoginModel model)
        {
            return await _loginService.Login(model);
        }

        public async Task<IdentityResult> Register(RegistrationModel model)
        {
            return await _registrationService.Register(model);
        }

        public async Task<AuthenticatedResponse> Authenticate(User user)
        {
            return await _authenticator.Authenticate(user);
        }
    }
}
