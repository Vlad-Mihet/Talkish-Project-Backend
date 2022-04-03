using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(dynamic RegistrationData);

        Task<IdentityUser> Login(dynamic LoginData);
    }
}
