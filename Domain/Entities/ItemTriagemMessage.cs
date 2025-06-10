using System.ComponentModel.DataAnnotations;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Entities
{
    public class ItemTriagemMessage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
