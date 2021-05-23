using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class SkillViewModel
    {
        public SkillViewModel()
        {
            SkillDto = new SkillDto();
        }

        public SkillDto SkillDto { get; set; }
    }
}