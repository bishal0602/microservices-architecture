using PlatformService.Domain;

namespace PlatformService.Application.Contracts.Persistence
{
    public interface IPlatformRepository
    {
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Platform>> GetAllAsync();
        Task<Platform?> GetByIdAsync(PlatformId id);
        Task AddPlatformAsync(Platform platform);
    }
}
