namespace uBeac.Core.Identity
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double Expires { get; set; } // second
        public string Secret { get; set; }
    }
}
