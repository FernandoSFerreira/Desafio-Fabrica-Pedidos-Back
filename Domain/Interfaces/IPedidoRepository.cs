using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        /// <summary>
        /// Cria um novo pedido no banco de dados.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        Task<Pedido> CriarPedido(Pedido pedido);

        /// <summary>
        /// Obtém um pedido pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Pedido?> ObterPedidoPorId(Guid id);

        /// <summary>
        /// Atualiza um pedido existente no banco de dados.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        Task<Pedido> AtualizarPedido(Pedido pedido);

        /// <summary>
        /// Obtém todos os pedidos de uma revenda específica pelo ID da revenda.
        /// </summary>
        /// <param name="revendaId"></param>
        /// <returns></returns>
        Task<List<Pedido>> ObterPedidosPorRevenda(Guid revendaId);

        /// <summary>
        /// Obtém um pedido pelo ID e verifica se ele está pendente.
        /// </summary>
        /// <returns></returns>
        Task<List<Pedido>> ObterPedidosPendente();

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
