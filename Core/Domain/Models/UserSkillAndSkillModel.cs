using Core.Domain.DbModels;

namespace Core.Domain.Models
{
    public class UserSkillAndSkillModel
    {
        public UserSkillAndSkillModel()
        {
            Skill = new Skill();
            UserSkill = new UserSkill();
        }
        public UserSkill UserSkill { get; set; }
        public Skill Skill { get; set; }
    }
}