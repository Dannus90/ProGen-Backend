using System.Threading.Tasks;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services.Interfaces;

namespace Infrastructure.Identity.Services
{
    public class UserAuthService : IUserAuthService
    {
        private IUserAuthRepository _userAuthRepository;
        
        public UserAuthService(IUserAuthRepository userAuthRepository)
        {
            _userAuthRepository = userAuthRepository;
        }

        public async Task RegisterUser()
        {
            await _userAuthRepository.RegisterUser();
        }
    }
}