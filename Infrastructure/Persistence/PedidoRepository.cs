using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Persistence
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo pedido no banco de dados.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<Pedido> CriarPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        /// <summary>
        /// Obtém um pedido pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Pedido?> ObterPedidoPorId(Guid id)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Atualiza um pedido existente no banco de dados.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public async Task<Pedido> AtualizarPedido(Pedido pedido)
        {
            _context.Update(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        /// <summary>
        /// Obtém todos os pedidos de uma revenda.
        /// </summary>
        /// <param name="revendaId"></param>
        /// <returns></returns>
        public async Task<List<Pedido>> ObterPedidosPorRevenda(Guid revendaId)
        {
            return await _context.Pedidos.Where(p => p.RevendaId == revendaId).ToListAsync();
        }

        /// <summary>
        /// Obtém todos os pedidos pendentes.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Pedido>> ObterPedidosPendente()
        {
            return await _context.Pedidos.Where(p => p.Status == "Pendente").ToListAsync();
        }

        /// <summary>
        /// Deleta um pedido do banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeletarPedido(Guid id)
        {
            var pedido = await ObterPedidoPorId(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lista todos os pedidos no banco de dados.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Pedido>> ListarTodosPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }
    }
}
