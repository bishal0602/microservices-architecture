using AutoMapper;
using CommandService.API.Data;
using CommandService.API.Dtos;
using CommandService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.API.Controllers
{
    [ApiController]
    [Route("api/c/platforms")]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepository commandRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult TestEndpoint()
        {
            Console.WriteLine("<--- Test Endpoint was called --->");
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformDto>> GetPlatforms()
        {
            Console.WriteLine("<--- Getting Platforms from CommandService --->");

            IEnumerable<Platform> platforms = _commandRepository.GetAllPlatforms();
            IEnumerable<PlatformDto> platformsDto = _mapper.Map<IEnumerable<PlatformDto>>(platforms);
            return Ok(platformsDto);
        }
    }
}
