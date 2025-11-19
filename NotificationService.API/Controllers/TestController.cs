using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NotificationService.API.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("ratelimit")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok("Rate limit test successful");
        }
    }

}
