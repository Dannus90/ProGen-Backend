using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IUserSkillService
    {
        Task<CreateUpdateDeleteUserSkillViewModel> CreateUserSkill
            (UserSkillDto userSkillDto, string userId);

        Task<UserSkillsViewModel> GetAllUserSkills
            (string userId);

        Task<CreateUpdateDeleteUserSkillViewModel> DeleteUserSkill
            (string userId, string userSkillId);

        Task<CreateUpdateDeleteUserSkillViewModel> UpdateUserSkill
            (string userSkillId, UserSkillDto userSkillDto);

        Task<UserSkillViewModel> GetSingleUserSkill
            (string userId, string userSkillId);
    }
}