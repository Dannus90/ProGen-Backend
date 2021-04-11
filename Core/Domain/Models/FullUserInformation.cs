using Core.Domain.DbModels;

namespace Core.Domain.Dtos
{
    public class FullUserInformation
    {
        public UserData UserData { get; set; }
        public User User { get; set; }
    }
}