using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;

namespace Desafio_Fabrica_Pedidos_Back.Seeds
{
    public class PedidoSeeder
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // Verifica se já existem pedidos cadastrados para evitar seed duplicado
            var existingPedidos = await context.Pedidos.ToListAsync();
            if (existingPedidos.Any())
            {
                Console.WriteLine("Pedidos já foram seedados.");
                return;
            }

            // Busca as revendas existentes para associar aos pedidos
            var revendas = await context.Revendas.ToListAsync();
            if (!revendas.Any())
            {
                Console.WriteLine("Nenhuma revenda encontrada. Execute o RevendaSeeder primeiro.");
                return;
            }

            var pedidos = new List<Pedido>
            {
                new Pedido
                {
                    RevendaId = revendas[0].Id,
                    DataPedido = DateTime.Now.AddDays(-15),
                    DataConfirmacao = DateTime.Now.AddDays(-14),
                    Status = "Confirmado",
                    Observacoes = "Pedido prioritário - entrega urgente"
                },
                new Pedido
                {
                    RevendaId = revendas[1].Id,
                    DataPedido = DateTime.Now.AddDays(-10),
                    DataConfirmacao = DateTime.Now.AddDays(-9),
                    Status = "Confirmado",
                    Observacoes = "Cliente regular - manter qualidade do atendimento"
                },
                new Pedido
                {
                    RevendaId = revendas[2].Id,
                    DataPedido = DateTime.Now.AddDays(-7),
                    DataConfirmacao = null,
                    Status = "Pendente",
                    Observacoes = "Aguardando confirmação de estoque"
                },
                new Pedido
                {
                    RevendaId = revendas[0].Id,
                    DataPedido = DateTime.Now.AddDays(-5),
                    DataConfirmacao = DateTime.Now.AddDays(-4),
                    Status = "Em Processamento",
                    Observacoes = "Separação em andamento no depósito"
                },
                new Pedido
                {
                    RevendaId = revendas[3].Id,
                    DataPedido = DateTime.Now.AddDays(-3),
                    DataConfirmacao = DateTime.Now.AddDays(-2),
                    Status = "Enviado",
                    Observacoes = "Transportadora: LogExpress - Código: LX123456"
                },
                new Pedido
                {
                    RevendaId = revendas[4].Id,
                    DataPedido = DateTime.Now.AddDays(-1),
                    DataConfirmacao = null,
                    Status = "Pendente",
                    Observacoes = "Primeiro pedido do cliente - verificar limite de crédito"
                }
            };

            // Adiciona os pedidos ao contexto
            await context.Pedidos.AddRangeAsync(pedidos);
            await context.SaveChangesAsync();

            Console.WriteLine($"{pedidos.Count} pedidos seedados com sucesso!");
        }
    }
}