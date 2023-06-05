using Grpc.Core;
using PlatformService.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Infrastructure.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepository _repository;

        public GrpcPlatformService(IPlatformRepository repository)
        {
            _repository = repository;
        }
        public override async Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = await _repository.GetAllAsync();

            response.Platform.AddRange(platforms.Select(p => new GrpcPlatformModel()
            {
                Name = p.Name,
                PlatformId = p.Id.Value.ToString(),
                Publisher = p.Publisher,
            }));

            return response;
        }
    }
}
