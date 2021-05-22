using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

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
        
        
        public async Task<LanguageIdViewModel> UpdateUserLanguage
            (string languageId, LanguageDto languageDto, string userId)
        {
            var language = _mapper.Map<Language>(languageDto);
            
            var retrievedLanguageId = await _languageRepository.UpdateUserLanguage
                (languageId, language, userId);

            return new LanguageIdViewModel()
            {
                LanguageId = Guid.Parse(retrievedLanguageId)
            };
        }
        
        public async Task<UserLanguageViewModel> GetUserLanguage
            (string languageId, string userId)
        {
            var language = await _languageRepository.GetUserLanguage
                (languageId, userId);
            
            var languageDto = _mapper.Map<LanguageDto>(language);

            return new UserLanguageViewModel()
            {
                LanguageDto = languageDto
            };
        }
        
        public async Task<UserLanguagesViewModel> GetUserLanguages
            (string userId)
        {
            var languages = await _languageRepository.GetUserLanguages
                (userId);
            
            var languageDtos = _mapper.Map<List<LanguageDto>>(languages);

            return new UserLanguagesViewModel()
            {
                LanguageDtos = languageDtos
            };
        }
        
        public async Task<LanguageIdViewModel> DeleteUserLanguage
            (string languageId, string userId)
        {
            if (languageId == null) 
                throw new HttpExceptionResponse(StatusCodes.Status404NotFound,
                    "No languageId was provided");
            
            var retrievedLanguageId = await _languageRepository.DeleteUserLanguage
                (languageId, userId);

            return new LanguageIdViewModel()
            {
                LanguageId = Guid.Parse(retrievedLanguageId)
            };
        }
    }
}