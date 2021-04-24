using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Data
{
    [ApiController]
    [Authorize]
    [Route("api/v1/user/[controller]")]
    public class UserPresentationController : ControllerBase
    {
        private readonly IUserPresentationService _userPresentationService;

        public UserPresentationController(IUserPresentationService userPresentationService)
        {
            _userPresentationService = userPresentationService;
        }
        
        [HttpGet] //api/v1/user/userpresentation
        [Route("")]
        public async Task<ActionResult<UserPresentationViewModel>> GetUserPresentation()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _userPresentationService.GetUserPresentation(userId));
        }
        
        [HttpPost] //api/v1/user/userpresentation
        [Route("")]
        public async Task<ActionResult<UserPresentationViewModel>> CreateUserPresentation(UserPresentationDto userDataDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _userPresentationService.CreateUserPresentation(userId, userDataDto));
        }
    }
}