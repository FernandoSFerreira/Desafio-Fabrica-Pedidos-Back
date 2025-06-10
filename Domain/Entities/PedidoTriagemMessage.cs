using System.ComponentModel.DataAnnotations;
using static Desafio_Fabrica_Pedidos_Back.Application.Services.PedidoService;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Entities
{
    public class PedidoTriagemMessage
    {
        [Key]
        public Guid PedidoId { get; set; }
        public Guid RevendaId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal QuantidadeTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemTriagemMessage> Itens { get; set; } = new List<ItemTriagemMessage>();
    }
}
