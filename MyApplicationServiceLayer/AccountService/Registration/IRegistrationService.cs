using Microsoft.AspNetCore.Identity;
using MyApplicationServiceLayer.Authenticate.Registration.Models;

namespace MyApplicationServiceLayer.Authenticate.Registration
{
    public interface IRegistrationService
    {
        Task<IdentityResult> Register(RegistrationModel model);
    }
}