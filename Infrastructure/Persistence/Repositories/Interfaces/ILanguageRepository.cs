using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        Task<Guid> CreateUserLanguage(string userId, Language language);
        Task<Language> GetUserLanguage(string languageId, string userId);

        Task<string> DeleteUserLanguage(string languageId, string userId);

        Task<IEnumerable<Language>> GetUserLanguages(string userId);

        Task<string> UpdateUserLanguage(string languageId, Language language, string userId);
    }
}