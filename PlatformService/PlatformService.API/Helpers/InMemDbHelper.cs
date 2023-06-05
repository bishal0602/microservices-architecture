using PlatformService.Domain;
using PlatformService.Infrastructure.Persistence;

namespace PlatformService.API.Helpers
{
    public static class InMemDbHelper
    {
        public static void PopulateDate(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                AppDbContext? appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!appDbContext.Platforms.Any())
                {
                    Console.WriteLine("Seeding");
                    List<Platform> platforms = new()
                    {
                        Platform.CreateNew("Dot Net", "Microsoft", "Free"),
                        Platform.CreateNew("SQL Server Express", "Microsoft", "Free"),
                        Platform.CreateNew("Kubernetes", "Cloud Native Computing Foundation", "Free")
                    };
                    appDbContext.Platforms.AddRange(platforms);
                    appDbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Already populated");
                }
            }
        }
    }
}
