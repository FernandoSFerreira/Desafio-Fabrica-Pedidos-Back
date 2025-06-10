using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Entities
{
    public class Produto
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome do produto é obrigatório.")]
        public string Nome { get; set; } = null!; // Obrigatório

        [Required(ErrorMessage = "Descrição do produto é obrigatória.")]
        public string Descricao { get; set; } = null!;

        [Range(1, 2000, ErrorMessage = "Preço deve ser maior que zero.")]
        public decimal Preco { get; set; } // Obrigatório

        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}
