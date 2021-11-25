using AutoMapper;
using Talkish.API.DTOs;
using Talkish.API.Models;

namespace Talkish.API.Profiles
{
    public class BlogProfiles : Profile
    {
        public BlogProfiles()
        {

            CreateMap<Blog, AuthorBlogDTO>();

            CreateMap<Blog, UpdateBlogDTO>()
                .ReverseMap();

            CreateMap<Blog, AddBlogDTO>()
                .ReverseMap();

            CreateMap<Blog, BlogDTO>();
        }
    }
}
