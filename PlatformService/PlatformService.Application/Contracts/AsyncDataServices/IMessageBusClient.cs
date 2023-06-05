using PlatformService.Domain;

namespace PlatformService.Application.Contracts.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(Platform platform);
    }
}
