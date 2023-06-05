using PlatformService.Domain;

namespace PlatformService.Infrastructure.AsyncDataServices.Models
{
    public class PlatformForPublishDto
    {
        public PlatformForPublishDto(Platform platform)
        {
            Id = platform.Id.Value;
            Name = platform.Name;
            Event = "Platform_Published";
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Event { get; set; } = string.Empty;
    }
}
