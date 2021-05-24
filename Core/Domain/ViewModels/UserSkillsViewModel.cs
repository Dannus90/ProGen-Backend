using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class UserSkillsViewModel
    {
        public UserSkillsViewModel()
        {
            UserSkillAndSkillDtos = new List<UserSkillAndSkillDto>();
        }
        public IEnumerable<UserSkillAndSkillDto> UserSkillAndSkillDtos { get; set; }
    }
}