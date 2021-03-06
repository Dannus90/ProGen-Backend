using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<CreateUpdateCertificateViewModel> CreateCertificate
            (string userId, CertificateDto certificateDto);
        Task<CertificatesViewModel> GetAllCertificatesForUser(string userId);
        Task<CertificateViewModel> GetCertificateForUser(string certificateId, string userId);
        Task DeleteSingleCertificateForUser(string certificateId, string userId);
        Task<CertificateViewModel> UpdateCertificateForUser
            (string certificateId, CertificateDto certificateDto, string userId);
    }
}