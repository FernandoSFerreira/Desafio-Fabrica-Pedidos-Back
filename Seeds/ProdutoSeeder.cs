using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;

namespace Desafio_Fabrica_Pedidos_Back.Seeds
{
    public class ProdutoSeeder
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // Verifica se já existem produtos cadastrados para evitar seed duplicado
            var existingProducts = await context.Produtos.ToListAsync();
            if (existingProducts.Any())
            {
                Console.WriteLine("Produtos já foram seedados.");
                return;
            }

            var produtos = new List<Produto>
            {
                new Produto
                {
                    Nome = "Coca-Cola 350ml",
                    Descricao = "Refrigerante de cola em lata de 350ml",
                    Preco = 4.50m
                },
                new Produto
                {
                    Nome = "Pepsi 350ml",
                    Descricao = "Refrigerante de cola em lata de 350ml",
                    Preco = 4.25m
                },
                new Produto
                {
                    Nome = "Guaraná Antarctica 350ml",
                    Descricao = "Refrigerante de guaraná em lata de 350ml",
                    Preco = 4.30m
                },
                new Produto
                {
                    Nome = "Fanta Laranja 350ml",
                    Descricao = "Refrigerante de laranja em lata de 350ml",
                    Preco = 4.20m
                },
                new Produto
                {
                    Nome = "Sprite 350ml",
                    Descricao = "Refrigerante de limão em lata de 350ml",
                    Preco = 4.15m
                },
                new Produto
                {
                    Nome = "Red Bull 250ml",
                    Descricao = "Bebida energética em lata de 250ml",
                    Preco = 12.90m
                },
                new Produto
                {
                    Nome = "Monster Energy 473ml",
                    Descricao = "Bebida energética em lata de 473ml",
                    Preco = 15.50m
                },
                new Produto
                {
                    Nome = "Água Mineral Crystal 500ml",
                    Descricao = "Água mineral sem gás em garrafa de 500ml",
                    Preco = 2.50m
                },
                new Produto
                {
                    Nome = "Suco Del Valle Uva 1L",
                    Descricao = "Suco de uva em caixa tetra pak de 1 litro",
                    Preco = 6.80m
                },
                new Produto
                {
                    Nome = "Cerveja Brahma 350ml",
                    Descricao = "Cerveja pilsen em lata de 350ml",
                    Preco = 3.90m
                },
                new Produto
                {
                    Nome = "Cerveja Skol 350ml",
                    Descricao = "Cerveja pilsen em lata de 350ml",
                    Preco = 3.85m
                },
                new Produto
                {
                    Nome = "Água Tônica Schweppes 350ml",
                    Descricao = "Água tônica em lata de 350ml",
                    Preco = 5.20m
                }
            };

            // Adiciona os produtos ao contexto
            await context.Produtos.AddRangeAsync(produtos);
            await context.SaveChangesAsync();

            Console.WriteLine($"{produtos.Count} produtos seedados com sucesso!");
        }
    }
}