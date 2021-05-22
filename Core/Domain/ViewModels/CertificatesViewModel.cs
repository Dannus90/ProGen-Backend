using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class CertificatesViewModel
    {
        public CertificatesViewModel()
        {
            CertificatesDto = new List<CertificateDto>();
        }
        public IEnumerable<CertificateDto> CertificatesDto { get; set; }
    }
}