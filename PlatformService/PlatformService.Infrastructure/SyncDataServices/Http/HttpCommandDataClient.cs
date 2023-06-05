using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using PlatformService.Application.Contracts.SyncDataServices.Http;
using PlatformService.Domain;
using PlatformService.Infrastructure.SyncDataServices.Http.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.Infrastructure.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(Platform platform)
        {
            var platformDto = new PlatformDto { Id = platform.Id.Value, Name = platform.Name, Publisher = platform.Publisher, Cost = platform.Cost };
            StringContent content = new StringContent(JsonSerializer.Serialize(platformDto), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(_configuration["CommandService"], content);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Succesfully posted to command service");
            }
            else
            {
                Console.WriteLine("Failed to post to command Service");
            }
        }
    }
}
