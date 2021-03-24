using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Identity
{
    [ApiController]
    [Route("api/v1")]
    public class EntryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult EntryPoint()
        {
            return Ok("Welcome to ProGen API Service.");
        }
    }
}