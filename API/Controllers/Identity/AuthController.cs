using System.Threading.Tasks;
using Core.Domain.Dtos;
using Infrastructure.Identity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Identity
{
    [ApiController]
    [Route("api/v1/user/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;

        public AuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }
        
        /**
         * Responsible for registering users.
         */
        [HttpPost] //api/v1/user/auth/register
        [Route("register")]
        public async Task<ActionResult> Register(UserCredentialsDto userCredentials)
        {
            await _userAuthService.RegisterUser(userCredentials);
            return StatusCode(201);
        }
        
        /**
         * Responsible for logging an user into the application.
         */
        [HttpPost] //api/v1/user/auth/login
        [Route("login")]
        public async Task<ActionResult> Login(UserCredentialsDto userCredentials)
        {
            await _userAuthService.LoginUser(userCredentials);
            return Ok();
        }
        
        /**
         * Responsible for generating a new access token with refresh token.
         */
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> refresh()
        {
            return Ok("Hello");
        }
        
        /**
         * Responsible for logging a user out from an application. 
         */
        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> logout()
        {
            return Ok("Hello");
        }
    }
}