using Desafio_Fabrica_Pedidos_Back.Presentation;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Fabrica_Pedidos_Back.Domain.DTOs.PedidoDTOs.Request
{
    public class CriarPedidoRequest
    {
        [Required(ErrorMessage = "RevendaId é obrigatório")]
        public Guid RevendaId { get; set; }

        [Required(ErrorMessage = "Itens são obrigatórios")]
        [MinLength(1, ErrorMessage = "Deve haver pelo menos um item no pedido")]
        public List<CriarItemPedidoRequest> Itens { get; set; } = new List<CriarItemPedidoRequest>();
    }
}
