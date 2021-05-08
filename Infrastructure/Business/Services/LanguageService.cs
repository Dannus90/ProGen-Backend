using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IMapper _mapper;
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository,
            IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }
        
        public async Task<LanguageIdViewModel> CreateUserLanguage
            (string userId, LanguageDto languageDto)
        {
            var language = _mapper.Map<Language>(languageDto);
            
            var languageId = await _languageRepository.CreateUserLanguage
                (userId, language);

            return new LanguageIdViewModel()
            {
                LanguageId = languageId
            };
        }
        
        public async Task<UserLanguageViewModel> GetUserLanguage
            (string languageId)
        {
            var language = await _languageRepository.GetUserLanguage
                (languageId);
            
            var languageDto = _mapper.Map<LanguageDto>(language);

            return new UserLanguageViewModel()
            {
                LanguageDto = languageDto
            };
        }
        
        public async Task<LanguageIdViewModel> DeleteUserLanguage
            (string languageId)
        {
            if (languageId == null) 
                throw new HttpExceptionResponse(404, "No languageId was provided");
            
            var retrievedLanguageId = await _languageRepository.DeleteUserLanguage
                (languageId);

            return new LanguageIdViewModel()
            {
                LanguageId = Guid.Parse(retrievedLanguageId)
            };
        }
    }
}