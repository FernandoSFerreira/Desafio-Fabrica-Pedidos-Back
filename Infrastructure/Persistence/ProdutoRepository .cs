using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Persistence
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Busca todos os produtos cadastrados no banco de dados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Produto>> BuscarTodosProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        /// <summary>
        /// Busca um produto pelo seu ID no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Produto?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        /// <summary>
        /// Adiciona um novo produto ao banco de dados.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public async Task AdicionarProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza um produto existente no banco de dados.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public async Task AtualizarProdutoAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove a product by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoverProdutoAsync(Guid id)
        {
            var produto = await BuscarPorIdAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
