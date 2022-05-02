using Microsoft.AspNetCore.Identity;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Talkish.Dal;
using Microsoft.Extensions.Logging;
using Talkish.Domain.Models;
using Talkish.Services.DTOs;

namespace Talkish.Services
{
    public class AuthService
    {
        private readonly AppDbContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;

        public AuthService(UserManager<IdentityUser> userManager, AppDbContext ctx, ILogger<AuthService> logger)
        {
            _ctx = ctx;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IdentityUser> Login(LoginDTO LoginData)
        {
            try
            {
                IdentityUser identityUser = await ValidateAndGetIdentityAsync(LoginData);

                if (identityUser is null)
                {
                    return null;
                }

                return identityUser;

            }
            catch (Exception e)
            {
                _logger.LogDebug(JsonConvert.SerializeObject(e));
                return null;
            }
        }

        public async Task<User> Register(RegisterDTO RegistrationData)
        {
            await using var transaction = await _ctx.Database.BeginTransactionAsync();

            try
            {
                var existingIdentity = await ValidateIdentityDoesNotExist(RegistrationData);

                if (existingIdentity != null)
                {
                    return null;
                }

                var identity = await CreateIdentityUserAsync(RegistrationData);

                var user = await CreateUserAsync(RegistrationData, identity);

                if (identity is null || user is null)
                {
                    return null;
                }

                await transaction.CommitAsync();

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(JsonConvert.SerializeObject(ex));
                await transaction.RollbackAsync();
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

        public async Task<IdentityUser> CreateIdentityUserAsync(dynamic RegistrationData)
        {
            IdentityUser identity = new()
            {
                Email = RegistrationData.Email,
                UserName = RegistrationData.Email
            };

            IdentityResult createdIdentity = await _userManager.CreateAsync(identity, RegistrationData.Password);

            if (!createdIdentity.Succeeded)
            {
                throw new Exception(createdIdentity.Errors.ToString());
            }

            return identity;
        }

        private async Task<User> CreateUserAsync(dynamic RegistrationData, IdentityUser identity)
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

                User user = new()
                {
                    IdentityId = identity.Id,
                    BasicInfo = basicInfo,
                };

                _ctx.Users.Add(user);

                await _ctx.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
