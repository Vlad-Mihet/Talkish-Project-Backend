using AutoMapper;
using Talkish.API.DTOs;
using Talkish.Domain.Models;

namespace Talkish.API.Profiles
{
    public class PublicationProfiles : Profile
    {
        public PublicationProfiles()
        {
            CreateMap<Publication, AddPublicationDTO>()
                .ReverseMap();

            CreateMap<Publication, UpdatePublicationDTO>()
                .ReverseMap();

            CreateMap<Publication, PublicationDTO>();
        }
    }
}
