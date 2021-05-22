using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Exceptions;
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

        public async Task<CertificatesViewModel> GetAllCertificatesForUser(string userId)
        {
            var certificates = await _certificateRepository.GetAllCertificatesForUser(userId);
            
            var certificatesDtos = _mapper.Map<List<CertificateDto>>(certificates);

            return new CertificatesViewModel()
            {
                CertificatesDto = certificatesDtos
            };
        }
        
        public async Task<CertificateViewModel> GetCertificateForUser(string certificateId, string userId)
        {
            var certificate = await _certificateRepository.GetCertificateForUser(certificateId, userId);
            
            if (certificate == null) 
                throw new HttpExceptionResponse(404, "No certificate with the provided id was found");
            
            var certificateDto = _mapper.Map<CertificateDto>(certificate);

            return new CertificateViewModel()
            {
                CertificateDto = certificateDto
            };
        }
        
        public async Task DeleteSingleCertificateForUser(string certificateId, string userId)
        {
            await _certificateRepository.DeleteSingleCertificateForUser(certificateId, userId);
        }
    }
}