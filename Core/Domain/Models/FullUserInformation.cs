using Core.Domain.DbModels;

namespace Core.Domain.Dtos
{
    public class FullUserInformation
    {
        public User User { get; set; }
        public UserData UserData { get; set; }
    }
}