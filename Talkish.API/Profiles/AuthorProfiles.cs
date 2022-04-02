using AutoMapper;
using Talkish.API.DTOs;
using Talkish.Domain.Models;

namespace Talkish.API.Profiles
{
    public class AuthorProfiles : Profile
    {
        public AuthorProfiles()
        {
            CreateMap<Author, AddAuthorDTO>()
                .ReverseMap();

            CreateMap<Author, AuthorDTO>()
                .ForMember(authorDTO => authorDTO.FirstName, opt => opt.MapFrom(author => author.UserProfile.BasicInfo.FirstName))
                .ForMember(authorDTO => authorDTO.LastName, opt => opt.MapFrom(author => author.UserProfile.BasicInfo.LastName));

            CreateMap<Author, AuthorWithBlogsDTO>()
                .ForMember(authorDTO => authorDTO.FirstName, opt => opt.MapFrom(author => author.UserProfile.BasicInfo.FirstName))
                .ForMember(authorDTO => authorDTO.LastName, opt => opt.MapFrom(author => author.UserProfile.BasicInfo.LastName));

            CreateMap<Author, BlogAuthorDTO>()
                .ForMember(authorDTO => authorDTO.AuthorName, opt => opt.MapFrom(author => $"{author.UserProfile.BasicInfo.FirstName} {author.UserProfile.BasicInfo.LastName}"));
        }
    }
}
