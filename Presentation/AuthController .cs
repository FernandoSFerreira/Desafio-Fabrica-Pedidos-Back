using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BCrypt.Net;
using Desafio_Fabrica_Pedidos_Back.Security.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Domain.DTOs.AuthDTOs.Request;
using Microsoft.AspNetCore.Authorization;

namespace Desafio_Fabrica_Pedidos_Back.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtService _jwtService;

        public AuthController(IUsuarioService usuarioService, IJwtService jwtService)
        {
            _jwtService = jwtService;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT se as credenciais forem válidas.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(loginRequest.Password, "$2a$12$tZ0PbBRMMcEohEnF0NBVge", false, hashType: HashType.SHA512);
            var usuario = await _usuarioService.ObterUsuarioPorUsernameAsync(loginRequest.UserName);

            if (usuario == null || usuario.PasswordHash != passwordHash) // Compare com o hash armazenado!
            {
                return Unauthorized("Credenciais inválidas.");
            }

            //if (BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash, false, hashType: HashType.SHA512))
            //{
            //    return Unauthorized("Credenciais inválidas.");
            //}


            var token = _jwtService.GenerateToken(usuario.Id, usuario.Username);
            return Ok(new { Token = token });
        }

        /// <summary>
        /// Refresca um token JWT existente, retornando um novo token.
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            if (!_jwtService.TryDecodeJwt(refreshToken, out SecurityToken decodedToken, out Exception? exception))
            {
                return BadRequest("Token de atualização inválido.");
            }

            var jwt = (JwtSecurityToken)decodedToken;
            var expirationTime = jwt.Payload.Expiration;

            // Verifica se o token expirou
            if (DateTimeOffset.FromUnixTimeSeconds(expirationTime.Value) < DateTime.Now)
            {
                return Unauthorized("Token de atualização expirado.");
            }

            var usuarioId = Guid.Parse(jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value);
            var usuarioUsername = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
            var token = _jwtService.GenerateToken(usuarioId, usuarioUsername);
            return Ok(new { Token = token });
        }


        /// <summary>
        /// Desloga o usuário, invalidando seu token.  Implementação básica (sem blacklist real).
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            // Em uma implementação completa, invalidaria o token aqui.
            // Isso pode envolver adicionar o token a uma blacklist ou revogar sua validade de alguma forma.
            // Como este é um desafio simplificado, apenas reconhecemos que o logout foi solicitado e retornamos uma mensagem de sucesso.

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("Logout realizado com sucesso.");
        }

        
    }
}
