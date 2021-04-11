namespace Core.Domain.Models
{
    public class UserCredentialsWithName : UserCredentials
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}