using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;
        private readonly ILogger _logger;

        public UserRepository(AppDbContext ctx, ILogger<UserRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<bool?> FollowAsync(int FollowingId, int FollowedUserId)
        {
            using var transaction = await _ctx.Database.BeginTransactionAsync();

            try
            {
                Follower follower = new()
                {
                    FollowedUserId = FollowedUserId,
                    FollowingUserId = FollowingId,
                };

                if (follower is null)
                {
                    throw new Exception();
                }

                _ctx.Followers.Add(follower);

                _ctx.SaveChanges();

                User followingUser = await _ctx.Users.FirstOrDefaultAsync((user) => user.UserId == FollowingId);

                User followedUser = await _ctx.Users.FirstOrDefaultAsync((user) => user.UserId == FollowedUserId);

                if (followedUser is null || followingUser is null)
                {
                    throw new Exception();
                }

                followedUser.Followers.Add(followingUser);

                followingUser.Following.Add(followedUser);

                _ctx.SaveChanges();

                await transaction.CommitAsync();

                return true;
            } catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = await _ctx.Users
                .Include((user) => user.BasicInfo)
                .ToListAsync();

            return users;
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            User user = await _ctx.Users
                .Include((user) => user.BasicInfo)
                .FirstOrDefaultAsync((user) => user.UserId == Id);

            return user;
        }

        public async Task<User> GetUserByAuthorIdAsync(int Id)
        {
            User user = await _ctx.Users
                .Include((user) => user.BasicInfo)
                .FirstOrDefaultAsync((user) => user.AuthorId == Id);

            return user;
        }

        public async Task<List<User>> GetUserFollowersByUserIdAsync(int Id)
        {
            User user = await _ctx.Users
                .Include((user) => user.BasicInfo)
                .Include((user) => user.Followers)
                .ThenInclude((follower) => follower.BasicInfo)
                .FirstOrDefaultAsync((user) => user.UserId == Id);

            return user.Followers;
        }

        public async Task<List<User>> GetUserFollowedUsersByUserIdAsync(int Id) {
            User user = await _ctx.Users
                .Include((user) => user.BasicInfo)
                .Include((user) => user.Following)
                .ThenInclude((follower) => follower.BasicInfo)
                .FirstOrDefaultAsync((user) => user.UserId == Id);

            return user.Following;
        }
    }
}
