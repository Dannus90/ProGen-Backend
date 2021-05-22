using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface IUserSkillService
    {
        Task<CreateUpdateUserSkillViewModel> CreateUserSkill
            (UserSkillDto userSkillDto, string userId);

        Task<UserSkillViewModel> GetAllUserSkills
            (string userId);
    }
}