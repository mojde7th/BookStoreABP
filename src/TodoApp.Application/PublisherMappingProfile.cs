using AutoMapper;
using TodoApp.Domain;
using TodoApp.Dtos;

namespace TodoApp
{
    public class PublisherMappingProfile : Profile
    {
        public PublisherMappingProfile()
        {
            CreateMap<Publisher, PublisherDto>();
            CreateMap<CreatePublisherDto, Publisher>()
                .ForMember(dest => dest.Books, opt => opt.Ignore());
            CreateMap<UpdatePublisherDto, Publisher>()
                .ForMember(dest => dest.Books, opt => opt.Ignore());
        }
    }
}
