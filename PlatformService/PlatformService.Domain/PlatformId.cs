namespace PlatformService.Domain
{
    public record PlatformId
    {
        public Guid Value { get; init; }
        private PlatformId() { }
        private PlatformId(Guid value) => Value = value;
        public static PlatformId Create(Guid value) => new(value);
        public static PlatformId CreateNew() => new(Guid.NewGuid());
    }
}
