using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Models;

namespace Talkish.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int Id);

        Task<List<User>> GetUserFollowersByUserIdAsync(int Id);

        Task<List<User>> GetUserFollowedUsersByUserIdAsync(int Id);

        Task<bool?> FollowAsync(int FollowingId, int FollowedUserId);
    }
}
