using AutoMapper;
using Talkish.API.DTOs;
using Talkish.API.Models;

namespace Talkish.API.Profiles
{
    public class AuthorProfiles : Profile
    {
        public AuthorProfiles()
        {
            CreateMap<Author, AddAuthorDTO>()
                .ReverseMap();

            CreateMap<Author, UpdateAuthorDTO>()
                .ReverseMap();

            CreateMap<Author, AuthorDTO>();

            CreateMap<Author, AuthorWithBlogsDTO>();

            CreateMap<Author, BlogAuthorDTO>()
                .ForMember(authorDTO => authorDTO.AuthorName, opt => opt.MapFrom(author => $"{author.FirstName} {author.LastName}"));
        }
    }
}
