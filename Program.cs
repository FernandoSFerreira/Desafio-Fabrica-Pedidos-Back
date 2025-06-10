using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using Desafio_Fabrica_Pedidos_Back.IoC;

var builder = WebApplication.CreateBuilder(args);

// Configurando o Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    //.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
// Registrar o Serilog como o provedor de logging da aplicação
builder.Host.UseSerilog();


//Adiciona os serviços da aplicação
DependencyInjection.Register(builder.Services, builder.Configuration);


var app = builder.Build();

app.UseSerilogRequestLogging(); // Adiciona o logging de requisições com Serilog

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseHttpsRedirection();
app.UseCors(a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Aplicar migrações automaticamente (opcional, para desenvolvimento)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Verificando migrações do banco de dados...");
        context.Database.Migrate();

        // Chama o SeedAsync para popular o banco de dados
        logger.LogInformation("Executando SeedAsync...");
        await context.SeedAsync();

        logger.LogInformation("Seed concluído com sucesso!");

    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao verificar migrações do banco de dados");
    }
}


// Middleware global para tratamento de erros
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Ocorreu uma exceção não tratada durante o processamento da solicitação.");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "Ocorreu um erro inesperado. Tente novamente mais tarde." });
    }
});

app.Run();
