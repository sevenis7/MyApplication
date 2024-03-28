namespace MyApplicationServiceLayer.Tokens.TokenGenerators
{
    public class AuthenticationConfiguration
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Secret { get; set; }
        public double AccessTokenExpirationMinutes { get; set; }
        public double RefreshTokenExpirationMinutes { get; set; }
    }
}
