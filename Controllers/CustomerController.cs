using Carvana.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carvana.Controllers
{
    [ApiController]
    [Route("auth")] // authentication route
    public class CustomerController : ControllerBase
    {
        private readonly string? _userDB; // IMPLEMENT DATABASE

        [HttpGet("/login")]
        public IActionResult Login([FromQuery] string username, string password) // logs user in via credentails passed
        {
            return Ok();
        }


        [HttpPost("/signup")]
        public IActionResult SignUp([FromQuery] string username, [FromQuery] string password) // signs the user up, push to DB
        {
            return Ok();
        }
    }
}