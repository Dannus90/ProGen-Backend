using System;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        Task<Guid> CreateUserLanguage(string userId, Language language);
        Task<Language> GetUserLanguage(string languageId);

        Task<string> DeleteUserLanguage(string languageId);
    }
}