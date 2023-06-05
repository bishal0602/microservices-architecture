using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Infrastructure.SyncDataServices.Http.Model
{
    public class PlatformDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Publisher { get; set; } = default!;
        public string Cost { get; set; } = default!;
    }
}
