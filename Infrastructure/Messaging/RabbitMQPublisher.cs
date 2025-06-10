using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Configurations;
using Desafio_Fabrica_Pedidos_Back.Security;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Messaging
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly RabbitMQ.Client.IConnectionFactory _connectionFactory;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;

        public RabbitMQPublisher(RabbitMQ.Client.IConnectionFactory connectionFactory, IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
        {
            _connectionFactory = connectionFactory;
            _rabbitMqConfiguration = rabbitMqConfiguration.Value;
        }

        public async Task SendMessageAsync(string message)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _rabbitMqConfiguration.QueueTriagem,
                               durable: true,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = new BasicProperties();
            properties.Persistent = true;

            await channel.BasicPublishAsync(exchange: "",
                               routingKey: _rabbitMqConfiguration.QueueTriagem,
                               mandatory: false,
                               basicProperties: properties,
                               body: body);
        }
    }
}