using AutoMapper;
using Talkish.API.DTOs;
using Talkish.API.Models;

namespace Talkish.API.Profiles
{
    public class BlogProfiles : Profile
    {
        public BlogProfiles()
        {

            CreateMap<Blog, AddBlogDTO>()
                .ReverseMap();

            CreateMap<Blog, BlogDTO>();
        }
    }
}
