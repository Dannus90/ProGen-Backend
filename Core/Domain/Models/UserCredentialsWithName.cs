namespace Core.Domain.Models
{
    public class UserCredentialsWithName : UserCredentials
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}