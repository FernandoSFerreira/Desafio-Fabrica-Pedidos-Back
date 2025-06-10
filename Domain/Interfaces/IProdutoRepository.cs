using Desafio_Fabrica_Pedidos_Back.Domain.Entities;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        /// <summary>
        /// Busca todos os produtos cadastrados no banco de dados.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Produto>> BuscarTodosProdutosAsync();

        /// <summary>
        /// Busca um produto pelo ID no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Produto?> BuscarPorIdAsync(Guid id);

        /// <summary>
        /// Adiciona um novo produto ao sistema e o salva no banco de dados.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        Task AdicionarProdutoAsync(Produto produto);

        /// <summary>
        /// Atualiza um produto existente no sistema.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        Task AtualizarProdutoAsync(Produto produto);

        /// <summary>
        /// Remove a product by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoverProdutoAsync(Guid id);
    }
}
