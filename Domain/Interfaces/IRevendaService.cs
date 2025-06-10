using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using System.Threading.Tasks;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IRevendaService
    {
        /// <summary>
        /// Cadastra uma nova revenda no sistema.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        Task<Revenda> CadastrarRevenda(Revenda revenda);

        /// <summary>
        /// Obtém uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        Task<Revenda?> ObterRevendaPorCnpj(string cnpj);

        /// <summary>
        /// Atualiza os dados de uma revenda existente no sistema.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
        Task<bool> Excluir(Guid id);

        /// <summary>
        /// Obtém todas as revendas cadastradas no sistema.
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
