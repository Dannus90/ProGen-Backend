using System;
using System.Linq;
using System.Threading.Tasks;
using API.helpers;
using API.helpers.SendGrid.Interfaces;
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
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Identity.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailHandler _emailHandler;

        public UserAuthService(IUserAuthRepository userAuthRepository,
            IUserRepository userRepository,
            ITokenHandler tokenHandler,
            IEmailHandler emailHandler,
            IMapper mapper)
        {
            _userAuthRepository = userAuthRepository;
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
            _emailHandler = emailHandler;
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
            if (user == null) throw new HttpExceptionResponse
                (StatusCodes.Status401Unauthorized, "Incorrect email or password");

            // Password verification.
            if (!PasswordHandler.VerifyPassword(userCredentials.Password,
                user.Password.Trim()))
                throw new HttpExceptionResponse
                    (StatusCodes.Status401Unauthorized, "Incorrect email or password");

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
                throw new HttpExceptionResponse(StatusCodes.Status400BadRequest,
                    "No refresh token related to user exist.");

            // Check so that the provided refresh token and db refresh token are equal.
            if (refreshTokenDb.Token != refreshToken)
                throw new HttpExceptionResponse(StatusCodes.Status400BadRequest,
                    "The provided refresh token is not valid.");

            var user = await _userRepository.GetUserByUserId(userId);
            
            // Check so that the user actually exist in database and should get a token back. 
            if (user == null)
                throw new HttpExceptionResponse(StatusCodes.Status404NotFound,
                    "No user related to the user id exist.");
            
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
        
        public async Task DeleteUserAccount(string userId, DeleteAccountDto deleteAccountDto)
        {
            var user = await _userRepository.GetUserByUserId(userId);

            if (user == null) 
                throw new HttpExceptionResponse(StatusCodes.Status404NotFound,
                    "No user was found with the provided id");

            var verified = PasswordHandler.
                VerifyPassword(deleteAccountDto.Password, user.Password);
            
            if (!verified) 
                throw new HttpExceptionResponse(StatusCodes.Status400BadRequest,
                    "Incorrect password provided.");

            await _userRepository.DeleteUserByUserId(Guid.Parse(userId));
        }

        public async Task DeleteRefreshToken(string userId)
        {
            await _userAuthRepository.DeleteRefreshTokenByUserId(userId);
        }

        public async Task ChangePassword(ChangePasswordDto changePasswordDto, string userId)
        {
            var changePasswordData = _mapper.Map<ChangePasswordModel>(changePasswordDto);

            var user = await _userRepository.GetUserByUserId(userId);

            var isPasswordValid = PasswordHandler.VerifyPassword
                (changePasswordData.OldPassword, user.Password);

            if (!isPasswordValid)
            {
                throw new HttpExceptionResponse
                    (StatusCodes.Status401Unauthorized,
                    "Incorrect old password provided.");
            }
            
            // Validate new password
            CredentialsValidation.ValidatePasswordLength(changePasswordData.NewPassword);
            var hashedPassword = PasswordHandler.HashPassword(changePasswordDto.NewPassword);

            await _userAuthRepository.UpdatePassword(hashedPassword, userId);
        }
        
        public async Task ChangeEmail(ChangeEmailDto changeEmailDto, string userId)
        {
            var changeEmailData = _mapper.Map<ChangeEmailModel>(changeEmailDto);

            CredentialsValidation.ValidateEmailChange(changeEmailData);

            var user = await _userRepository.GetUserByUserId(userId);

            var isPasswordValid = PasswordHandler.VerifyPassword
                (changeEmailData.Password, user.Password);

            if (!isPasswordValid)
            {
                throw new HttpExceptionResponse
                    (StatusCodes.Status401Unauthorized,
                    "Incorrect password provided.");
            }

            await _userAuthRepository.UpdateEmail(changeEmailData.NewEmail, userId);
        }

        public async Task ResetPasswordByEmail(ResetPasswordDto changeEmailData)
        {
            var token = _tokenHandler.GenerateResetPasswordToken(changeEmailData.Email);
            await _emailHandler.SendResetPasswordEmail(changeEmailData.Email, token);
        }

        public async Task ResetPasswordWithToken(string token, string password)
        {
            _tokenHandler.ValidateJwtToken(token);

            if (!_tokenHandler.ValidateJwtToken(token))
                throw new HttpExceptionResponse(StatusCodes.Status400BadRequest,
                    "The token sent has an invalid format. " +
                    "Please make a new request password request. ");
            
            var decodedToken = _tokenHandler.DecodeToken(token);
            var currentTime = DateTime.Now;
            var currentUnixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
            
            if(currentUnixTime > decodedToken.Payload.Exp)
                throw new HttpExceptionResponse
                    (StatusCodes.Status400BadRequest, 
                    "Request was invalid, probably the link has expired. " +
                    "Please make a new reset password request.");

            var email = decodedToken.Claims
                .Where(c => c.Type == "email")
                .Select(cl => cl.Value).SingleOrDefault();

            if (email == null) throw new HttpExceptionResponse
                (StatusCodes.Status400BadRequest,
                "Request was invalid, probably the link has expired. " +
                "Please make a new reset password request.");

            var user = _userRepository.GetUserByEmail(email);
            
            if (user == null) throw new HttpExceptionResponse
                (StatusCodes.Status400BadRequest, 
                "No user connected to the email could be found.");

            var hashedPassword = PasswordHandler.HashPassword(password);
            await _userAuthRepository.UpdatePasswordByEmail(email, hashedPassword);
        }
    }
}