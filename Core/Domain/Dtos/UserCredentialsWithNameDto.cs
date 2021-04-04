namespace Core.Domain.Dtos
{
    public class UserCredentialsWithNameDto : UserCredentialsDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}