using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealWorldApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            // Only authorized users can access this action
            return Ok(new { Data = "Secret Data" });
        }
    }
}
