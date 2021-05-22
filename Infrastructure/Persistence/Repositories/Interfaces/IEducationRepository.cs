using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IEducationRepository
    {
        Task<Guid> CreateEducation(Education education, string userId);
        Task<Education> GetEducation(string educationId, string userId);
        Task<IEnumerable<Education>> GetEducations(string userId);
        Task DeleteEducation(string educationId, string userId);

        Task<Education> UpdateEducation
            (string educationId, Education education, string userId);
    }
}