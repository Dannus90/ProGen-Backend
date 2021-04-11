using Core.Domain.DbModels;

namespace Core.Domain.Dtos
{
    public class FullUserInformationDto
    {
        public User User { get; set; }
        public UserData UserData { get; set; }
    }
}