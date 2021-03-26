using System.Threading.Tasks;
using API.helpers;
using AutoMapper;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services.Interfaces;
using Infrastructure.Security;

namespace Infrastructure.Identity.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IMapper _mapper;
        
        public UserAuthService(IUserAuthRepository userAuthRepository, IMapper mapper)
        {
            _userAuthRepository = userAuthRepository;
            _mapper = mapper;
        }

        public async Task RegisterUser(UserCredentialsDto userCredentialsDto)
        {
            var userCredentials = _mapper.Map<UserCredentials>(userCredentialsDto);
            CredentialsValidation.ValidateCredentials(userCredentials.Password,
                userCredentials.Email);
            
            var hashedPassword = PasswordHandler.HashPassword(userCredentials.Password);
            await _userAuthRepository.RegisterUser(hashedPassword, userCredentials.Email);
        }
    }
}