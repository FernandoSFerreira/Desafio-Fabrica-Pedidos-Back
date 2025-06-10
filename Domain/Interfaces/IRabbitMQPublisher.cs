namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IRabbitMQPublisher
    {
        Task SendMessageAsync(string message);
    }
}
