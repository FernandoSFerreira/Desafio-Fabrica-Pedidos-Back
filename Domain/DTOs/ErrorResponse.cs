namespace Desafio_Fabrica_Pedidos_Back.Domain.DTOs
{
    public class ErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public List<string>? Details { get; set; }
    }
}
