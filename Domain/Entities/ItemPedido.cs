using System.ComponentModel.DataAnnotations;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Entities
{
    public class ItemPedido
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid PedidoId { get; set; }
        [Required]
        public Guid ProdutoId { get; set; }
        public Pedido Pedido { get; set; }
        [Required]
        public decimal Quantidade { get; set; }
        [Required]
        public decimal PrecoUnitario { get; set; }
        public Produto Produto { get; set; } = null!; // Obrigatório, pois cada item de pedido deve estar associado a um produto
    }
}
