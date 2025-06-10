using Microsoft.IdentityModel.Tokens;

namespace Desafio_Fabrica_Pedidos_Back.Security.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string userName);
        bool TryDecodeJwt(string token, out SecurityToken decodedToken, out Exception? exception);
    }
}
