using System.Threading.Tasks;
using API.helpers;
using AutoMapper;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.Models;
using Core.Domain.ViewModels;
using Infrastructure.Identity.Repositories.Interfaces;
using Infrastructure.Identity.Services.Interfaces;
using Infrastructure.Persistence.Repositories.Interfaces;
using Infrastructure.Security;
using Infrastructure.Security.Tokens;

namespace Infrastructure.Identity.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;

        public UserAuthService(IUserAuthRepository userAuthRepository,
            IUserRepository userRepository,
            ITokenHandler tokenHandler,
            IMapper mapper)
        {
            _userAuthRepository = userAuthRepository;
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
            _mapper = mapper;
        }

        public async Task RegisterUser(UserCredentialsWithNameDto userCredentialsWithNameDto)
        {
            var userCredentialsWithName = _mapper.Map<UserCredentialsWithName>(userCredentialsWithNameDto);
            CredentialsValidation.ValidateCredentials(userCredentialsWithName);

            var hashedPassword = PasswordHandler.HashPassword(userCredentialsWithName.Password);
            userCredentialsWithName.Password = hashedPassword;
            
            await _userAuthRepository.RegisterUser(userCredentialsWithName);
        }

        public async Task<TokenResponseViewModel> LoginUser(UserCredentialsDto userCredentialsDto)
        {
            var userCredentials = _mapper.Map<UserCredentials>(userCredentialsDto);
            var user = await _userRepository.GetUserByEmail(userCredentials.Email);

            // If no user we send unauthorized to not give information regarding if email exist or not.
            // Not found would give information that the email is not in use at the moment. 
            if (user == null) throw new HttpExceptionResponse(401, "Incorrect email or password");

            // Password verification.
            if (!PasswordHandler.VerifyPassword(userCredentials.Password,
                user.Password.Trim()))
                throw new HttpExceptionResponse(401, "Incorrect email or password");

            var accessToken = _tokenHandler.GenerateJsonWebToken(user);
            var refreshToken = _tokenHandler.GenerateRefreshToken(user);
            var refreshTokenDb = await _userAuthRepository
                .GetRefreshTokenByUserId(user.Id.ToString());

            // Either updating or saving the token depending on if the user has a refresh token saved or not. 
            if (refreshTokenDb != null)
                await _userAuthRepository.UpdateRefreshTokenByUserId(refreshToken, user.Id);
            else
                await _userAuthRepository.SaveRefreshToken(refreshToken, user.Id);

            // Set last logged in. 
            await _userAuthRepository.UpdateLastLoggedIn(user.Id);

            return new TokenResponseViewModel
            {
                TokenResponse = new TokenDataDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }

        public async Task<TokenResponseViewModel> GenerateAccessTokenFromRefreshToken
            (string userId, string refreshToken)
        {
            var refreshTokenDb = await _userAuthRepository.GetRefreshTokenByUserId(userId);
            
            // Check if refreshToken exist in db. 
            if (refreshTokenDb == null)
                throw new HttpExceptionResponse(400, "No refresh token related to user exist.");

            // Check so that the provided refresh token and db refresh token are equal.
            if (refreshTokenDb.Token != refreshToken)
                throw new HttpExceptionResponse(400, "The provided refresh token is not valid.");

            var user = await _userRepository.GetUserByUserId(userId);
            
            // Check so that the user actually exist in database and should get a token back. 
            if (user == null)
                throw new HttpExceptionResponse(404, "No user related to the user id exist.");
            
            // Generating a new access token.
            var accessToken = _tokenHandler.GenerateJsonWebToken(user);

            // We only update life time and generate new refresh token upon login for security reasons. 
            return new TokenResponseViewModel
            {
                TokenResponse = new TokenDataDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }

        public async Task DeleteRefreshToken(string userId)
        {
            await _userAuthRepository.DeleteRefreshTokenByUserId(userId);
        }
    }
}