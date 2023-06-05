using Microsoft.EntityFrameworkCore;
using PlatformService.Application.Contracts.Persistence;
using PlatformService.Domain;

namespace PlatformService.Infrastructure.Persistence.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddPlatformAsync(Platform platform)
        {
            await _context.AddAsync(platform);
        }

        public async Task<Platform?> GetByIdAsync(PlatformId id)
        {
            return await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            return await _context.Platforms.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
