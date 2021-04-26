using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Infrastructure.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Data
{
    [ApiController]
    [Authorize]
    [Route("api/v1/user/[controller]")]
    public class WorkExperienceController : ControllerBase
    {
        private readonly IWorkExperienceService _workExperienceService;
        
        public WorkExperienceController(IWorkExperienceService workExperienceService)
        {
            _workExperienceService = workExperienceService;
        }
        
        [HttpPost] //api/v1/user/workexperience
        [Route("")]
        public async Task<ActionResult> CreateWorkExperience (WorkExperienceDto workExperienceDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _workExperienceService.CreateWorkExperience(userId, workExperienceDto));
        }
    }
}