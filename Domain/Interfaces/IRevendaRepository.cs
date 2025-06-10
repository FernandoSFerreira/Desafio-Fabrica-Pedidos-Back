using Desafio_Fabrica_Pedidos_Back.Domain.Entities;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IRevendaRepository
    {
        /// <summary>
        /// Cadastra uma nova revenda no banco de dados.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        Task<Revenda> Cadastrar(Revenda revenda);

        /// <summary>
        /// Obtém uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        Task<Revenda?> ObterRevendaPorCnpj(string cnpj);

        /// <summary>
        /// Atualiza os dados de uma revenda existente no banco de dados.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        Task<Revenda> Atualizar(Revenda revenda);

        /// <summary>
        /// Exclui uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        Task<bool> Excluir(string cnpj);

        /// <summary>
        /// Exclui uma revenda pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExcluirPorId(Guid id);

        /// <summary>
        /// Obtém todas as revendas cadastradas no banco de dados.
        /// </summary>
        /// <returns></returns>
        Task<List<Revenda>> ObterTodasRevendas();

        /// <summary>
        /// Obtém uma revenda pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Revenda?> ObterRevendaPorId(Guid id);
    }
}
