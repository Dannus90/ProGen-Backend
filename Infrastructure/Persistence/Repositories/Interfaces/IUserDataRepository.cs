using System.Threading.Tasks;
using Core.Domain.Models;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserDataRepository
    {
        Task<FullUserInformation> GetFullUserInformation(string userId);
    }
}