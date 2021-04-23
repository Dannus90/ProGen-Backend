using System.Linq;
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
    }
}