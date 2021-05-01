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
        private readonly IEducationService _educationSerivce;
        
        public EducationController(IEducationService educationService)
        {
            _educationSerivce = educationService;
        }
        
        /*[HttpGet] //api/v1/user/education
        [Route("")]
        public async Task<ActionResult<WorkExperiencesViewModel>> GetWorkExperiences ()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _workExperienceService.GetWorkExperiences(userId));
        }*/
        
        /*
        [HttpGet] //api/v1/user/workexperience/:workexperienceId
        [Route("{workExperienceId}")]
        public async Task<ActionResult<WorkExperienceViewModel>> GetSingleWorkExperience(string workExperienceId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _workExperienceService.GetWorkExperience(workExperienceId));
        }
        */
        
        [HttpPost] //api/v1/user/workexperience
        [Route("")]
        public async Task<ActionResult<CreateUpdateEducationViewModel>> CreateWorkExperience (EducationDto educationDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _educationSerivce.CreateEducation(userId, educationDto));
        }

        /*[HttpPut] //api/v1/user/workexperience/:workexperienceId
        [Route("{workExperienceId}")]
        public async Task<ActionResult<CreateWorkExperienceViewModel>> UpdateWorkExperience
            (string workExperienceId, WorkExperienceDto workExperienceDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _workExperienceService.UpdateWorkExperience(workExperienceId, workExperienceDto));
        }
        
        [HttpDelete] //api/v1/user/workexperience/:workexperienceId
        [Route("{workExperienceId}")]
        public async Task<ActionResult> DeleteSingleWorkExperience(string workExperienceId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            await _workExperienceService.DeleteWorkExperience(workExperienceId);

            return Ok();
        }*/
    }
}