using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserDataViewModel
    {
        public UserDataViewModel()
        {
            UserDataDto = new UserDataDto();
        }
        public UserDataDto UserDataDto { get; set; }
    }
}