using Microsoft.AspNetCore.Mvc;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Desafio_Fabrica_Pedidos_Back.Presentation
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        /// <summary>
        /// CriarUsuario - Cria um novo usuário no sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("Criar")]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var novoUsuario = await _usuarioService.CriarUsuarioAsync(usuario);
                return CreatedAtAction(nameof(ObterUsuarioPorId), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (Exception ex)
            {
                // Log the error properly in a production environment
                return BadRequest(ex.Message); // Or a more descriptive error response
            }
        }

        /// <summary>
        /// ObterUsuarioPorId - Obtém um usuário pelo seu ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ObterPorId/{id}")]
        public async Task<IActionResult> ObterUsuarioPorId([FromRoute] Guid id)
        {
            var usuario = await _usuarioService.ObterUsuarioPorIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// ObterUsuarioPorUsername - Obtém um usuário pelo seu nome de usuário (username).
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("ObterPorUsername/{username}")]
        public async Task<IActionResult> ObterUsuarioPorUsername([FromRoute] string? username)
        {
            var usuario = await _usuarioService.ObterUsuarioPorUsernameAsync(username);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// AtualizarUsuario - Atualiza os dados de um usuário existente no sistema.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("Atualizar")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var usuarioAtualizado = await _usuarioService.AtualizarUsuarioAsync(usuario);
                return Ok(usuarioAtualizado);
            }
            catch (Exception ex)
            {
                // Log the error properly in a production environment
                return BadRequest(ex.Message); // Or a more descriptive error response
            }

        }

        /// <summary>
        /// DeletarUsuario - Exclui um usuário do sistema pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> DeletarUsuario([FromRoute] Guid id)
        {
            bool deletado = await _usuarioService.DeletarUsuarioAsync(id);

            if (!deletado)
            {
                return NotFound();
            }

            return Ok(); // Or StatusCode(204 No Content)
        }

        /// <summary>
        /// ListarTodosUsuarios - Obtém todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListarTodos")]
        public async Task<IActionResult> ListarTodosUsuarios()
        {
            var usuarios = await _usuarioService.ListarTodosUsuariosAsync();
            return Ok(usuarios);
        }
    }
}
