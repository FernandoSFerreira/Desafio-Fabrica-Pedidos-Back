using System.ComponentModel.DataAnnotations;

namespace Desafio_Fabrica_Pedidos_Back.Domain.Entities
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string? Username { get; set; } // Nome de usuário para login
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string? PasswordHash { get; set; }  // Hash da senha (NÃO armazene a senha em texto plano!)
    }
}
