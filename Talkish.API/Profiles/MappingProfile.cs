using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.DTOs;
using Talkish.Domain.Models;

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
