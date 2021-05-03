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
    public class OtherInformationController : ControllerBase
    {
        private readonly IOtherInformationService _otherInformationService;

        public OtherInformationController(IOtherInformationService otherInformationService)
        {
            _otherInformationService = otherInformationService;
        }
        
        [HttpGet] //api/v1/user/otherinformation
        [Route("")]
        public async Task<ActionResult<OtherInformationViewModel>> GetOtherInformation()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _otherInformationService.GetOtherInformation(userId));
        }
        
        [HttpPut] //api/v1/user/otherinformation
        [Route("")]
        public async Task<ActionResult<OtherInformationViewModel>> UpdateOtherInformation(OtherInformationDto otherInformationDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _otherInformationService.UpdateOtherInformation(userId, otherInformationDto));
        }
    }
}