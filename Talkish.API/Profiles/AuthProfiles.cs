using AutoMapper;
using Talkish.API.DTOs;
using Talkish.Domain.Models;

namespace Talkish.API.Profiles
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<User, RegisteredUserDTO>()
                .ForMember(registeredUserDTO => registeredUserDTO.Id, opt => opt.MapFrom(user => user.UserId))
                .ForMember(registeredUserDTO => registeredUserDTO.FirstName, opt => opt.MapFrom(user => user.BasicInfo.FirstName))
                .ForMember(registeredUserDTO => registeredUserDTO.LastName, opt => opt.MapFrom(user => user.BasicInfo.LastName))
                .ForMember(registeredUserDTO => registeredUserDTO.Email, opt => opt.MapFrom(user => user.BasicInfo.Email));
        }
    }
}
