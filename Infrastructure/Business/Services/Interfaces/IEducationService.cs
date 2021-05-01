using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IEducationService
    {
        Task<CreateUpdateEducationViewModel> CreateEducation(string userId, EducationDto educationDto);
        Task<EducationViewModel> GetEducation(string educationId);
        Task<EducationsViewModel> GetEducations(string userId);
        Task DeleteEducation(string educationId);
    }
}