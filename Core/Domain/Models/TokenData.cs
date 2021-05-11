namespace Core.Domain.ViewModels
{
    public class TokenData
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}