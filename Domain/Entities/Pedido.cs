using System.ComponentModel.DataAnnotations;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Entities
{
    public class Pedido
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RevendaId { get; set; } // Chave estrangeira para a revenda

        [Required]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        public DateTime? DataConfirmacao { get; set; } // Data de confirmação do pedido

        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pendente"; // Status do pedido (Pendente, Confirmado, Cancelado, Em Processamento, Enviado)

        [MaxLength(500)]
        public string? Observacoes { get; set; } // Observações adicionais sobre o pedido

        // Propriedade para calcular o valor total do pedido (para validação)
        public decimal ValorTotal => Itens?.Sum(i => i.Quantidade * i.PrecoUnitario) ?? 0;

        // Propriedade calculada para quantidade total
        public decimal QuantidadeTotal => Itens?.Sum(i => i.Quantidade) ?? 0;
    }
}
