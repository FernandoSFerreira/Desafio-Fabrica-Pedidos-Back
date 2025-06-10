using Desafio_Fabrica_Pedidos_Back.Application.Services;
using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Messaging
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RabbitMQConsumer> _logger;

        public RabbitMQConsumer(ApplicationDbContext context, ILogger<RabbitMQConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ProcessarMensagemAsync(string message)
        {
            try
            {
                _logger.LogInformation("Iniciando processamento da mensagem: {Message}", message);

                // Deserializar a mensagem JSON
                var pedidoConfirmado = JsonSerializer.Deserialize<PedidoConfirmadoMessage>(message);

                if (pedidoConfirmado == null)
                {
                    _logger.LogWarning("Mensagem inválida ou vazia recebida");
                    return;
                }

                // Buscar o pedido no banco de dados
                var pedido = await _context.Pedidos.FindAsync(pedidoConfirmado.PedidoId);

                if (pedido == null)
                {
                    _logger.LogWarning("Pedido com ID {PedidoId} não encontrado", pedidoConfirmado.PedidoId);
                    return;
                }

                // Atualizar o status do pedido
                pedido.Status = pedidoConfirmado.Status;
                pedido.DataConfirmacao = DateTime.Now;

                if (!string.IsNullOrEmpty(pedidoConfirmado.Observacoes))
                {
                    pedido.Observacoes = pedidoConfirmado.Observacoes;
                }

                // Salvar as alterações
                await _context.SaveChangesAsync();

                _logger.LogInformation("Pedido {PedidoId} atualizado com status: {Status}",
                    pedido.Id, pedido.Status);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro ao deserializar mensagem JSON: {Message}", message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem: {Message}", message);
                throw;
            }
        }
    }

    // Classe para representar a mensagem de confirmação do pedido
    public class PedidoConfirmadoMessage
    {
        public Guid PedidoId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Observacoes { get; set; }
        public DateTime DataProcessamento { get; set; }
    }
}