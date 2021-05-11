using Core.Domain.Dtos;

namespace Core.Domain.ViewModels
{
    public class OtherInformationViewModel
    {
        public OtherInformationViewModel()
        {
            OtherInformationDto = new OtherInformationDto();
        }
        public OtherInformationDto OtherInformationDto { get; set; }
    }
}