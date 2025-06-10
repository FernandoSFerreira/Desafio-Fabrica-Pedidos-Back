using System.ComponentModel.DataAnnotations;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Entities
{
    public class Revenda
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Cnpj { get; set; } = null!; // Obrigatório
        [Required]
        public string RazaoSocial { get; set; } = null!; // Obrigatório
        [Required]
        public string NomeFantasia { get; set; } = null!;  // Obrigatório
        [Required]
        public string Email { get; set; } = null!; // Obrigatório
        public List<string> Telefones { get; set; } = new List<string>(); // Opcional, pode haver mais de um.
        public string NomeContatoPrincipal { get; set; } = null!; // Obrigatório
    }
}
