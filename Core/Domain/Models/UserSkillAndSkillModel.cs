using Core.Domain.DbModels;

namespace Core.Domain.Models
{
    public class UserSkillAndSkillModel
    {
        public UserSkill UserSkill { get; set; }
        public Skill Skill { get; set; }
    }
}