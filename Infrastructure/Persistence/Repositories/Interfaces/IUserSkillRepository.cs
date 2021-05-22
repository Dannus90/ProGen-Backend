using System;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserSkillRepository
    {
        Task<Guid> CreateUserSkill(Guid skillId, int level, string userId);
    }
}