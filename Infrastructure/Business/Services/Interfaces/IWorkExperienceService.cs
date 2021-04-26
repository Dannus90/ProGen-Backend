using System.Threading.Tasks;
using Core.Domain.Dtos;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IWorkExperienceService
    {
        Task CreateWorkExperience(string userId, WorkExperienceDto workExperienceDto);
    }
}