using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class FullCvInformationService : IFullCvInformationService
    {
        private readonly IFullCvInformationRepository _fullCvInformationRepository;

        public FullCvInformationService(IFullCvInformationRepository IFullCvInformationRepository)
        {
            _fullCvInformationRepository = IFullCvInformationRepository;
        }
        
        public async Task<FullCvInformationViewModel> GetFullCvInformation
            (string userId)
        {
            return await _fullCvInformationRepository.GetFullCvInformation(userId);
        }
    }
}