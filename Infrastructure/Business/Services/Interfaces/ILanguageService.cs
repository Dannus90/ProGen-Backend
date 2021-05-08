using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<CreateUpdateLanguageViewModel> CreateUserLanguage
            (string userId, LanguageDto languageDto);

        Task<UserLanguageViewModel> GetUserLanguage
            (string languageId);
    }
}