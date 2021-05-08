using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserLanguagesViewModel
    {
        public IEnumerable<LanguageDto> LanguageDtos { get; set; }
    }
}