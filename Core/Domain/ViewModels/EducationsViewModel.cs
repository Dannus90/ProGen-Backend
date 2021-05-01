using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class EducationsViewModel
    {
        public IEnumerable<EducationDto> EducationsDto { get; set; }
    }
}