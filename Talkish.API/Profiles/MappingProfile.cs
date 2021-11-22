using AutoMapper;
using Talkish.API.DTOs;
using Talkish.API.Models;

namespace Talkish.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Blog, BlogDTO>().ReverseMap();

            CreateMap<Author, BlogAuthorDTO>()
                .ForMember(authorDTO => authorDTO.AuthorName, opt => opt.MapFrom(author => $"{author.FirstName} {author.LastName}"));

            CreateMap<Topic, TopicDTO>()
                .ForMember(topicDTO => topicDTO.TopicName, opt => opt.MapFrom(topic => topic.Name));
        }
    }
}
