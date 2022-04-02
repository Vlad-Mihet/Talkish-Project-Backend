using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(dynamic RegistrationData);

        Task<User> Login(dynamic LoginData);
    }
}
