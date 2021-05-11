using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserLanguageViewModel
    {
        public UserLanguageViewModel()
        {
            LanguageDto = new LanguageDto();
        }
        public LanguageDto LanguageDto { get; set; }
    }
}