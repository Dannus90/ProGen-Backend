using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Identity.Services.Interfaces
{
    public interface IUserAuthService
    {
        Task RegisterUser(UserCredentialsDto userCredentials);
        Task<TokenResponseViewModel> LoginUser(UserCredentialsDto userCredentialsDto);
    }
}