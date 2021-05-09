using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class FullCvInformationService : IFullCvInformationService
    {
        private readonly IMapper _mapper;
        private readonly IFullCvInformationRepository _fullCvInformationRepository;

        public FullCvInformationService(IFullCvInformationRepository IFullCvInformationRepository,
            IMapper mapper)
        {
            _fullCvInformationRepository = IFullCvInformationRepository;
            _mapper = mapper;
        }
        
        public async Task<FullCvInformationViewModel> GetFullCvInformation
            (string userId)
        {
            return await _fullCvInformationRepository.GetFullCvInformation(userId);
        }
    }
}