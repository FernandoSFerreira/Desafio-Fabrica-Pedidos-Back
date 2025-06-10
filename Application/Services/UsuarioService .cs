using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio_Fabrica_Pedidos_Back.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        /// <summary>
        /// Cadastra um novo usuário no sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> CriarUsuarioAsync(Usuario usuario)
        {
            return await _usuarioRepository.CriarUsuario(usuario);
        }

        /// <summary>
        /// Cadastra um novo usuário no sistema.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Usuario?> ObterUsuarioPorIdAsync(Guid id)
        {
            return await _usuarioRepository.ObterUsuarioPorId(id);
        }

        /// <summary>
        /// Obtém um usuário pelo seu username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Usuario?> ObterUsuarioPorUsernameAsync(string? username)
        {
            return await _usuarioRepository.ObterUsuarioPorUsername(username);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente no sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> AtualizarUsuarioAsync(Usuario usuario)
        {
            return await _usuarioRepository.AtualizarUsuario(usuario);
        }

        /// <summary>
        /// Exclui um usuário pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeletarUsuarioAsync(Guid id)
        {
            return await _usuarioRepository.DeletarUsuario(id);
        }

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Usuario>> ListarTodosUsuariosAsync()
        {
            return await _usuarioRepository.ListarTodosUsuarios();
        }
    }
}
