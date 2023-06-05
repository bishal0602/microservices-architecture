using Microsoft.EntityFrameworkCore;
using PlatformService.Domain;

namespace PlatformService.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Platform> platforms = new()
            {
                Platform.CreateNew("Dot Net", "Microsoft", "Free"),
                Platform.CreateNew("SQL Server Express", "Microsoft", "Free"),
                Platform.CreateNew("Kubernetes", "Cloud Native Computing Foundation", "Free")
            };
            modelBuilder.Entity<Platform>().HasData(platforms);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
