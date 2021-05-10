using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class FullCvInformationViewModel
    {
        public FullUserInformationDto FullUserInformationDto { get; set; }
        public OtherInformationDto OtherInformationDto { get; set; }
        public IEnumerable<EducationDto> EducationDtos { get; set; }
        public IEnumerable<WorkExperienceDto> WorkExperienceDtos { get; set; }
        public IEnumerable<LanguageDto> LanguageDtos { get; set; }
        public UserPresentationDto UserPresentationDto { get; set; }
    }
}