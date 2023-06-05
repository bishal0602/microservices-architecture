using Microsoft.Extensions.Configuration;
using PlatformService.Application.Contracts.AsyncDataServices;
using PlatformService.Domain;
using PlatformService.Infrastructure.AsyncDataServices.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PlatformService.Infrastructure.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"] ?? throw new ArgumentNullException("Missing RabbitMQ Port Configuration"))
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("<--- Connected to RabbitMQ MessageBus --->");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<--- Unable to connect to Message Bus: {ex.Message} --->");
            }

        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("<--- RabbitMQ Connection Shutdown --->");
        }

        public void PublishNewPlatform(Platform platform)
        {
            var platformForPublishDto = new PlatformForPublishDto(platform);

            var message = JsonSerializer.Serialize(platformForPublishDto);

            if (_connection.IsOpen)
            {
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("<--- RabbitMQ connection is closed. Not publishing... --->");
            }

        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
                                  routingKey: "",
                                  basicProperties: null,
                                  body: body);

            Console.WriteLine($"<--- Message published: {message} --->");
        }

        public void Dispose()
        {
            Console.WriteLine("<--- Message bus disposed --->");
            if (_connection.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
