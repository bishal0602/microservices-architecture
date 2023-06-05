using System.ComponentModel.DataAnnotations;

namespace CommandService.API.Dtos
{
    public class CommandForCreationDto
    {
        [Required]
        public string HowTo { get; set; } = string.Empty;
        [Required]
        public string CommandLine { get; set; } = string.Empty;
    }
}
