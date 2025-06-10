using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;

namespace Desafio_Fabrica_Pedidos_Back.Application.Services
{
    public class RevendaService : IRevendaService
    {
        private readonly IRevendaRepository _revendaRepository;

        public RevendaService(IRevendaRepository revendaRepository)
        {
            _revendaRepository = revendaRepository ?? throw new ArgumentNullException(nameof(revendaRepository));
        }

        /// <summary>
        /// Cadastra uma nova revenda no sistema.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<Revenda> CadastrarRevenda(Revenda revenda)
        {
            if (string.IsNullOrEmpty(revenda.Cnpj))
            {
                throw new ArgumentException("CNPJ é obrigatório.");
            }

            var existingRevenda = await _revendaRepository.ObterRevendaPorCnpj(revenda.Cnpj);
            if (existingRevenda != null)
            {
                throw new Exception($"Revenda com CNPJ {revenda.Cnpj} já cadastrada.");
            }

            return await _revendaRepository.Cadastrar(revenda);
        }

        /// <summary>
        /// Obtém uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public async Task<Revenda?> ObterRevendaPorCnpj(string cnpj)
        {
            return await _revendaRepository.ObterRevendaPorCnpj(cnpj);
        }

        /// <summary>
        /// Atualiza os dados de uma revenda existente no sistema.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Revenda> Atualizar(Revenda revenda)
        {
            if (string.IsNullOrEmpty(revenda.Cnpj))
            {
                throw new ArgumentException("CNPJ é obrigatório.");
            }

            return await _revendaRepository.Atualizar(revenda);
        }

        /// <summary>
        /// Exclui uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public async Task<bool> Excluir(string cnpj)
        {
            return await _revendaRepository.Excluir(cnpj);
        }

        public async Task<bool> Excluir(Guid id)
        {
            return await _revendaRepository.ExcluirPorId(id);
        }

        /// <summary>
        /// Obtém todas as revendas cadastradas no sistema.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Revenda>> ObterTodasRevendas()
        {
            return await _revendaRepository.ObterTodasRevendas();
        }

        /// <summary>
        /// Obtém uma revenda pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Revenda?> ObterRevendaPorId(Guid id)
        {
            return await _revendaRepository.ObterRevendaPorId(id);
        }
    }
}
