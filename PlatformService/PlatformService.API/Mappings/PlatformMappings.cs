using AutoMapper;

namespace PlatformService.API.Mappings
{
    public class PlatformMappings : Profile
    {
        public PlatformMappings()
        {
            CreateMap<Domain.Platform, Models.PlatformDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            CreateMap<Models.PlatformForCreationDto, Domain.Platform>().ConstructUsing(src => Domain.Platform.CreateNew(src.Name, src.Publisher, src.Cost));

        }
    }
}
