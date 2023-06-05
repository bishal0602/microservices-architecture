namespace PlatformService.Domain
{
    public class Platform
    {
        public PlatformId Id { get; private set; }
        public string Name { get; private set; }
        public string Publisher { get; private set; }
        public string Cost { get; private set; }
        private Platform(PlatformId id, string name, string publisher, string cost)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Cost = cost;
        }
        public static Platform Create(PlatformId id, string name, string publisher, string cost) => new(id, name, publisher, cost);
        public static Platform CreateNew(string name, string publisher, string cost) => new(PlatformId.CreateNew(), name, publisher, cost);
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Platform() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
