using AutoMapper;
using CommandService.API.Dtos;
using CommandService.API.Models;
using PlatformService;

namespace CommandService.API.Mappings
{
    public class CommandMapping : Profile
    {
        public CommandMapping()
        {
            CreateMap<Platform, PlatformDto>();

            CreateMap<Command, CommandDto>();
            CreateMap<CommandForCreationDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<PlatformForPublishDto, Platform>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));


            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => Guid.Parse(src.PlatformId)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }
    }
}
