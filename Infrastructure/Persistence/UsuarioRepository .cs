using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Persistence
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo usuário no banco de dados.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Obtém um usuário pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Usuario?> ObterUsuarioPorId(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Obtém um usuário pelo seu username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Usuario?> ObterUsuarioPorUsername(string? username)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Atualiza um usuário existente no banco de dados.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Deleta um usuário do banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeletarUsuario(Guid id)
        {
            var usuario = await ObterUsuarioPorId(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lista todos os usuários no banco de dados.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Usuario>> ListarTodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
