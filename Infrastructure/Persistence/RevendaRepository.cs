using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Persistence
{
    public class RevendaRepository : IRevendaRepository
    {
        private readonly ApplicationDbContext _context;

        public RevendaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastra uma nova revenda no banco de dados.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        public async Task<Revenda> Cadastrar(Revenda revenda)
        {
            _context.Revendas.Add(revenda);
            await _context.SaveChangesAsync();
            return revenda;
        }

        /// <summary>
        /// Obtém uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public async Task<Revenda?> ObterRevendaPorCnpj(string cnpj)
        {
            return await _context.Revendas.FirstOrDefaultAsync(r => r.Cnpj == cnpj);
        }

        /// <summary>
        /// Atualiza os dados de uma revenda existente no banco de dados.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        public async Task<Revenda> Atualizar(Revenda revenda)
        {
            _context.Update(revenda);
            await _context.SaveChangesAsync();
            return revenda;
        }

        /// <summary>
        /// Exclui uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public async Task<bool> Excluir(string cnpj)
        {
            var revenda = await ObterRevendaPorCnpj(cnpj);
            if (revenda != null)
            {
                _context.Revendas.Remove(revenda);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Exclui uma revenda pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExcluirPorId(Guid id)
        {
            var revenda = await ObterRevendaPorId(id);
            if (revenda != null)
            {
                _context.Revendas.Remove(revenda);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Obtém todas as revendas cadastradas no banco de dados.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Revenda>> ObterTodasRevendas()
        {
            return await _context.Revendas.ToListAsync();
        }

        /// <summary>
        /// Obtém uma revenda pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Revenda?> ObterRevendaPorId(Guid id)
        {
            return await _context.Revendas.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
