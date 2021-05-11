using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class WorkExperiencesViewModel
    {
        public WorkExperiencesViewModel()
        {
            WorkExperienceDto = new List<WorkExperienceDto>();
        }
        public IEnumerable<WorkExperienceDto> WorkExperienceDto { get; init; }
    }
}