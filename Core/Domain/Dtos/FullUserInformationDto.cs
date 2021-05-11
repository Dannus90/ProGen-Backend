using Core.Domain.DbModels;

namespace Core.Domain.Dtos
{
    public class FullUserInformationDto
    {
        public User User { get; set; } = null!;
        public UserData UserData { get; set; } = null!;
    }
}