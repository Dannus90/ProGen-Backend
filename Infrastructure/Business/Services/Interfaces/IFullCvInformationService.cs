using System.Threading.Tasks;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IFullCvInformationService
    {
        Task<FullCvInformationViewModel> GetFullCvInformation(string userId);
    }
}