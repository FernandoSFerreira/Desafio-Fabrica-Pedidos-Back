using System;
using Desafio_Fabrica_Pedidos_Back.Domain.Entities;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IPedidoService
    {
        /// <summary>
        /// Salva um novo pedido no sistema e envia para a fila de triagem via RabbitMQ.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        Task<Guid> SalvarPedidoAsync(Pedido pedido);

        /// <summary>
        /// Obtém um pedido pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Pedido?> ObterPedidoPorIdAsync(Guid id);

        /// <summary>
        /// Atualiza um pedido existente no sistema.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        Task<Pedido?> AtualizarPedidoAsync(Pedido pedido);

        /// <summary>
        /// Obtém todos os pedidos pendentes de uma revenda específica pelo ID da revenda.
        /// </summary>
        /// <param name="revendaId"></param>
        /// <returns></returns>
        Task<List<Pedido>> ObterPedidosPorRevendaAsync(Guid revendaId);

        /// <summary>
        /// Obtém todos os pedidos pendentes do sistema.
        /// </summary>
        /// <returns></returns>
        Task<List<Pedido>> ObterTodosPedidosAsync();

        /// <summary>
        /// Exclui um pedido pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletarPedido(Guid id);

        /// <summary>
        /// Lista todos os pedidos cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        Task<List<Pedido>> ListarTodosPedidos();
    }
}
