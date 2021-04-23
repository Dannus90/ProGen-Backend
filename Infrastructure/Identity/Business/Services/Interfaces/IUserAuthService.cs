using System.Threading.Tasks;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;

namespace Infrastructure.Identity.Services.Interfaces
{
    public interface IUserAuthService
    {
        Task RegisterUser(UserCredentialsWithNameDto userCredentialsWithNameDto);
        Task<TokenResponseViewModel> LoginUser(UserCredentialsDto userCredentials);
        Task<TokenResponseViewModel> GenerateAccessTokenFromRefreshToken(string userId, string refreshToken);
        Task DeleteRefreshToken(string userId);
        Task ChangePassword(ChangePasswordDto changePasswordDto, string userId);
        Task ChangeEmail(ChangeEmailDto changeEmailDto, string userId);
    }
}