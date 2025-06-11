using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;

namespace Desafio_Fabrica_Pedidos_Back.Seeds
{
    public class ItemPedidoSeeder
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // Verifica se já existem itens de pedidos cadastrados para evitar seed duplicado
            var existingItens = await context.ItensPedidos.ToListAsync();
            if (existingItens.Any())
            {
                Console.WriteLine("Itens de pedidos já foram seedados.");
                return;
            }

            // Busca pedidos e produtos existentes para associar aos itens
            var pedidos = await context.Pedidos.ToListAsync();
            var produtos = await context.Produtos.ToListAsync();

            if (!pedidos.Any())
            {
                Console.WriteLine("Nenhum pedido encontrado. Execute o PedidoSeeder primeiro.");
                return;
            }

            if (!produtos.Any())
            {
                Console.WriteLine("Nenhum produto encontrado. Execute o ProdutoSeeder primeiro.");
                return;
            }

            var itensPedidos = new List<ItemPedido>();
            var random = new Random();

            // Gera itens para cada pedido
            foreach (var pedido in pedidos)
            {
                // Cada pedido terá entre 2 a 5 itens diferentes
                var quantidadeItens = random.Next(2, 6);
                var produtosSelecionados = produtos.OrderBy(x => Guid.NewGuid()).Take(quantidadeItens).ToList();

                foreach (var produto in produtosSelecionados)
                {
                    var quantidade = random.Next(5, 51); // Entre 5 e 50 unidades

                    itensPedidos.Add(new ItemPedido
                    {
                        PedidoId = pedido.Id,
                        ProdutoId = produto.Id,
                        Quantidade = quantidade,
                        PrecoUnitario = produto.Preco
                    });
                }
            }

            // Adiciona alguns itens específicos para demonstrar variedade
            if (pedidos.Count > 0 && produtos.Count >= 3)
            {
                // Pedido com grandes quantidades
                itensPedidos.Add(new ItemPedido
                {
                    PedidoId = pedidos[0].Id,
                    ProdutoId = produtos[0].Id, // Coca-Cola
                    Quantidade = 100,
                    PrecoUnitario = produtos[0].Preco
                });

                itensPedidos.Add(new ItemPedido
                {
                    PedidoId = pedidos[0].Id,
                    ProdutoId = produtos[1].Id, // Pepsi
                    Quantidade = 75,
                    PrecoUnitario = produtos[1].Preco
                });
            }

            // Adiciona os itens ao contexto
            await context.ItensPedidos.AddRangeAsync(itensPedidos);
            await context.SaveChangesAsync();

            Console.WriteLine($"{itensPedidos.Count} itens de pedidos seedados com sucesso!");
        }
    }
}