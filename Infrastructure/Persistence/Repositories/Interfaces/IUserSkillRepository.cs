using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.Models;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserSkillRepository
    {
        Task<Guid> CreateUserSkill(Guid skillId, int level, string userId);
        Task<IEnumerable<UserSkillAndSkillModel>> GetAllUserSkills(string userId);

        Task DeleteUserSkill(string userId, string userSkillId);
        Task<Guid> UpdateUserSkill(string userSkillId, int skillLevel);
    }
}