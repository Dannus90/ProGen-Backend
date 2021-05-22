using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class SkillsViewModel
    {
        public IEnumerable<SkillDto> Skills { get; set; }
    }
}