using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Identity.Services.Interfaces;
using Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Identity
{
    [ApiController]
    [Route("api/v1/user/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserAuthService userAuthService, ITokenHandler tokenHandler)
        {
            _userAuthService = userAuthService;
            _tokenHandler = tokenHandler;
        }
        
        [HttpPost] //api/v1/user/auth/register
        [Route("register")]
        public async Task<ActionResult> Register(UserCredentialsWithNameDto userCredentialsWithName)
        {
            await _userAuthService.RegisterUser(userCredentialsWithName);
            return StatusCode(201);
        }
        
        [HttpDelete] //api/v1/user/auth/delete-account
        [Authorize]
        [Route("delete-account")]
        public async Task<ActionResult> DeleteAccount(DeleteAccountDto deleteAccountDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) 
                throw new HttpExceptionResponse((int) HttpStatusCode.Unauthorized, "No userId provided");

            await _userAuthService.DeleteUserAccount(userId, deleteAccountDto);
            
            return Ok("Account successfully deleted");
        }
        
        [HttpPost] //api/v1/user/auth/login
        [Route("login")]
        public async Task<ActionResult<TokenResponseViewModel>> Login(UserCredentialsDto userCredentials)
        {
            return Ok(await _userAuthService.LoginUser(userCredentials));
        }
        
        [HttpPost] //api/v1/user/auth/refresh
        [Route("refresh")]
        public async Task<ActionResult<TokenResponseViewModel>> Refresh(TokenDataDto tokenDataDto)
        {
            var userId = _tokenHandler.GetUserIdFromAccessToken(tokenDataDto.AccessToken);

            if (userId == null) throw new HttpExceptionResponse(400, "No userId provided");

            return Ok(await _userAuthService.GenerateAccessTokenFromRefreshToken(userId, tokenDataDto.RefreshToken));
        }
        
        [HttpPost] //api/v1/user/auth/logout
        [Authorize]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            await _userAuthService.DeleteRefreshToken(userId);

            return NoContent();
        }
        
        [HttpPost] //api/v1/user/auth/change-password
        [Authorize]
        [Route("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            await _userAuthService.ChangePassword(changePasswordDto, userId);

            return Ok();
        }
        
        [HttpPost] //api/v1/user/auth/change-email
        [Authorize]
        [Route("change-email")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailDto changeEmailDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            await _userAuthService.ChangeEmail(changeEmailDto, userId);

            return Ok();
        }
        
        [HttpPost] //api/v1/user/auth/request-password-reset
        [Route("request-password-reset")]
        public async Task<ActionResult> ResetPasswordByEmail(ResetPasswordDto changeEmailData)
        {
            await _userAuthService.ResetPasswordByEmail(changeEmailData);

            return Ok();
        }

        [HttpPost] //api/v1/user/auth/reset-password-with-token
        [Route("reset-password-with-token")]
        public async Task<ActionResult> ResetPasswordWithToken
            ([FromQuery] string token, [FromBody] ResetPasswordDto resetPasswordData)
        {
            await _userAuthService.ResetPasswordWithToken(token, resetPasswordData.NewPassword);

            return Ok();
        }
    }
}