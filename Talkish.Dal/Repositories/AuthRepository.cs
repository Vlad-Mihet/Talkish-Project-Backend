using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal.Repositories
{
    class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public AuthRepository(AppDbContext ctx, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _ctx = ctx;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<User> Login(dynamic LoginData)
        {
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(LoginData);

                if (identityUser is null)
                {
                    throw new Exception("User couldn't be found");
                }

                return LoginData;

            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> Register(dynamic RegistrationData)
        {
            try
            {
                var existingIdentity = await ValidateIdentityDoesNotExist(RegistrationData);

                if (existingIdentity is not null)
                {
                    return null;
                }

                await using var transaction = await _ctx.Database.BeginTransactionAsync();

                var identity = await CreateIdentityUserAsync(RegistrationData, transaction);

                var user = await CreateUserAsync(RegistrationData, transaction, identity);

                return user;
            } catch (Exception)
            {
                return null;
            }
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(dynamic LoginData)
        {
            var identityUser = await _userManager.FindByEmailAsync(LoginData.Email);

            if (identityUser is null)
                return null;

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, LoginData.Password);

            if (!validPassword)
                return null;

            return identityUser;
        }

        private async Task<IdentityUser> ValidateIdentityDoesNotExist(dynamic RegistrationData)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(RegistrationData.Email);

            if (existingIdentity != null)
                return null;

            return existingIdentity;
        }

        private async Task<IdentityUser> CreateIdentityUserAsync(dynamic RegistrationData,
        IDbContextTransaction transaction)
        {
            IdentityUser identity = new () {
                Email = RegistrationData.Email
            };

            var createdIdentity = await _userManager.CreateAsync(identity, RegistrationData.Password);

            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync();

                return null;
            }
            return identity;
        }

        private async Task<User> CreateUserAsync(BasicInfo RegistrationData,
        IDbContextTransaction transaction, IdentityUser identity)
        {
            try
            {
                BasicInfo basicInfo = new()
                {
                    Email = RegistrationData.Email,
                    FirstName = RegistrationData.FirstName,
                    LastName = RegistrationData.LastName,
                };

                _ctx.BasicInfo.Add(basicInfo);

                await _ctx.SaveChangesAsync();

                User user = new() {
                    IdentityId = identity.Id,
                    BasicInfo = basicInfo,
                };

                _ctx.Users.Add(user);

                await _ctx.SaveChangesAsync();

                return user;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
