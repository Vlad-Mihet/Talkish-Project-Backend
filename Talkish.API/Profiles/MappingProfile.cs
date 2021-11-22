using AutoMapper;
using Talkish.Domain.Models;
using Talkish.Domain.DTOs;

namespace Talkish.Domain.Profiles
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
