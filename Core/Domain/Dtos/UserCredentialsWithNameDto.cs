namespace Core.Domain.Dtos
{
    public class UserCredentialsWithNameDto : UserCredentialsDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}