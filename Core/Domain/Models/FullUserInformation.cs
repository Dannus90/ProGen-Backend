using Core.Domain.DbModels;

namespace Core.Domain.Models
{
    public class FullUserInformation
    {
        public FullUserInformation()
        {
            User = new User();
            UserData = new UserData();
        }
        public User User { get; set; }
        public UserData UserData { get; set; }
    }
}