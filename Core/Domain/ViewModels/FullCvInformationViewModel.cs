using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class FullCvInformationViewModel
    {
        public UserDataDto UserDataDto { get; set; }
        public OtherInformationDto OtherInformationDto { get; set; }
        public WorkExperienceDto WorkExperienceDto { get; set; }
        public UserPresentationDto UserPresentationDto { get; set; }
    }
}