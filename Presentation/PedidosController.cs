using Desafio_Fabrica_Pedidos_Back.Application.Services;
using Desafio_Fabrica_Pedidos_Back.Domain.DTOs;
using Desafio_Fabrica_Pedidos_Back.Domain.DTOs.PedidoDTOs.Request;
using Desafio_Fabrica_Pedidos_Back.Domain.DTOs.PedidoDTOs.Response;
using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Fabrica_Pedidos_Back.Presentation
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly ILogger<PedidosController> _logger;

        public PedidosController(IPedidoService pedidoService, ILogger<PedidosController> logger)
        {
            _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        /// <param name="request">Dados do pedido a ser criado</param>
        /// <returns>ID do pedido criado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CriarPedidoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Message = "Dados inválidos",
                        Details = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                    });
                }

                var pedido = new Pedido
                {
                    RevendaId = request.RevendaId,
                    Itens = request.Itens.Select(i => new ItemPedido
                    {
                        ProdutoId = i.ProdutoId,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario
                    }).ToList()
                };

                var pedidoId = await _pedidoService.SalvarPedidoAsync(pedido);

                _logger.LogInformation("Pedido {PedidoId} criado com sucesso para revenda {RevendaId}",
                    pedidoId, request.RevendaId);

                return CreatedAtAction(nameof(ObterPedidoPorId),
                    new { id = pedidoId },
                    new CriarPedidoResponse { PedidoId = pedidoId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Erro de validação ao criar pedido: {Message}", ex.Message);
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao criar pedido");
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Erro interno do servidor ao criar pedido"
                });
            }
        }

        /// <summary>
        /// Obtém um pedido pelo ID
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>Dados do pedido</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterPedidoPorId(Guid id)
        {
            try
            {
                var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);

                if (pedido == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        Message = $"Pedido com ID {id} não encontrado"
                    });
                }

                var response = new PedidoResponse
                {
                    Id = pedido.Id,
                    RevendaId = pedido.RevendaId,
                    DataPedido = pedido.DataPedido,
                    DataConfirmacao = pedido.DataConfirmacao,
                    Status = pedido.Status,
                    Observacoes = pedido.Observacoes,
                    ValorTotal = pedido.ValorTotal,
                    QuantidadeTotal = pedido.QuantidadeTotal,
                    Itens = pedido.Itens.Select(i => new ItemPedidoResponse
                    {
                        ProdutoId = i.ProdutoId,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario,
                        ValorTotal = i.Quantidade * i.PrecoUnitario
                    }).ToList()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pedido {PedidoId}", id);
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Erro interno do servidor ao obter pedido"
                });
            }
        }

        /// <summary>
        /// Atualiza um pedido existente
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarPedidoAsync([FromBody] Pedido pedido, Guid id)
        {
            if (id != pedido.Id)
            {
                return BadRequest("O ID do objeto não corresponde ao ID da rota.");
            }

            var resultado = await _pedidoService.AtualizarPedidoAsync(pedido);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Obtém todos os pedidos de uma revenda
        /// </summary>
        /// <param name="revendaId">ID da revenda</param>
        /// <returns>Lista de pedidos da revenda</returns>
        [HttpGet("revenda/{revendaId:guid}")]
        [ProducesResponseType(typeof(List<PedidoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterPedidosPorRevenda(Guid revendaId)
        {
            try
            {
                var pedidos = await _pedidoService.ObterPedidosPorRevendaAsync(revendaId);

                var response = pedidos.Select(pedido => new PedidoResponse
                {
                    Id = pedido.Id,
                    RevendaId = pedido.RevendaId,
                    DataPedido = pedido.DataPedido,
                    DataConfirmacao = pedido.DataConfirmacao,
                    Status = pedido.Status,
                    Observacoes = pedido.Observacoes,
                    ValorTotal = pedido.ValorTotal,
                    QuantidadeTotal = pedido.QuantidadeTotal,
                    Itens = pedido.Itens.Select(i => new ItemPedidoResponse
                    {
                        ProdutoId = i.ProdutoId,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario,
                        ValorTotal = i.Quantidade * i.PrecoUnitario
                    }).ToList()
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pedidos da revenda {RevendaId}", revendaId);
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Erro interno do servidor ao obter pedidos"
                });
            }
        }

        /// <summary>
        /// Obtém todos os pedidos do sistema
        /// </summary>
        /// <returns>Lista de todos os pedidos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PedidoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodosPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.ObterTodosPedidosAsync();

                var response = pedidos.Select(pedido => new PedidoResponse
                {
                    Id = pedido.Id,
                    RevendaId = pedido.RevendaId,
                    DataPedido = pedido.DataPedido,
                    DataConfirmacao = pedido.DataConfirmacao,
                    Status = pedido.Status,
                    Observacoes = pedido.Observacoes,
                    ValorTotal = pedido.ValorTotal,
                    QuantidadeTotal = pedido.QuantidadeTotal,
                    Itens = pedido.Itens.Select(i => new ItemPedidoResponse
                    {
                        ProdutoId = i.ProdutoId,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario,
                        ValorTotal = i.Quantidade * i.PrecoUnitario
                    }).ToList()
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os pedidos");
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Erro interno do servidor ao obter pedidos"
                });
            }
        }

        /// <summary>
        /// Deleta um pedido pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<PedidoResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<PedidoResponse>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarPedido(Guid id)
        {
            bool deleted = await _pedidoService.DeletarPedido(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Lista todos os pedidos cadastrados no sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet("listarTodos")]
        [ProducesResponseType(typeof(List<PedidoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodosPedidos()
        {
            var pedidos = await _pedidoService.ListarTodosPedidos();
            return Ok(pedidos);
        }
    }
}