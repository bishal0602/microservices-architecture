using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.API.Models;
using PlatformService.Application.Contracts.AsyncDataServices;
using PlatformService.Application.Contracts.Persistence;
using PlatformService.Application.Contracts.SyncDataServices.Http;
using PlatformService.Domain;

namespace PlatformService.API.Controllers
{
    [ApiController]
    [Route("api/platforms")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository platformRepository, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Platform>>> GetPlatforms()
        {
            IEnumerable<Platform> platforms = await _platformRepository.GetAllAsync();
            IEnumerable<PlatformDto> platformsDto = _mapper.Map<IEnumerable<PlatformDto>>(platforms);
            return base.Ok(platformsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Platform>> GetPlatformById(Guid id)
        {
            Platform? platform = await _platformRepository.GetByIdAsync(PlatformId.Create(id));
            if (platform == null)
            {
                return NotFound();
            }

            PlatformDto platformDto = _mapper.Map<PlatformDto>(platform);
            return base.Ok(platformDto);
        }
        [HttpPost]
        public async Task<ActionResult<Platform>> AddPlatform([FromBody] PlatformForCreationDto platformCreateDto)
        {
            Platform platform = _mapper.Map<Platform>(platformCreateDto);

            await _platformRepository.AddPlatformAsync(platform);
            await _platformRepository.SaveChangesAsync();
            //// Synchronously
            //try
            //{
            //    await _commandDataClient.SendPlatformToCommand(platform);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Failed to send data to command service(Synchronously) : ", ex.Message);
            //}

            // Asynchronously
            try
            {
                _messageBusClient.PublishNewPlatform(platform);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send data to command service(Asynchronously) : ", ex.Message);
            }

            PlatformDto platformDto = _mapper.Map<PlatformDto>(platform);
            return base.CreatedAtAction(nameof(GetPlatformById), new { id = platform.Id }, platformDto);
        }
    }
}
