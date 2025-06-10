using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;

namespace Desafio_Fabrica_Pedidos_Back.Seeds
{
    public class UsuarioSeeder
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // Verifica se já existem usuários cadastrados para evitar seed duplicado
            var existingUsers = await context.Usuarios.ToListAsync();

            if (existingUsers.Any())
            {
                Console.WriteLine("Usuários já foram seedados.");
                return;
            }

            // Cria um novo usuário com senha criptografada
            var newUser = new Usuario
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123", "$2a$12$tZ0PbBRMMcEohEnF0NBVge", false, hashType: HashType.SHA512)
            };

            // Adiciona o usuário ao contexto e salva as mudanças
            await context.Usuarios.AddAsync(newUser);
            await context.SaveChangesAsync();

            Console.WriteLine("Usuário administrador seedado com sucesso!");
        }
    }
}
