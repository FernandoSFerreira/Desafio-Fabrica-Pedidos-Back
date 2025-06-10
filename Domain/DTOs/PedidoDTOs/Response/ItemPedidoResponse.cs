namespace Desafio_Fabrica_Pedidos_Back.Domain.DTOs.PedidoDTOs.Response
{
    public class ItemPedidoResponse
    {
        public Guid ProdutoId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
