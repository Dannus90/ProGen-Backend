using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserSkillViewModel
    {
        public IEnumerable<UserSkillAndSkillDto> UserSkillAndSkillDtos { get; set; }
    }
}