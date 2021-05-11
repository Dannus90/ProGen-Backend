using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserLanguagesViewModel
    {
        public UserLanguagesViewModel()
        {
            LanguageDtos = new List<LanguageDto>();
        }
        public IEnumerable<LanguageDto> LanguageDtos { get; set; }
    }
}