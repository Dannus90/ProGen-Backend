using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Application.Exceptions;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Data
{
    [ApiController]
    [Authorize]
    [Route("api/v1/user/[controller]")]
    public class FullCvInformationController : ControllerBase
    {
        private readonly IFullCvInformationService _fullCvInformationService;
        
        public FullCvInformationController(IFullCvInformationService fullCvInformationService)
        {
            _fullCvInformationService = fullCvInformationService;
        }
        
        [HttpGet] // api/v1/user/fullcvinformation
        [Route("")]
        public async Task<ActionResult<FullCvInformationViewModel>> GetFullCvInformation ()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _fullCvInformationService.GetFullCvInformation(userId));
        }
    
    }
}