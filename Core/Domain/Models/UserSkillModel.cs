using System;

namespace Core.Domain.Models
{
    public class UserSkillModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SkillId { get; set; }
        public int SkillLevel { get; set; }
    }
}