namespace Core.Domain.Models
{
    public class ChangeEmailModel
    {
        public string NewEmail { get; init; }  = null!;
        public string Password { get; init; }  = null!;
    }
}