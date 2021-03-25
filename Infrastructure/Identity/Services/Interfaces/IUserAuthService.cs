using System.Threading.Tasks;

namespace Infrastructure.Identity.Services.Interfaces
{
    public interface IUserAuthService
    {
        Task RegisterUser();
    }
}