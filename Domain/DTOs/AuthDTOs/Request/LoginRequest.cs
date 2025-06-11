using Desafio_Fabrica_Pedidos_Back.Domain.DTOs.PedidoDTOs.Request;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Fabrica_Pedidos_Back.Domain.DTOs.AuthDTOs.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "UserName é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password é obrigatório")]
        public string Password { get; set; }
    }
}
