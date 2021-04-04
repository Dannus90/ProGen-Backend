namespace Core.Domain.Models
{
    public class UserCredentialsWithName : UserCredentials
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}