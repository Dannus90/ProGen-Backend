using System.Threading.Tasks;
using API.helpers;
using Core.Domain.Dtos;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services.Interfaces;
using Infrastructure.Security;

namespace Infrastructure.Identity.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        
        public UserAuthService(IUserAuthRepository userAuthRepository)
        {
            _userAuthRepository = userAuthRepository;
        }

        public async Task RegisterUser(UserCredentialDto userCredentials)
        {
            CredentialsValidation.ValidateCredentials(userCredentials.Email,
                userCredentials.Password);
            
            var hashedPassword = PasswordHandler.HashPassword(userCredentials.Password);
            await _userAuthRepository.RegisterUser();
        }
    }
}