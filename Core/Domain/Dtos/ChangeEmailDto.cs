namespace Core.Domain.Dtos
{
    public class ChangeEmailDto
    {
        public string NewEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}