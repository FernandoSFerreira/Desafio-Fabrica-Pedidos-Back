using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Desafio_Fabrica_Pedidos_Back.Presentation
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RevendasController : ControllerBase
    {
        private readonly IRevendaService _revendaService;

        public RevendasController(IRevendaService revendaService)
        {
            _revendaService = revendaService;
        }

        /// <summary>
        /// POST /revendas - Cadastra uma nova revenda no sistema.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CadastrarRevenda([FromBody] Revenda revenda)
        {
            try
            {
                var revendaCadastrada = await _revendaService.CadastrarRevenda(revenda);
                return CreatedAtAction(nameof(ObterRevendaPorCnpj), new { cnpj = revendaCadastrada.Cnpj }, revendaCadastrada);
            }
            catch (ArgumentException ex)
            {
                // Lidar com erros de validação/negócio
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Lidar com outros erros inesperados
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// GET /revendas/{cnpj} - Obtém uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> ObterRevendaPorCnpj(string cnpj)
        {
            var revenda = await _revendaService.ObterRevendaPorCnpj(cnpj);

            if (revenda == null)
            {
                return NotFound(); // Retorna 404 se a revenda não for encontrada
            }

            return Ok(revenda);
        }

        /// <summary>
        /// PUT /revendas - Atualiza uma revenda existente no sistema.
        /// </summary>
        /// <param name="revenda"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] Revenda revenda)
        {
            try
            {
                var revendaAtualizada = await _revendaService.Atualizar(revenda);
                return Ok(revendaAtualizada);
            }
            catch (ArgumentException ex)
            {
                // Lidar com erros de validação/negócio
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Lidar com outros erros inesperados
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// DELETE /revendas/{cnpj} - Exclui uma revenda pelo CNPJ.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        [HttpDelete("cnpj/{cnpj}")]
        public async Task<IActionResult> Excluir(string cnpj)
        {
            bool excluido = await _revendaService.Excluir(cnpj);

            if (excluido)
            {
                return NoContent(); // Retorna 204 se a exclusão for bem-sucedida
            }
            else
            {
                return NotFound(); // Retorna 404 se a revenda não for encontrada
            }
        }

        /// <summary>
        /// DELETE /revendas/{id} - Exclui uma revenda pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirPorId(Guid id)
        {
            bool excluido = await _revendaService.Excluir(id);

            if (excluido)
            {
                return NoContent(); // Retorna 204 se a exclusão for bem-sucedida
            }
            else
            {
                return NotFound(); // Retorna 404 se a revenda não for encontrada
            }
        }

        /// <summary>
        /// GET /revendas - Obtém todas as revendas cadastradas no sistema.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ObterTodasRevendas()
        {
            var revendas = await _revendaService.ObterTodasRevendas();
            return Ok(revendas);
        }

        /// <summary>
        /// GET /revendas/{id} - Obtém uma revenda pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterRevendaPorId(Guid id)
        {
            var revenda = await _revendaService.ObterRevendaPorId(id);

            if (revenda == null)
            {
                return NotFound(); // Retorna 404 se a revenda não for encontrada
            }

            return Ok(revenda);
        }
    }
}
