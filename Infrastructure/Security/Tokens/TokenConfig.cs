namespace Infrastructure.Security.Tokens
{
    public class TokenConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public long AccessTokenExpiration { get; set; }
        public long RefreshTokenExpiration { get; set; }

        public string SecretKey { get; set; }

        // These will be used in future. 
        public long ResetPasswordTokenExpiration { get; set; }
        public long InviteNewUserTokenExpiration { get; set; }
    }
}