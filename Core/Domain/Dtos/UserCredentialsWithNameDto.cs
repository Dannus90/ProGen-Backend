namespace Core.Domain.Dtos
{
    public class UserCredentialsWithNameDto : UserCredentialsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}