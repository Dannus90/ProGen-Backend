using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<CreateUpdateCertificateViewModel> CreateCertificate
            (string userId, CertificateDto certificateDto);
    }
}