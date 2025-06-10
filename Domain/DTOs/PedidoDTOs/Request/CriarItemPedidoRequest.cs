using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace Desafio_Fabrica_Pedidos_Back.Domain.DTOs.PedidoDTOs.Request
{
    public class CriarItemPedidoRequest
    {
        [Required(ErrorMessage = "ProdutoId é obrigatório")]
        public Guid ProdutoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Preço unitário deve ser maior que zero")]
        public decimal PrecoUnitario { get; set; }
    }
}
