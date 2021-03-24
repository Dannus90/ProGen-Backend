using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Identity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register()
        {
            return Ok(_userAuthService.Test());
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login()
        {
            return Ok("Hello");
        }
        
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> refresh()
        {
            return Ok("Hello");
        }
        
        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> logout()
        {
            return Ok("Hello");
        }
    }
}