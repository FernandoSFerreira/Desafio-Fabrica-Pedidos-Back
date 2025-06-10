using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;

namespace Desafio_Fabrica_Pedidos_Back.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        /// <summary>
        /// Busca todos os produtos cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Produto>> BuscarTodosProdutosAsync()
        {
            return await _produtoRepository.BuscarTodosProdutosAsync();
        }

        /// <summary>
        /// Busca um produto pelo ID no sistema.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Produto?> BuscarPorIdAsync(Guid id)
        {
            return await _produtoRepository.BuscarPorIdAsync(id);
        }

        /// <summary>
        /// Adiciona um novo produto ao sistema.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public async Task AdicionarProdutoAsync(Produto produto)
        {
            await _produtoRepository.AdicionarProdutoAsync(produto);
        }

        /// <summary>
        /// Atualiza um produto existente no sistema.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        public async Task AtualizarProdutoAsync(Produto produto)
        {
            await _produtoRepository.AtualizarProdutoAsync(produto);
        }

        /// <summary>
        /// Remove a product by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoverProdutoAsync(Guid id)
        {
            return await _produtoRepository.RemoverProdutoAsync(id);
        }
    }
}
