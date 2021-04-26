using System;
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
    public class WorkExperienceController : ControllerBase
    {
        private readonly IWorkExperienceService _workExperienceService;
        
        public WorkExperienceController(IWorkExperienceService workExperienceService)
        {
            _workExperienceService = workExperienceService;
        }
        
        [HttpGet] //api/v1/user/workexperience
        [Route("")]
        public async Task<ActionResult<WorkExperiencesViewModel>> GetWorkExperiences ()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _workExperienceService.GetWorkExperiences(userId));
        }
        
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
        
        [HttpPost] //api/v1/user/workexperience
        [Route("")]
        public async Task<ActionResult<CreateWorkExperienceViewModel>> CreateWorkExperience (WorkExperienceDto workExperienceDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _workExperienceService.CreateWorkExperience(userId, workExperienceDto));
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
        }
    }
}