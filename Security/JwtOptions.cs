namespace Desafio_Fabrica_Pedidos_Back.Security
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationTimeMinutes { get; set; }

    }
}
