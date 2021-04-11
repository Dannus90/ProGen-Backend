using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IUserDataService
    {
        Task<UserInformationViewModel> GetFullUserData(string userId);
        Task<UserDataViewModel> UpdateUserData(string userId, UserDataDto userDataDto);
    }
}