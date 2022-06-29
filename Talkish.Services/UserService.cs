using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool?> FollowAsync(int FollowingId, int FollowedUserId)
        {
            return await _repo.FollowAsync(FollowingId, FollowedUserId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = await _repo.GetAllUsersAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            User user = await _repo.GetUserByIdAsync(Id);
            return user;
        }

        public async Task<User> GetUserByAuthorIdAsync(int Id)
        {
            User user = await _repo.GetUserByAuthorIdAsync(Id);
            return user;
        }

        public async Task<List<User>> GetUserFollowersByUserIdAsync(int Id)
        {
            List<User> userFollowers = await _repo.GetUserFollowersByUserIdAsync(Id);
            return userFollowers;
        }

        public async Task<List<User>> GetUserFollowedUsersByUserIdAsync(int Id)
        {
            List<User> userFollowedUsers = await _repo.GetUserFollowedUsersByUserIdAsync(Id);
            return userFollowedUsers;
        }
    }
}
