using CommandService.API.Models;

namespace CommandService.API.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        // Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(Guid platformId);
        bool ExternalPlatformExists(Guid externalPlatformId);

        // Commands
        IEnumerable<Command> GetCommandsForPlatform(Guid platformId);
        Command? GetCommand(Guid platformId, Guid commandId);
        void CreateCommand(Guid platformId, Command command);


    }
}
