using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Services
{
    class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;

        public AuthService(IAuthRepository repo)
        {
            _repo = repo;
        }

        public async Task<AuthUser> Login(AuthUser LoginData)
        {
            AuthUser user = await _repo.Login(LoginData);
            return user;
        }

        public async Task<AuthUser> Register(AuthUser RegistrationData)
        {
            AuthUser registeredUser = await _repo.Register(RegistrationData);
            return registeredUser;
        }
    }
}
