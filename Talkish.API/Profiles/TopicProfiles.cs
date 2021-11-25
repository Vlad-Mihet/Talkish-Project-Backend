using AutoMapper;
using Talkish.API.Models;
using Talkish.API.DTOs;


namespace Talkish.API.Profiles
{
    public class TopicProfiles : Profile
    {
        public TopicProfiles()
        {
            CreateMap<Topic, AddTopicDTO>()
                .ReverseMap();

            CreateMap<Topic, UpdateTopicDTO>()
                .ReverseMap();

            CreateMap<Topic, TopicDTO>()
                    .ForMember(topicDTO => topicDTO.TopicName, opt => opt.MapFrom(topic => topic.Name));
        }
    }
}
