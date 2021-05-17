using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IUserDataService
    {
        Task<UserInformationViewModel> GetFullUserData(string userId);
        Task<UserDataViewModel> UpdateUserData(string userId, UserDataDto userDataDto);
        Task<UserImageViewModel> UploadProfileImage(IFormFile file, string userId);
        Task DeleteProfileImage(string publicId, string userId);
        Task DeleteUserAccount(string userId, DeleteAccountDto deleteAccountDto);
    }
}