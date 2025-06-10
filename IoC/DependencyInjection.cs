using Desafio_Fabrica_Pedidos_Back.Application.Services;
using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Configurations;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Data;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Messaging;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Persistence;
using Desafio_Fabrica_Pedidos_Back.Security;
using Desafio_Fabrica_Pedidos_Back.Security.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

namespace Desafio_Fabrica_Pedidos_Back.IoC
{
    public class DependencyInjection
    {
        public static void Register(IServiceCollection svcCollection, IConfiguration configuration)
        {
            // Misc
            svcCollection.AddControllers();
            svcCollection.AddEndpointsApiExplorer();
            svcCollection.AddSwaggerGen();


            // Configuração do Serilog
            svcCollection.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));


            // Configuração do JSON Serializer
            svcCollection.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


            // Configuração do JwtOptions
            svcCollection.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            // Adicionar autenticação JWT Bearer
            svcCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtOptions = svcCollection.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;
                    var secretKey = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                    };
                });

            // Configuração do Swagger
            svcCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Desafio Fabrica Pedidos API",
                    Version = "v1",
                    Description = "API para gerenciamento de pedidos",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Fernando de Souza Ferreira",
                        Email = "ferproguitar@gmail.com",
                        Url = new Uri("https://ferreirasystems.com")
                    }
                });

                // Adicionando a definição de autenticação JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Insira o token JWT no campo abaixo. Exemplo: Bearer {seu_token}"
                });

                // Exigir autenticação JWT em todas as requisições
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });


            // DbContext - PostgreSQL
            svcCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));


            // Repositories
            svcCollection.AddScoped<IPedidoRepository, PedidoRepository>();
            svcCollection.AddScoped<IRevendaRepository, RevendaRepository>();
            svcCollection.AddScoped<IProdutoRepository, ProdutoRepository>();
            svcCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();


            // Services
            svcCollection.AddScoped<IJwtService, JwtService>();
            svcCollection.AddScoped<IPedidoService, PedidoService>();
            svcCollection.AddScoped<IRevendaService, RevendaService>();
            svcCollection.AddScoped<IProdutoService, ProdutoService>();
            svcCollection.AddScoped<IUsuarioService, UsuarioService>();


            // Messaging - RabbitMQ
            svcCollection.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMQ"));
            svcCollection.AddSingleton<IConnectionFactory>(sp =>
            {
                var rabbitMqConfig = sp.GetRequiredService<IOptions<RabbitMqConfiguration>>().Value;
                return new ConnectionFactory()
                {
                    HostName = rabbitMqConfig.HostName ?? "localhost",
                    UserName = rabbitMqConfig.UserName ?? "guest",
                    Password = rabbitMqConfig.Password ?? "guest",
                    Port = rabbitMqConfig.Port > 0 ? rabbitMqConfig.Port : 5672
                };
            });
            svcCollection.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
            svcCollection.AddScoped<IRabbitMQConsumer, RabbitMQConsumer>();

            // Configurar a função de processamento de mensagens do RabbitMQ
            svcCollection.AddSingleton<Func<string, Task>>(sp => async (message) =>
            {
                using var scope = sp.CreateScope();
                var consumer = scope.ServiceProvider.GetRequiredService<IRabbitMQConsumer>();
                await consumer.ProcessarMensagemAsync(message);
            });

            // Registrar o Background Service do RabbitMQ
            svcCollection.AddHostedService<RabbitMQBackgroundService>();
        }
    }
}
