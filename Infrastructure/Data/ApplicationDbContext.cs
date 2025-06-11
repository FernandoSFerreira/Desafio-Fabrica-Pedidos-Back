﻿using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Desafio_Fabrica_Pedidos_Back.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Fabrica_Pedidos_Back.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // Nenhuma configuração adicional necessária aqui
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Revenda> Revendas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ItemPedido> ItensPedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações de relacionamento
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Pedido>()
                .Property(p => p.DataPedido)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.DataConfirmacao)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Produto)
                .WithMany(p => p.Itens) 
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PedidoTriagemMessage>()
                .Property(p => p.DataPedido)
                .HasColumnType("timestamp without time zone");
        }

        public async Task SeedAsync()
        {
            await DatabaseSeeder.SeedAll(this); // Chama o seeder para popular o banco de dados
        }
    }
}
