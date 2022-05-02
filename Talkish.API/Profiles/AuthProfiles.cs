using AutoMapper;
using Talkish.API.DTOs;
using Talkish.Domain.Models;

namespace Talkish.API.Profiles
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<User, RegisteredUserDTO>();
        }
    }
}
