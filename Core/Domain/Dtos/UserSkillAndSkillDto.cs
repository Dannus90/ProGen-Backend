using Core.Domain.Models;

namespace Core.Domain.Dtos
{
    public class UserSkillAndSkillDto
    {
        public UserSkillAndSkillDto()
        {
            SkillModel = new SkillModel();
            UserSkillModel = new UserSkillModel();
        }
        public UserSkillModel UserSkillModel { get; set; }
        public SkillModel SkillModel { get; set; }
    }
}