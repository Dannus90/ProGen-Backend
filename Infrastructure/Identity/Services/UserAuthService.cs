using System.Threading.Tasks;
using Core.Domain.Dtos;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services.Interfaces;

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
            await _userAuthRepository.RegisterUser();
        }
    }
}