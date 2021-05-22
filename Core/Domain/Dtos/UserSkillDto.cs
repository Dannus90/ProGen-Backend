using System;
using Microsoft.AspNetCore.Mvc;

namespace Core.Domain.Dtos
{
    public class UserSkillDto
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        [BindProperty]
        public Guid SkillId { get; set; }
        public int SkillLevel { get; set; } 
    }
}