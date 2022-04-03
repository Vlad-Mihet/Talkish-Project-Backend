using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;

        public AuthService(IAuthRepository repo)
        {
            _repo = repo;
        }

        public async Task<IdentityUser> Login(dynamic LoginData)
        {
            var success = await _repo.Login(LoginData);
            return success;
        }

        public async Task<User> Register(dynamic RegistrationData)
        {
            User registeredUser = await _repo.Register(RegistrationData);
            return registeredUser;
        }
    }
}
