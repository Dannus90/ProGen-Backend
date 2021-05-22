using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IUserSkillService
    {
        Task<CreateUpdateUserSkillViewModel> CreateUserSkill
            (UserSkillDto userSkillDto, string userId);

        Task<UserSkillViewModel> GetAllUserSkills
            (string userId);

        Task DeleteUserSkill
            (string userId, string userSkillId);

        Task<CreateUpdateUserSkillViewModel> UpdateUserSkill
            (string userSkillId, UserSkillDto userSkillDto);
    }
}