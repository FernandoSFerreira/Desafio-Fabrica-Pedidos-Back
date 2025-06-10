using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Desafio_Fabrica_Pedidos_Back.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IRabbitMQPublisher _rabbitMQPublisher;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IRabbitMQPublisher rabbitMQPublisher, IPedidoRepository pedidoRepository)
        {
            _rabbitMQPublisher = rabbitMQPublisher ?? throw new ArgumentNullException(nameof(rabbitMQPublisher));
            _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
        }

        /// <summary>
        /// Salva um novo pedido no sistema e envia para a fila de triagem via RabbitMQ.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Guid> SalvarPedidoAsync(Pedido pedido)
        {
            // Validação do pedido
            if (pedido.Itens.Sum(item => item.Quantidade) < 1000)
            {
                throw new ArgumentException("O pedido deve ter pelo menos 1000 unidades.");
            }

            // Criar novo pedido com ID gerado
            var novoPedido = new Pedido
            {
                RevendaId = pedido.RevendaId,
                DataPedido = DateTime.Now,
                Status = "Pendente",
                Itens = pedido.Itens
            };

            novoPedido = await _pedidoRepository.CriarPedido(novoPedido); // Usando o repositório para criar o pedido

            // Enviar mensagem para fila de triagem
            await EnviarPedidoParaTriagemAsync(novoPedido);

            return novoPedido.Id;
        }

        /// <summary>
        /// Obtém um pedido pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Pedido?> ObterPedidoPorIdAsync(Guid id)
        {
            return await _pedidoRepository.ObterPedidoPorId(id); // Usando o repositório para obter o pedido por ID
        }

        /// <summary>
        /// Atualiza um pedido existente no sistema.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<Pedido?> AtualizarPedidoAsync(Pedido pedido)
        {
            return await _pedidoRepository.AtualizarPedido(pedido);
        }

        /// <summary>
        /// Obtém todos os pedidos de uma revenda específica pelo ID da revenda.
        /// </summary>
        /// <param name="revendaId"></param>
        /// <returns></returns>
        public async Task<List<Pedido>> ObterPedidosPorRevendaAsync(Guid revendaId)
        {
            return await _pedidoRepository.ObterPedidosPorRevenda(revendaId); // Usando o repositório para obter pedidos por revenda
        }

        /// <summary>
        /// Obtém todos os pedidos pendentes do sistema.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Pedido>> ObterTodosPedidosAsync()
        {
            return await _pedidoRepository.ObterPedidosPendente();
        }

        /// <summary>
        /// Exclui um pedido pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeletarPedido(Guid id)
        {
            return await _pedidoRepository.DeletarPedido(id);
        }

        /// <summary>
        /// Lista todos os pedidos cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Pedido>> ListarTodosPedidos()
        {
            return await _pedidoRepository.ListarTodosPedidos();
        }

        /// <summary>
        /// Envia o pedido para a fila de triagem via RabbitMQ.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        private async Task EnviarPedidoParaTriagemAsync(Pedido pedido)
        {
            try
            {
                var mensagemTriagem = new PedidoTriagemMessage
                {
                    PedidoId = pedido.Id,
                    RevendaId = pedido.RevendaId,
                    DataPedido = pedido.DataPedido,
                    QuantidadeTotal = pedido.QuantidadeTotal,
                    ValorTotal = pedido.ValorTotal,
                    Itens = pedido.Itens.Select(i => new ItemTriagemMessage
                    {
                        ProdutoId = i.ProdutoId,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario
                    }).ToList()
                };

                var jsonMessage = JsonSerializer.Serialize(mensagemTriagem);
                await _rabbitMQPublisher.SendMessageAsync(jsonMessage);
            }
            catch (Exception ex)
            {
                // Log do erro, mas não impede o salvamento do pedido
                Console.WriteLine($"Erro ao enviar mensagem para triagem: {ex.Message}");
            }
        }
    }
}
