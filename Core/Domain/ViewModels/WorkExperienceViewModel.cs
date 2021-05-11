using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class WorkExperienceViewModel
    {
        public WorkExperienceViewModel()
        {
            WorkExperienceDto = new WorkExperienceDto();
        }
        public WorkExperienceDto WorkExperienceDto { get; init; }
    }
}