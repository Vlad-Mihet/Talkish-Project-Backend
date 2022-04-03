using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
using Talkish.Domain.Models;

namespace Talkish.Dal.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AuthRepository(AppDbContext ctx, UserManager<IdentityUser> userManager, IMapper mapper, ILogger<AuthRepository> logger)
        {
            _ctx = ctx;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IdentityUser> Login(dynamic LoginData)
        {
            try
            {
                IdentityUser identityUser = await ValidateAndGetIdentityAsync(LoginData);

                if (identityUser is null)
                {
                    return null;
                }

                return identityUser;

            } catch (Exception e)
            {
                _logger.LogDebug(JsonConvert.SerializeObject(e));
                return null;
            }
        }

        public async Task<User> Register(dynamic RegistrationData)
        {
            try
            {
                var existingIdentity = await ValidateIdentityDoesNotExist(RegistrationData);

                if (existingIdentity != null)
                {
                    return null;
                }

                await using var transaction = await _ctx.Database.BeginTransactionAsync();

                var identity = await CreateIdentityUserAsync(RegistrationData, transaction);

                var user = await CreateUserAsync(RegistrationData, transaction, identity);

                if (identity is null || user is null)
                {
                    return null;
                }

                await transaction.CommitAsync();

                return user;
            } catch (Exception ex)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(ex));
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

        public async Task<IdentityUser> CreateIdentityUserAsync(dynamic RegistrationData,
        IDbContextTransaction transaction)
        {
            IdentityUser identity = new () {
                Email = RegistrationData.Email,
                UserName = RegistrationData.Email
            };

            IdentityResult createdIdentity = await _userManager.CreateAsync(identity, RegistrationData.Password);

            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync();

                return null;
            }

            return identity;
        }

        private async Task<User> CreateUserAsync(dynamic RegistrationData,
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

                User user = new() {
                    IdentityId = identity.Id,
                    BasicInfo = basicInfo,
                };

                _ctx.Users.Add(user);

                await _ctx.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(ex));
                await transaction.RollbackAsync();
                return null;
            }
        }
    }
}
