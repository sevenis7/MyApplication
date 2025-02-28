using MyApplicationDomain.Entities;

namespace MyApplicationServiceLayer.Authenticate
{
    public interface IAuthenticator
    {
        Task<AuthenticatedResponse> Authenticate(User user);
    }
}