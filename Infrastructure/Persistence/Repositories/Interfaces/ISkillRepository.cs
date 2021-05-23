using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.DbModels;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ISkillRepository
    {
        Task<Guid> CreateSkill(string skillName);
        Task<IEnumerable<Skill>> GetAllSkills();
        Task DeleteSkillById(string skillId);

        Task<Skill> GetSkillBySkillId(string skillId);
    }
}