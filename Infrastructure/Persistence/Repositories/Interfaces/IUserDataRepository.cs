using System.Threading.Tasks;
using Core.Domain.DbModels;
using Core.Domain.Models;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserDataRepository
    {
        Task<FullUserInformation> GetFullUserInformation(string userId);
        Task<UserData> UpdateUserData(string userId, UserDataModel userData);
        Task<ProfileImageModel> UploadProfileImage(string imagePublicId, string imageUrl, string userId);
        Task DeleteProfileImage(string userId);
    }
}