using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IEducationRepository
    {
        Task<Guid> CreateEducation(Education education, string userId);
        Task<Education> GetEducation(string educationId);
        Task<IEnumerable<Education>> GetEducations(string userId);
        Task DeleteEducation(string educationId);

        Task<Education> UpdateEducation
            (string educationId, Education education);
    }
}