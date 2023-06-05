namespace CommandService.API.Dtos
{
    public class PlatformForPublishDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Event { get; set; } = string.Empty;
    }
}
