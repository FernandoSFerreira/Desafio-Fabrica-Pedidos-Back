using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Desafio_Fabrica_Pedidos_Back.Presentation
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// GET /produtos - Obtém todos os produtos cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> BuscarTodosProdutos()
        {
            var produtos = await _produtoService.BuscarTodosProdutosAsync();
            return Ok(produtos);
        }

        /// <summary>
        /// GET /produtos/{id} - Obtém um produto pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var produto = await _produtoService.BuscarPorIdAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }


        /// <summary>
        /// POST /produtos - Adiciona um novo produto.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto([FromBody] Produto produto)
        {
            try
            {
                await _produtoService.AdicionarProdutoAsync(produto);
                return CreatedAtAction(nameof(BuscarPorId), new { id = produto.Id }, produto);
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
        /// PUT /produtos - Atualiza um produto existente.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto([FromBody] Produto produto)
        {
            try
            {
                await _produtoService.AtualizarProdutoAsync(produto);
                return Ok(produto);
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
        /// DELETE /produtos/{id} - Remove um produto pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverProduto(Guid id)
        {
            bool removido = await _produtoService.RemoverProdutoAsync(id);

            if (removido)
            {
                return NoContent(); // Retorna 204 se a exclusão for bem-sucedida
            }
            else
            {
                return NotFound(); // Retorna 404 se o produto não for encontrado
            }
        }

    }
}
