using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Identity.Services.Interfaces
{
    public interface IUserPresentationService
    {
        Task<UserPresentationViewModel> UpdateUserPresentation
            (string userId, UserPresentationDto userPresentationDto);
        Task<UserPresentationViewModel> GetUserPresentation(string userId);
    }
}