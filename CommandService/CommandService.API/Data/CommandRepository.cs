using CommandService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.API.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(Guid platformId, Command command)
        {
            if (command is null)
                throw new ArgumentNullException();

            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform is null)
                throw new ArgumentNullException();

            _context.Platforms.Add(platform);
        }

        public bool ExternalPlatformExists(Guid externalPlatformId)
        {
            return _context.Platforms.Any(p => p.ExternalId == externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command? GetCommand(Guid platformId, Guid commandId)
        {
            return _context.Commands.FirstOrDefault(c => c.Id == commandId && c.PlatformId == platformId);
        }

        public IEnumerable<Command> GetCommandsForPlatform(Guid platformId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId)
                                          .OrderBy(c => c.Platform.Name)
                                          .ToList();
        }

        public bool PlatformExists(Guid platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
