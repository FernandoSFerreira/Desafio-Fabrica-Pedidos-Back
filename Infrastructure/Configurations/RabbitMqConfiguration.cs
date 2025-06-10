namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Configurations
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "rabbitmq";
        public string Password { get; set; } = "rabbitmq";
        public int Port { get; set; }
        public string QueueTriagem { get; set; } = "revenda-pedidos-triagem";
        public string QueueConfirmados { get; set; } = "revenda-pedidos-confirmados";
    }
}
