using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class FullCvInformationViewModel
    {
        public FullCvInformationViewModel()
        {
            FullUserInformationDto = new FullUserInformationDto();
            OtherInformationDto = new OtherInformationDto();
            EducationDtos = new List<EducationDto>();
            WorkExperienceDtos = new List<WorkExperienceDto>();
            LanguageDtos = new List<LanguageDto>();
            UserPresentationDto = new UserPresentationDto();
            UserSkillAndSkillDtos = new List<UserSkillAndSkillDto>();
            CertificateDtos = new List<CertificateDto>();
        }
        public FullUserInformationDto FullUserInformationDto { get; set; }
        public OtherInformationDto OtherInformationDto { get; set; }
        public IEnumerable<EducationDto> EducationDtos { get; set; }
        public IEnumerable<WorkExperienceDto> WorkExperienceDtos { get; set; }
        public IEnumerable<LanguageDto> LanguageDtos { get; set; }
        public UserPresentationDto UserPresentationDto { get; set; }
        public IEnumerable<UserSkillAndSkillDto> UserSkillAndSkillDtos { get; set; }
        
        public IEnumerable<CertificateDto> CertificateDtos { get; set; }
    }
}