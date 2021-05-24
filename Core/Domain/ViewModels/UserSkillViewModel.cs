using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserSkillViewModel
    {
        public UserSkillViewModel()
        {
            UserSkillAndSkillDto = new UserSkillAndSkillDto();
        }
        public UserSkillAndSkillDto UserSkillAndSkillDto { get; set; }
    }
}