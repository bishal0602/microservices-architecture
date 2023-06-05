using AutoMapper;
using CommandService.API.Data;
using CommandService.API.Dtos;
using CommandService.API.Models;
using System.Text.Json;

namespace CommandService.API.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var EventType = DetermineEventType(message);
            switch (EventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    Console.WriteLine($"<--- Undefined event recieved : {message} --->");
                    break;
            }
        }
        private EventType DetermineEventType(string notificationMessage)
        {
            GenericEventDto? genericEventDto = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            return genericEventDto?.Event switch
            {
                "Platform_Published" => EventType.PlatformPublished,
                _ => EventType.Undetermined
            };
        }


        private void AddPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {

                try
                {
                    var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
                    var platformPublishedDto = JsonSerializer.Deserialize<PlatformForPublishDto>(platformPublishedMessage);
                    var platform = _mapper.Map<Platform>(platformPublishedDto);

                    if (!repository.ExternalPlatformExists(platform.ExternalId))
                    {
                        repository.CreatePlatform(platform);
                        repository.SaveChanges();
                        Console.WriteLine("<--- Platform added --->");
                    }
                    else
                    {
                        Console.WriteLine("<--- Platform already exists --->");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"<--- Couldnot add platform to database {ex.Message} --->");
                }
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}
