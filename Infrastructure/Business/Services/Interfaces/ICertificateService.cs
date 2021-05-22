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
        Task<CertificateViewModel> GetCertificateForUser(string certificateId);

        Task DeleteSingleCertificateForUser(string certificateId, string userId);
    }
}