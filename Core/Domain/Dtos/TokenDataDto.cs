namespace Core.Domain.Dtos
{
    public class TokenDataDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}