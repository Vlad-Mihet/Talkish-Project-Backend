using Microsoft.EntityFrameworkCore;
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

        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
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

                if (followedUser is null)
                {
                    throw new Exception();
                }

                followedUser.Followers.Add(followingUser);

                followingUser.Following.Add(followedUser);

                _ctx.SaveChanges();

                transaction.Commit();

                return true;
            } catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = await _ctx.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            User user = await _ctx.Users.FirstOrDefaultAsync((user) => user.UserId == Id);

            return user;
        }

        public async Task<List<User>> GetUserFollowersByUserIdAsync(int Id)
        {
            User user = await _ctx.Users.FirstOrDefaultAsync((user) => user.UserId == Id);

            return user.Followers;
        }

        public async Task<List<User>> GetUserFollowedUsersByUserIdAsync(int Id) {
            User user = await _ctx.Users.FirstOrDefaultAsync((user) => user.UserId == Id);

            return user.Following;
        }
    }
}
