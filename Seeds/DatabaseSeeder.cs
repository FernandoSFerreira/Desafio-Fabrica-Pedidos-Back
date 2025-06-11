using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;

namespace Desafio_Fabrica_Pedidos_Back.Seeds
{
    public class DatabaseSeeder
    {
        public static async Task SeedAll(ApplicationDbContext context)
        {
            Console.WriteLine("Iniciando processo de seed do banco de dados...");

            try
            {
                // Executa os seeders na ordem correta devido às dependências
                await UsuarioSeeder.Seed(context);
                await ProdutoSeeder.Seed(context);
                await RevendaSeeder.Seed(context);
                await PedidoSeeder.Seed(context);
                await ItemPedidoSeeder.Seed(context);

                Console.WriteLine("Processo de seed concluído com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante o processo de seed: {ex.Message}");
                throw;
            }
        }
    }
}