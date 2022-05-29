using Microsoft.AspNetCore.Identity;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Talkish.Dal;
using Microsoft.Extensions.Logging;
using Talkish.Domain.Models;
using Talkish.Services.DTOs;
using Microsoft.Extensions.Options;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Talkish.Services.Options;
using Microsoft.EntityFrameworkCore;

namespace Talkish.Services
{
    public class AuthService
    {
        private readonly AppDbContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly JwtSettings _jwtSettings;
        private readonly byte[] _key;

        public AuthService(UserManager<IdentityUser> userManager, AppDbContext ctx, ILogger<AuthService> logger, IOptions<JwtSettings> jwtOptions)
        {
            _ctx = ctx;
            _userManager = userManager;
            _logger = logger;
            _jwtSettings = jwtOptions.Value;
            _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
        }

        public async Task<string> Login(LoginDTO LoginData)
        {
            try
            {
                IdentityUser identityUser = await ValidateAndGetIdentityAsync(LoginData);

                User user = await _ctx.Users.FirstOrDefaultAsync((user) => user.IdentityId == identityUser.Id);

                if (user is null)
                {
                    throw new Exception("User doesn't exist");
                }

                return GetJwtString(identityUser, user);

            }
            catch (Exception e)
            {
                _logger.LogDebug(JsonConvert.SerializeObject(e));
                throw;
            }
        }

        public async Task<string> Register(RegisterDTO RegistrationData)
        {
            await using var transaction = await _ctx.Database.BeginTransactionAsync();

            try
            {
                var existingIdentity = await ValidateIdentityDoesNotExist(RegistrationData);

                if (existingIdentity != null)
                {
                    throw new Exception("User does already exist");
                }

                var identity = await CreateIdentityUserAsync(RegistrationData);

                var user = await CreateUserAsync(RegistrationData, identity);

                if (identity is null || user is null)
                {
                    throw new Exception("There was an issue creating the user");
                }

                await transaction.CommitAsync();

                return GetJwtString(identity, user);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(JsonConvert.SerializeObject(ex));
                await transaction.RollbackAsync();
                throw;
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

        private async Task<IdentityUser> CreateIdentityUserAsync(dynamic RegistrationData)
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

        public readonly JwtSecurityTokenHandler TokenHandler = new();

        public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
        {
            var tokenDescriptor = GetTokenDescriptor(identity);

            return TokenHandler.CreateToken(tokenDescriptor);
        }

        public string WriteToken(SecurityToken token)
        {
            return TokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.Now.AddHours(48),
                Audience = _jwtSettings.Audiences[0],
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private string GetJwtString(IdentityUser identityUser, User user)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
            new Claim("IdentityId", identityUser.Id),
            new Claim("UserId", user.UserId.ToString())
            });

            var token = this.CreateSecurityToken(claimsIdentity);
            return this.WriteToken(token);
        }
    }
}
