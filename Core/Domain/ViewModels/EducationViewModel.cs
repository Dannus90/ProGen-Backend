using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class EducationViewModel
    {
        public EducationViewModel()
        {
            EducationDto = new EducationDto();
        }
        public EducationDto EducationDto { get; set; }
    }
}