using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IOtherInformationRepository
    {
        Task<OtherInformation> UpdateOtherInformation(string userId, OtherInformation otherInformation);
    }
}