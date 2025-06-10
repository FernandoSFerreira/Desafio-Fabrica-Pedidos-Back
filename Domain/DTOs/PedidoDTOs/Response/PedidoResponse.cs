using Desafio_Fabrica_Pedidos_Back.Presentation;

namespace Desafio_Fabrica_Pedidos_Back.Domain.DTOs.PedidoDTOs.Response
{
    public class PedidoResponse
    {
        public Guid Id { get; set; }
        public Guid RevendaId { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime? DataConfirmacao { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Observacoes { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal QuantidadeTotal { get; set; }
        public List<ItemPedidoResponse> Itens { get; set; } = new List<ItemPedidoResponse>();
    }
}
