using Desafio_Fabrica_Pedidos_Back.Domain.Interfaces;
using Desafio_Fabrica_Pedidos_Back.Infrastructure.Persistence;
using Desafio_Fabrica_Pedidos_Back.Application.Services;
using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Moq;
using Xunit;

namespace Desafio_Fabrica_Pedidos_Back.Tests.Application.Services
{
    public class RevendaServiceTests
    {
        [Fact]
        public void CadastrarRevenda_ShouldReturnSavedRevenda()
        {
            // Arrange
            var mockRepository = new Mock<IRevendaRepository>();
            mockRepository.Setup(repo => repo.Cadastrar(It.IsAny<Revenda>())).ReturnsAsync(new Revenda { Cnpj = "12345678901234" });

            var revendaService = new RevendaService(mockRepository.Object);

            // Act
            var revenda = revendaService.CadastrarRevenda(new Revenda { Cnpj = "novo_cnpj" }).Result;

            // Assert
            Assert.NotNull(revenda);
            Assert.Equal("12345678901234", revenda.Cnpj);
        }
    }
}
