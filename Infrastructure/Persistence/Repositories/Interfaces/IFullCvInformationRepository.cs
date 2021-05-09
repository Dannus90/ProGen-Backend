using System.Threading.Tasks;
using Core.Domain.ViewModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IFullCvInformationRepository
    {
        Task<FullCvInformationViewModel> GetFullCvInformation(string userId);
    }
}