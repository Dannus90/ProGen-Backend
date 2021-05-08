using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<LanguageIdViewModel> CreateUserLanguage
            (string userId, LanguageDto languageDto);

        Task<UserLanguageViewModel> GetUserLanguage
            (string languageId);

        Task<LanguageIdViewModel> DeleteUserLanguage
            (string languageId);

        Task<UserLanguagesViewModel> GetUserLanguages
            (string userId);

        Task<LanguageIdViewModel> UpdateUserLanguage
            (string languageId, LanguageDto languageDto);
    }
}