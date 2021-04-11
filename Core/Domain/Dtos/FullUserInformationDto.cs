using Core.Domain.DbModels;

namespace Core.Domain.Dtos
{
    public class FullUserInformationDto
    {
        public UserData UserData { get; set; }
        public User User { get; set; }
    }
}