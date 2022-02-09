using Microsoft.AspNetCore.Mvc;

namespace DogTestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult doPing()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }
    }
}
