using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Application.Contracts.AsyncDataServices;
using PlatformService.Application.Contracts.Persistence;
using PlatformService.Application.Contracts.SyncDataServices.Http;
using PlatformService.Infrastructure.AsyncDataServices;
using PlatformService.Infrastructure.Persistence;
using PlatformService.Infrastructure.Persistence.Repositories;
using PlatformService.Infrastructure.SyncDataServices.Http;

namespace PlatformService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration, bool IsProductionEnviroment)
        {
            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddGrpc();

            services.AddPersistence(configuration, IsProductionEnviroment);
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration, bool IsProductionEnviroment)
        {
            services.AddScoped<IPlatformRepository, PlatformRepository>();


            if (IsProductionEnviroment)
            {
                Console.WriteLine("Using in SQL Server");
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"), sqlServerOptionsAction =>
                    {
                        sqlServerOptionsAction.EnableRetryOnFailure();
                    });
                });
            }
            else
            {
                Console.WriteLine("Using in memory database");
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("platform-service-inmemory");
                });
            }

            return services;
        }

    }
}
