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
    }
}