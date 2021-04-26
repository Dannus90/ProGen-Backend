using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IWorkExperienceRepository
    {
        Task<Guid> CreateWorkExperience(WorkExperience workExperienceDto, string userId);
        Task<IEnumerable<WorkExperience>> GetWorkExperiences(string userId);
        Task<WorkExperience> GetWorkExperience(string workExperienceId);
        Task DeleteWorkExperience(string workExperienceId);
        Task<WorkExperience> UpdateWorkExperience
            (string workExperienceId, WorkExperience workExperience);
    }
}