using System.Threading.Tasks;
using AutoMapper;
using Core.Domain.DbModels;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly IMapper _mapper;
        private readonly ICertificateRepository _certificateRepository;

        public CertificateService(ICertificateRepository certificateRepository,
            IMapper mapper)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
        }

        public async Task<CreateUpdateCertificateViewModel> CreateCertificate
            (string userId, CertificateDto certificateDto)
        {
            var certificate = _mapper.Map<Certificate>(certificateDto);
            
            var certificateId = await _certificateRepository.CreateCertificate
                (certificate, userId);

            return new CreateUpdateCertificateViewModel()
            {
                CertificateId = certificateId
            };
        }
    }
}