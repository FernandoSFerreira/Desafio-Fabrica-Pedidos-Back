using Desafio_Fabrica_Pedidos_Back.Domain.Entities;
using Xunit;

namespace Desafio_Fabrica_Pedidos_Back.Tests.Domain.Entities
{
    public class RevendaTests
    {
        [Fact]
        public void Revenda_ShouldCreateWithCnpj()
        {
            // Arrange
            string cnpj = "12345678901234";

            // Act
            var revenda = new Revenda { Cnpj = cnpj };

            // Assert
            Assert.NotNull(revenda);
            Assert.Equal(cnpj, revenda.Cnpj);
        }
    }
}
