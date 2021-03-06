using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IOtherInformationService
    {
        Task<OtherInformationViewModel> UpdateOtherInformation
            (string userId, OtherInformationDto otherInformationDto);

        Task<OtherInformationViewModel> GetOtherInformation
            (string userId);
    }
}