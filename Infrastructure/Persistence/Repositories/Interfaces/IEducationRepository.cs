using System;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IEducationRepository
    {
        Task<Guid> CreateEducation(Education education, string userId);
        Task<Education> GetEducation(string educationId);
    }
}