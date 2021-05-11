using Core.Domain.DbModels;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserPresentationViewModel
    {
        public UserPresentationViewModel()
        {
            UserPresentationData = new UserPresentationDto();
        }
        public UserPresentationDto UserPresentationData { get; init; }
    }
}