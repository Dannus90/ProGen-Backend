using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserInformationViewModel
    {
        public UserInformationViewModel()
        {
            FullUserInformationDto = new FullUserInformationDto();
        }
        public FullUserInformationDto FullUserInformationDto { get; set; }
    }
}