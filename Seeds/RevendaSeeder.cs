using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;

namespace Desafio_Fabrica_Pedidos_Back.Seeds
{
    public class RevendaSeeder
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // Verifica se já existem revendas cadastradas para evitar seed duplicado
            var existingRevendas = await context.Revendas.ToListAsync();
            if (existingRevendas.Any())
            {
                Console.WriteLine("Revendas já foram seedadas.");
                return;
            }

            var revendas = new List<Revenda>
            {
                new Revenda
                {
                    Cnpj = "12.345.678/0001-90",
                    RazaoSocial = "Distribuidora Central Ltda",
                    NomeFantasia = "Central Bebidas",
                    Email = "contato@centralbebidas.com.br",
                    Telefones = new List<string> { "(11) 3456-7890", "(11) 99876-5432" },
                    NomeContatoPrincipal = "João Silva"
                },
                new Revenda
                {
                    Cnpj = "98.765.432/0001-10",
                    RazaoSocial = "Comercial Norte SA",
                    NomeFantasia = "Norte Distribuição",
                    Email = "vendas@nortedistribuicao.com.br",
                    Telefones = new List<string> { "(21) 2345-6789", "(21) 98765-4321" },
                    NomeContatoPrincipal = "Maria Santos"
                },
                new Revenda
                {
                    Cnpj = "55.666.777/0001-88",
                    RazaoSocial = "Sul Bebidas e Distribuidora ME",
                    NomeFantasia = "Sul Bebidas",
                    Email = "pedidos@sulbebidas.com.br",
                    Telefones = new List<string> { "(47) 3234-5678", "(47) 99123-4567", "(47) 3234-5679" },
                    NomeContatoPrincipal = "Carlos Oliveira"
                },
                new Revenda
                {
                    Cnpj = "11.222.333/0001-44",
                    RazaoSocial = "Oeste Comercial de Bebidas Ltda",
                    NomeFantasia = "Oeste Express",
                    Email = "comercial@oesteexpress.com.br",
                    Telefones = new List<string> { "(65) 3987-6543" },
                    NomeContatoPrincipal = "Ana Paula Costa"
                },
                new Revenda
                {
                    Cnpj = "33.444.555/0001-66",
                    RazaoSocial = "Leste Distribuição e Logística SA",
                    NomeFantasia = "Leste Distribuidora",
                    Email = "logistica@lestedistribuidora.com.br",
                    Telefones = new List<string> { "(85) 2876-5432", "(85) 99234-5678" },
                    NomeContatoPrincipal = "Roberto Ferreira"
                }
            };

            // Adiciona as revendas ao contexto
            await context.Revendas.AddRangeAsync(revendas);
            await context.SaveChangesAsync();

            Console.WriteLine($"{revendas.Count} revendas seedadas com sucesso!");
        }
    }
}