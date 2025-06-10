namespace Desafio_Fabrica_Pedidos_Back.Domain.Interfaces
{
    public interface IRabbitMQConsumer
    {
        Task ProcessarMensagemAsync(string message);
    }
}
