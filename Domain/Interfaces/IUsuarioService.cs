using System;
using System.Collections.Generic;
using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using System.Threading.Tasks;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> CriarUsuarioAsync(Usuario usuario);

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Usuario?> ObterUsuarioPorIdAsync(Guid id);

        /// <summary>
        /// Obtém um usuário pelo username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<Usuario?> ObterUsuarioPorUsernameAsync(string? username);

        /// <summary>
        /// Atualiza os dados de um usuário existente no sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> AtualizarUsuarioAsync(Usuario usuario);

        /// <summary>
        /// Exclui um usuário pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeletarUsuarioAsync(Guid id);

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        Task<List<Usuario>> ListarTodosUsuariosAsync();
    }
}
