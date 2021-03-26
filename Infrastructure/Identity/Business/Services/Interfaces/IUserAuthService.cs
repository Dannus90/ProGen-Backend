using System.Threading.Tasks;
using Core.Domain.Dtos;

namespace Infrastructure.Identity.Services.Interfaces
{
    public interface IUserAuthService
    {
        Task RegisterUser(UserCredentialDto userCredentials);
    }
}