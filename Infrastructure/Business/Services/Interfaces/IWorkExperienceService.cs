using System;
using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IWorkExperienceService
    {
        Task<CreateWorkExperienceViewModel> CreateWorkExperience (string userId, WorkExperienceDto workExperienceDto);
        Task<WorkExperiencesViewModel> GetWorkExperiences(string userId);
    }
}