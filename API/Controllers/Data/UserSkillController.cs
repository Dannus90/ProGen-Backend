using System;
using System.Linq;
using System.Net;
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
    public class UserSkillController : ControllerBase
    {
        private readonly IUserSkillService _userSkillService;
        
        public UserSkillController(IUserSkillService userSkillService)
        {
            _userSkillService = userSkillService;
        }

        [HttpPost] //api/v1/user/userskill
        [Route("")]
        public async Task<ActionResult<CreateUpdateUserSkillViewModel>> CreateUserSkill 
            (UserSkillDto userSkillDto)
        {
            var isValid = Guid.TryParse(userSkillDto.SkillId.ToString(), out var guidOutput);
            
            if (!isValid)
                throw new HttpExceptionResponse((int) HttpStatusCode.BadRequest, "Invalid skillId provided");
            
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _userSkillService.CreateUserSkill(userSkillDto, userId));
        }
        
        [HttpGet] //api/v1/user/userskill
        [Route("")]
        public async Task<ActionResult<UserSkillsViewModel>> GetAllUserSkills 
            ()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _userSkillService.GetAllUserSkills(userId));
        }
        
        [HttpGet] //api/v1/user/userskill/:userSkillId
        [Route("{userSkillId}")]
        public async Task<ActionResult<UserSkillViewModel>> GetSingleUserSkill 
            (string userSkillId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _userSkillService.GetSingleUserSkill(userId, userSkillId));
        }
        
        [HttpDelete] //api/v1/user/userskill/:userSkillId
        [Route("{userSkillId}")]
        public async Task<ActionResult<UserSkillViewModel>> DeleteUserSkill 
            (string userSkillId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            await _userSkillService.DeleteUserSkill(userId, userSkillId);

            return NoContent();
        }
        
        [HttpPatch] //api/v1/user/userskill/:userSkillId
        [Route("{userSkillId}")]
        public async Task<ActionResult<CreateUpdateUserSkillViewModel>> UpdateUserSkill 
            (string userSkillId, UserSkillDto userSkillDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _userSkillService.UpdateUserSkill(userSkillId, userSkillDto));
        }
    }
}