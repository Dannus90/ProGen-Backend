using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Business.Services.Interfaces
{
    public interface ISkillService
    {
        Task<CreateSkillViewModel> CreateSkill
            (SkillDto skillDto);

        Task<SkillsViewModel> GetSkillsBySearchQuery
            (string searchQuery);

        Task DeleteSkillById(string skillId);
    }
}