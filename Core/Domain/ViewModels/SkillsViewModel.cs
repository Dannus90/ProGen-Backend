using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class SkillsViewModel
    {
        public SkillsViewModel()
        {
            SkillDtos = new List<SkillDto>();
        }
        
        public IEnumerable<SkillDto> SkillDtos { get; set; }
    }
}