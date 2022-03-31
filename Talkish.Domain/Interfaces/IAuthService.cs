using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<AuthUser> Register(AuthUser RegistrationData);

        Task<AuthUser> Login(AuthUser LoginData);
    }
}
