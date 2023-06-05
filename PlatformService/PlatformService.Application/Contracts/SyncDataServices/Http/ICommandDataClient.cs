using PlatformService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Application.Contracts.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(Platform platform);
    }
}
