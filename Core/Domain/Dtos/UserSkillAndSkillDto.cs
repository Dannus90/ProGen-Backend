using Core.Domain.Models;

namespace Core.Domain.Dtos
{
    public class UserSkillAndSkillDto
    {
        public UserSkillModel UserSkillModel { get; set; }
        public SkillModel SkillModel { get; set; }
    }
}