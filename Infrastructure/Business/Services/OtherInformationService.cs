using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class OtherInformationService : IOtherInformationService
    {
        private readonly IMapper _mapper;
        private readonly IOtherInformationRepository _otherInformationRepository;

        public OtherInformationService(IOtherInformationRepository otherInformationRepository,
            IMapper mapper)
        {
            _otherInformationRepository = otherInformationRepository;
            _mapper = mapper;
        }

        public async Task<OtherInformationViewModel> UpdateOtherInformation
            (string userId, OtherInformationDto otherInformationDto)
        {
            var otherInformation = _mapper.Map<OtherInformation>(otherInformationDto);
            
            var retrievedOtherInformation = await _otherInformationRepository.UpdateOtherInformation
                (userId, otherInformation);
            
            var retrievedOtherInformationDto = _mapper.Map<OtherInformationDto>(retrievedOtherInformation);

            return new OtherInformationViewModel()
            {
                OtherInformationDto = retrievedOtherInformationDto
            };
        }
    }
}