using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Cria um novo usuário no banco de dados.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> CriarUsuario(Usuario usuario);

        /// <summary>
        /// Obtém um usuário pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Usuario?> ObterUsuarioPorId(Guid id);

        /// <summary>
        /// Obtém um usuário pelo seu username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<Usuario?> ObterUsuarioPorUsername(string? username);

        /// <summary>
        /// Atualiza os dados de um usuário existente no banco de dados.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> AtualizarUsuario(Usuario usuario);

        /// <summary>
        /// Exclui um usuário pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletarUsuario(Guid id);

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        Task<List<Usuario>> ListarTodosUsuarios();
    }
}
