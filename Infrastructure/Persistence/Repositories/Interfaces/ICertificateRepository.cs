using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ICertificateRepository
    {
        Task<Guid> CreateCertificate(Certificate certificate, string userId);
        Task<IEnumerable<Certificate>> GetAllCertificatesForUser(string userId);
        Task<Certificate> GetCertificateForUser(string certificateId, string userId);
        Task DeleteSingleCertificateForUser(string certificateId, string userId);
    }
}