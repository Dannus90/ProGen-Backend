using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Data
{
    [ApiController]
    [Authorize]
    [Route("api/v1/general/[controller]")]
    public class UserSkillController : ControllerBase
    {
        private readonly IUserSkillService _userSkillService;
        
        public UserSkillController(IUserSkillService userSkillService)
        {
            _userSkillService = userSkillService;
        }

        [HttpPost] //api/v1/general/userskill
        [Route("")]
        public async Task<ActionResult<CreateUpdateUserSkillViewModel>> CreateUserSkill 
            (UserSkillDto userSkillDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _userSkillService.CreateUserSkill(userSkillDto, userId));
        }
        
        [HttpGet] //api/v1/general/userskill
        [Route("")]
        public async Task<ActionResult<UserSkillViewModel>> GetAllUserSkills 
            ()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _userSkillService.GetAllUserSkills(userId));
        }
    }
}