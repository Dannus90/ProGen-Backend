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
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _certificateService;
        
        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpPost] //api/v1/user/certificate
        [Route("")]
        public async Task<ActionResult<CreateUpdateCertificateViewModel>> CreateCertificate 
            (CertificateDto certificateDto)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _certificateService.CreateCertificate(userId, certificateDto));
        }
        
        [HttpGet] //api/v1/user/certificate
        [Route("")]
        public async Task<ActionResult<CertificatesViewModel>> GetAllCertificatesForUser()
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _certificateService.GetAllCertificatesForUser(userId));
        }
        
        [HttpGet] //api/v1/user/certificate/:certificateId
        [Route("{certificateId}")]
        public async Task<ActionResult<CertificateViewModel>> GetSingleCertificateForUser(string certificateId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) 
                throw new HttpExceptionResponse(401, "No userId provided");

            return Ok(await _certificateService.GetCertificateForUser(certificateId, userId));
        }
        
        [HttpDelete] //api/v1/user/certificate/:certificateId
        [Route("{certificateId}")]
        public async Task<ActionResult> DeleteSingleCertificateForUser(string certificateId)
        {
            var currentUser = HttpContext.User;
            var userId = currentUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) 
                throw new HttpExceptionResponse(401, "No userId provided");

            await _certificateService.DeleteSingleCertificateForUser(certificateId, userId);
            
            return NoContent();
        }
    }
}