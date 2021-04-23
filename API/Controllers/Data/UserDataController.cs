using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Helpers;
using Core.Application.Exceptions;
using Core.Domain.Dtos;
using Core.Domain.ViewModels;
using Infrastructure.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Data
{
    [ApiController]
    [Authorize]
    [Route("api/v1/user/[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataService _userDataService;

        public UserDataController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }
        
        [HttpGet] //api/v1/user/userdata
        [Route("")]
        public async Task<ActionResult<UserInformationViewModel>> GetFullUserData()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _userDataService.GetFullUserData(userId));
        }
        
        [HttpPut] //api/v1/user/userdata
        [Route("")]
        public async Task<ActionResult<UserDataViewModel>> UpdateUserData(UserDataDto userDataDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");
            
            return Ok(await _userDataService.UpdateUserData(userId, userDataDto));
        }

        [HttpPut] //api/v1/user/userdata/profile-image
        [Route("profile-image")]
        public async Task<ActionResult<UserImageViewModel>> UploadProfileImage
            ([FromForm] IFormFile file)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) 
                throw new HttpExceptionResponse(401, "No userId provided");
            
            var fileExt = System.IO.Path.GetExtension(file.FileName);

            if(!FileValidator.ValidateImageUpload(fileExt)) 
                throw new HttpExceptionResponse(415, "Following formats are allowed: jpg, jpeg, png");
            
            return Ok(await _userDataService.UploadProfileImage(file, userId));
        }
        
        [HttpDelete] //api/v1/user/userdata/profile-image/:publicId
        [Route("profile-image/{publicId}")]
        public async Task<ActionResult> DeleteProfileImage(string publicId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            await _userDataService.DeleteProfileImage(publicId, userId);
            
            return Ok();
        }
    }
}