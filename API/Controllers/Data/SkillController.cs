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
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost] //api/v1/general/skill
        [Route("")]
        public async Task<ActionResult<CreateSkillViewModel>> CreateSkill 
            (SkillDto skillDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _skillService.CreateSkill(skillDto));
        }
        
        [HttpGet] //api/v1/general/skill
        [Route("")]
        public async Task<ActionResult<SkillsViewModel>> GetSkillsBySearchQuery
            ([FromQuery] string searchQuery)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _skillService.GetSkillsBySearchQuery(searchQuery));
        }
        
        [HttpDelete] //api/v1/general/skill/:skillId
        [Route("{skillId}")]
        public async Task<ActionResult<SkillsViewModel>> DeleteSkillById
            (string skillId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            //TODO BLOCK THIS FOR EVERYONE ELSE THAN ADMINS!
            await _skillService.DeleteSkillById(skillId);
            
            return NoContent();
        }
    }
}