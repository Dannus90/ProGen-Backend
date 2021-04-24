using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Identity.Repositories.Interfaces
{
    public interface IUserPresentationRepository
    {
        Task<UserPresentation> CreateUserPresentation(string userId, UserPresentation userPresentation);
        Task<UserPresentation> GetUserPresentation(string userId);
    }
}