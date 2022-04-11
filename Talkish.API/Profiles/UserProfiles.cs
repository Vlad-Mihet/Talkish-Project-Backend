using AutoMapper;
using Talkish.Domain.Models;
using Talkish.API.DTOs;

namespace Talkish.API.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, GetUserDTO>()
                .ForMember(getUserDTO => getUserDTO.Email, opt => opt.MapFrom(user => user.BasicInfo.Email))
                .ForMember(getUserDTO => getUserDTO.FirstName, opt => opt.MapFrom(user => user.BasicInfo.FirstName))
                .ForMember(GetUserDTO => GetUserDTO.LastName, opt => opt.MapFrom(user => user.BasicInfo.LastName));
        
            CreateMap<User, UserWithBioDTO>()
                .ForMember(getUserDTO => getUserDTO.Email, opt => opt.MapFrom(user => user.BasicInfo.Email))
                .ForMember(getUserDTO => getUserDTO.FirstName, opt => opt.MapFrom(user => user.BasicInfo.FirstName))
                .ForMember(GetUserDTO => GetUserDTO.LastName, opt => opt.MapFrom(user => user.BasicInfo.LastName));

            CreateMap<User, FollowerDTO>()
                .ForMember(getUserDTO => getUserDTO.FirstName, opt => opt.MapFrom(user => user.BasicInfo.FirstName))
                .ForMember(GetUserDTO => GetUserDTO.LastName, opt => opt.MapFrom(user => user.BasicInfo.LastName));

            CreateMap<User, FollowerWithBioDTO>()
                .ForMember(getUserDTO => getUserDTO.FirstName, opt => opt.MapFrom(user => user.BasicInfo.FirstName))
                .ForMember(GetUserDTO => GetUserDTO.LastName, opt => opt.MapFrom(user => user.BasicInfo.LastName));
        }
    }
}
