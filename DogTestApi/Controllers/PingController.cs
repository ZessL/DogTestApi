using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
