using System.ComponentModel.DataAnnotations;

namespace PlatformService.API.Models
{
    public class PlatformForCreationDto
    {
        [Required]
        public string Name { get; init; } = string.Empty;
        [Required]
        public string Publisher { get; init; } = string.Empty;
        [Required]
        public string Cost { get; init; } = string.Empty;
    }
}
