using System.Threading.Tasks;

namespace Infrastructure.Identity.Repositories.Interfaces
{
    public interface IUserAuthRepository
    {
        Task RegisterUser();
    }
}