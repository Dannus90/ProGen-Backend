using System;
using System.Threading.Tasks;
using API.helpers;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security;

namespace Infrastructure.Identity.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public UserAuthService(IUserAuthRepository userAuthRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userAuthRepository = userAuthRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task RegisterUser(UserCredentialsDto userCredentialsDto)
        {
            var userCredentials = _mapper.Map<UserCredentials>(userCredentialsDto);
            CredentialsValidation.ValidateCredentials(userCredentials.Password,
                userCredentials.Email);
            
            var hashedPassword = PasswordHandler.HashPassword(userCredentials.Password);
            await _userAuthRepository.RegisterUser(hashedPassword.Trim(), 
                userCredentials.Email
                    .Trim()
                    .ToLower());
        }
        
        public async Task LoginUser(UserCredentialsDto userCredentialsDto)
        {
            var userCredentials = _mapper.Map<UserCredentials>(userCredentialsDto);
            var user = await _userRepository.GetUserByEmail(userCredentials.Email);

            // If no user we send unauthorized to not give information regarding if email exist or not.
            // Not found would give information that the email is not in use at the moment. 
            if (user == null)
            {
                throw new HttpExceptionResponse(401, "Incorrect email or password");
            }

            // Password verification.
            if (!PasswordHandler.VerifyPassword(userCredentials.Password,
                user.Password.Trim()))
            {
                throw new HttpExceptionResponse(401, "Incorrect email or password");
            }

            await _userRepository.UpdateLastLoggedIn(user.Id);
        }
    }
}