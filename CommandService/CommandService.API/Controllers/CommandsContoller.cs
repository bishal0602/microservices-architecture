using AutoMapper;
using CommandService.API.Data;
using CommandService.API.Dtos;
using CommandService.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.API.Controllers
{
    [ApiController]
    [Route("api/c/platforms{platformId}/commands")]
    public class CommandsContoller : ControllerBase
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public CommandsContoller(ICommandRepository commandRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandDto>> GetCommandsForPlatform(Guid platformId)
        {
            Console.WriteLine("<--- Getting Commands from CommandService --->");

            if (!(_commandRepository.PlatformExists(platformId)))
            {
                return NotFound($"Platform with id {platformId} not found");
            }

            var commands = _commandRepository.GetCommandsForPlatform(platformId);
            IEnumerable<CommandDto> commandsDto = _mapper.Map<IEnumerable<CommandDto>>(commands);

            return Ok(commandsDto);
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandDto> GetCommandForPlatform(Guid platformId, Guid commandId)
        {
            Console.WriteLine("<--- Getting Command from CommandService --->");

            if (!(_commandRepository.PlatformExists(platformId)))
            {
                return NotFound($"Platform with id {platformId} not found");
            }

            Models.Command? command = _commandRepository.GetCommand(platformId, commandId);

            if (command is null)
                return NotFound($"Command with id {commandId} not found");
            CommandDto commandDto = _mapper.Map<CommandDto>(command);

            return Ok(commandDto);
        }

        [HttpPost]
        public ActionResult<CommandDto> CreateCommandForPlatform(Guid platformId, CommandForCreationDto command)
        {
            Console.WriteLine("<--- Creating Command from CommandService --->");

            if (!(_commandRepository.PlatformExists(platformId)))
            {
                return NotFound($"Platform with id {platformId} not found");
            }

            Command commandToAdd = _mapper.Map<Command>(command);

            _commandRepository.CreateCommand(platformId, commandToAdd);
            _commandRepository.SaveChanges();

            var commandToReturn = _mapper.Map<CommandDto>(commandToAdd);

            return CreatedAtAction(nameof(GetCommandForPlatform), new { platformId, commandId = commandToReturn.Id }, commandToReturn);
        }
    }
}
