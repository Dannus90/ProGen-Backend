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
    [Route("api/v1/user/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;
        
        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }
        
        [HttpGet] //api/v1/user/education
        [Route("")]
        public async Task<ActionResult<EducationsViewModel>> GetEducations ()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _educationService.GetEducations(userId));
        }
        
        [HttpGet] //api/v1/user/education/:educationId
        [Route("{educationId}")]
        public async Task<ActionResult<EducationViewModel>> GetSingleEducation(string educationId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _educationService.GetEducation(educationId, userId));
        }

        [HttpPost] //api/v1/user/education
        [Route("")]
        public async Task<ActionResult<CreateUpdateEducationViewModel>> CreateWorkExperience (EducationDto educationDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _educationService.CreateEducation(userId, educationDto));
        }

        [HttpPut] //api/v1/user/education/:educationId
        [Route("{educationId}")]
        public async Task<ActionResult<EducationViewModel>> UpdateEducation
            (string educationId, EducationDto educationDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _educationService.UpdateEducation(educationId, educationDto, userId));
        }
        
        [HttpDelete] //api/v1/user/education/:educationId
        [Route("{educationId}")]
        public async Task<ActionResult> DeleteSingleEducation(string educationId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            await _educationService.DeleteEducation(educationId, userId);

            return Ok();
        }
    }
}