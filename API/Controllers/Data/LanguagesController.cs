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
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        
        [HttpGet] //api/v1/user/languages/:languageId
        [Route("{languageId}")]
        public async Task<ActionResult<UserLanguageViewModel>> GetUserLanguage(string languageId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _languageService.GetUserLanguage(languageId));
        }
        
        [HttpGet] //api/v1/user/languages
        [Route("")]
        public async Task<ActionResult<UserLanguagesViewModel>> GetUserLanguages()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _languageService.GetUserLanguages(userId));
        }
        
        [HttpPost] //api/v1/user/languages
        [Route("")]
        public async Task<ActionResult<LanguageIdViewModel>> CreateUserLanguage(LanguageDto languageDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _languageService.CreateUserLanguage(userId, languageDto));
        }

        [HttpDelete] //api/v1/user/languages/:languageId
        [Route("{languageId}")]
        public async Task<ActionResult<LanguageIdViewModel>> DeleteUserLanguage(string languageId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _languageService.DeleteUserLanguage(languageId));
        }
    }
}