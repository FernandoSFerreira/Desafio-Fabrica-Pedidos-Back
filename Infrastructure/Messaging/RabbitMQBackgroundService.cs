using Desafio_Fabrica_Pedidos_Back.Infrastructure.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Messaging
{
    public class RabbitMQBackgroundService : BackgroundService
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMQBackgroundService> _logger;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        //private readonly string _queueName = "revenda-pedidos-confirmados";
        private readonly Func<string, Task> _processMessage;
        private IConnection? _connection;
        private IChannel? _channel;

        public RabbitMQBackgroundService(
            IConnectionFactory connectionFactory,
            ILogger<RabbitMQBackgroundService> logger,
            Func<string, Task> processMessage,
            IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _processMessage = processMessage; ;
            _rabbitMqConfiguration = rabbitMqConfiguration.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _connection = await _connectionFactory.CreateConnectionAsync(stoppingToken);
                _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

                await _channel.QueueDeclareAsync(
                    queue: _rabbitMqConfiguration.QueueConfirmados,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: stoppingToken);

                // Configura QoS para processar uma mensagem por vez
                await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        _logger.LogInformation("Mensagem recebida: {Message}", message);

                        await _processMessage(message);

                        // Confirma o processamento
                        await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);

                        _logger.LogInformation("Mensagem processada com sucesso");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao processar mensagem");

                        // Rejeita e recoloca na fila
                        await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
                    }
                };

                await _channel.BasicConsumeAsync(
                    queue: _rabbitMqConfiguration.QueueConfirmados,
                    autoAck: false,
                    consumer: consumer,
                    cancellationToken: stoppingToken);

                _logger.LogInformation("Consumer iniciado para a fila: {QueueName}", _rabbitMqConfiguration.QueueConfirmados);

                // Mantém o serviço rodando até ser cancelado
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no consumer RabbitMQ");
                throw;
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Parando consumer RabbitMQ...");

            if (_channel != null)
            {
                await _channel.CloseAsync(cancellationToken);
                _channel.Dispose();
            }

            if (_connection != null)
            {
                await _connection.CloseAsync(cancellationToken);
                _connection.Dispose();
            }

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}