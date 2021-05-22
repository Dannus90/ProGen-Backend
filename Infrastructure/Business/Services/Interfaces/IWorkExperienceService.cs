using System;
using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IWorkExperienceService
    {
        Task<CreateUpdateWorkExperienceViewModel> CreateWorkExperience (string userId, WorkExperienceDto workExperienceDto);
        Task<WorkExperiencesViewModel> GetWorkExperiences(string userId);
        Task<WorkExperienceViewModel> GetWorkExperience(string workExperienceId, string userId);
        Task DeleteWorkExperience(string workExperienceId, string userId);

        Task<WorkExperienceViewModel> UpdateWorkExperience
            (string workExperienceId, WorkExperienceDto workExperienceDto, string userId);
    }
}