using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class CertificateViewModel
    {
        public CertificateViewModel()
        {
            CertificateDto = new CertificateDto();
        }
        public CertificateDto CertificateDto { get; set; }
    }
}