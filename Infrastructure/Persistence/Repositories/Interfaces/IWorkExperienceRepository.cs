using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IWorkExperienceRepository
    {
        Task CreateWorkExperience(WorkExperience workExperienceDto, string userId);
        Task<IEnumerable<WorkExperience>> GetWorkExperiences(string userId);
    }
}