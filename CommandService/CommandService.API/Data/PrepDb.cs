using CommandService.API.SyncDataServices.Grpc;

namespace CommandService.API.Data
{
    public static class PrepDb
    {
        public static IApplicationBuilder PopulateData(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = scope.ServiceProvider.GetService<IPlatformDataClient>();
                var commandRepository = scope.ServiceProvider.GetService<ICommandRepository>()!;

                IEnumerable<Models.Platform>? platforms = grpcClient?.ReturnAllPlatforms();

                if (platforms != null && platforms.Any())
                {
                    Console.WriteLine("<--- Seeding Database --->");

                    foreach (var platform in platforms)
                    {
                        if (!commandRepository.ExternalPlatformExists(platform.ExternalId))
                        {
                            commandRepository.CreatePlatform(platform);
                        }
                    }
                    commandRepository.SaveChanges();
                }

            }

            return app;
        }
    }
}
