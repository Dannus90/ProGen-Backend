using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel()
        {
            TokenResponse = new TokenDataDto();
        }
        
        public TokenDataDto
            TokenResponse { get; set; }
    }
}