using System.Threading.Tasks;

namespace API.helpers.SendGrid.Interfaces
{
    public interface IEmailHandler
    {
        Task SendResetPasswordEmail(string email);
    }
}